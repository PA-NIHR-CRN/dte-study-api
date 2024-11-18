using System.Runtime.CompilerServices;
using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms;
using BPOR.Rms.Constants;
using BPOR.Rms.Mappers;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Volunteer;
using BPOR.Rms.Services;
using BPOR.Rms.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;
using NIHR.Infrastructure.Interfaces;
using Polly;
using NIHR.NotificationService.Context;
using BPOR.Domain.Entities.Configuration;
using BPOR.Rms.Models.Email;
using Microsoft.AspNetCore.WebUtilities;
using NetTopologySuite.Geometries;
using BPOR.Rms.Models.Study;
using NIHR.NotificationService.Models;

public class CampaignService(
    ILogger<CampaignService> logger,
    IRefDataService refDataService,
    ParticipantDbContext context,
    IEncryptionService encryptionService,
    IReferenceGenerator referenceGenerator,
    NotificationDbContext notificationContext,
    IPostcodeMapper locationApiClient,
    TimeProvider timeProvider
    )
    : ICampaignService
{
    public async Task SendCampaignAsync(ServiceQueueItem item, CancellationToken cancellationToken = default)
    {
        var campaign = await context.Campaigns
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == item.Id, cancellationToken);
        var deliveryStatusId = GetDeliveryStatusId();

        var dbFilter = await GetFilterCriteriaAsync(campaign, cancellationToken);

        if (dbFilter == null)
        {
            logger.LogWarning("No filter criteria found for campaign {CampaignId}", campaign.Id);
            return;
        }

        var volunteers = await GetFilteredVolunteersAsync(dbFilter, campaign.TargetGroupSize, cancellationToken);
        if (!volunteers.Any())
        {
            logger.LogWarning("No volunteers found for campaign {CampaignId}", campaign.Id);
            return;
        }

        await ProcessAndQueueVolunteersAsync(volunteers, campaign, dbFilter, deliveryStatusId, item.Callback, cancellationToken);
    }

    private int GetDeliveryStatusId()
    {
        return refDataService.GetDeliveryStatusId(DeliveryStatus.Pending) ??
               throw new InvalidOperationException("Campaign delivery status not found");
    }

    private async Task<FilterCriteria?> GetFilterCriteriaAsync(Campaign campaign,
        CancellationToken cancellationToken)
    {
        return await context.FilterCriterias
            .AsNoTracking()
            .IncludeAllFilterProperties()
            .FirstOrDefaultAsync(fc => fc.Id == campaign.FilterCriteriaId, cancellationToken);
    }

    private async Task<List<CampaignParticipantDetails>> GetFilteredVolunteersAsync(FilterCriteria dbFilter,
        int? targetGroupSize, CancellationToken cancellationToken)
    {
        var filter = FilterMapper.MapToFilterModel(dbFilter);

        // TODO: save the original search location co-ordinates in the FilterCriteria
        if (dbFilter.FullPostcode is not null && dbFilter.SearchRadiusMiles is not null && dbFilter.SearchRadiusMiles > 0)
        {
            var location = await locationApiClient.GetCoordinatesFromPostcodeAsync(dbFilter.FullPostcode, cancellationToken);
            filter.PostcodeSearch.PostcodeRadiusSearch.Location = new Point(location.Longitude, location.Latitude) { SRID = ParticipantLocationConfiguration.LocationSrid };
        }

        var volunteerQuery = context.Participants.FilterVolunteers(timeProvider, filter);

        return await volunteerQuery
            .Where(v => !string.IsNullOrEmpty(v.Email))
            .Randomise()
            .Take(targetGroupSize ?? int.MaxValue)
            .AsCampaignParticipant()
            .ToListAsync(cancellationToken);
    }

    private async Task ProcessAndQueueVolunteersAsync(List<CampaignParticipantDetails> volunteers, Campaign campaign,
        FilterCriteria dbFilter, int deliveryStatusId, string callback, CancellationToken cancellationToken)
    {
        const int batchSize = 1000;

        var startTime = DateTime.UtcNow;
        logger.LogInformation("Started processing volunteers at {Time}", startTime);

        var queue = new List<ProcessingResults>();

        await foreach (var processingResult in ProcessVolunteersAsync(volunteers, campaign, dbFilter,
                           deliveryStatusId, batchSize, cancellationToken))
        {
            await context.CampaignParticipants.AddRangeAsync(processingResult.CampaignParticipants,
                cancellationToken);
            await context.StudyParticipantEnrollment.AddRangeAsync(processingResult.StudyParticipantEnrollments,
                cancellationToken);
            queue.Add(processingResult);
        }

        var endTime = DateTime.UtcNow;
        logger.LogInformation("Finished processing volunteers at {Time}, Duration: {Duration}ms", endTime,
            (endTime - startTime).TotalMilliseconds);

        await SaveChangesWithRetryAsync(queue, cancellationToken);

        await QueueNotificationsAsync(volunteers, campaign, queue, callback, cancellationToken);
    }

    private async Task SaveChangesWithRetryAsync(List<ProcessingResults> emailQueue,
        CancellationToken cancellationToken)
    {
        var retryPolicy = Policy
            .Handle<DbUpdateException>(IsUniqueConstraintViolation)
            .RetryAsync(3, onRetry: (exception, retryCount) =>
        {
            logger.LogError(exception, "Error saving email campaign participants, attempt {Attempt}", retryCount);
            UpdateUniqueConstraintViolations(emailQueue);
        });

        await retryPolicy.ExecuteAsync(async () => await context.SaveChangesAsync(cancellationToken));
    }

    private void UpdateUniqueConstraintViolations(List<ProcessingResults> emailQueue)
    {
        foreach (var participant in context.ChangeTracker.Entries<StudyParticipantEnrollment>()
                     .Where(e => e.State == EntityState.Added))
        {
            var newReference = referenceGenerator.GenerateReference();
            participant.Entity.Reference = newReference;

            foreach (var processingResult in emailQueue)
            {
                UpdateProcessingResultReferences(processingResult, participant.Entity.ParticipantId, newReference);
            }
        }
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        return ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE constraint failed");
    }

    private async IAsyncEnumerable<ProcessingResults> ProcessVolunteersAsync(
        List<CampaignParticipantDetails> volunteers,
        Campaign campaign,
        FilterCriteria dbFilter,
        int deliveryStatusId,
        int batchSize,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var volunteerChunks = volunteers.Chunk(batchSize);

        foreach (var volunteerChunk in volunteerChunks)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var processingResult = new ProcessingResults();

            foreach (var volunteer in volunteerChunk)
            {
                ProcessVolunteer(volunteer, campaign, dbFilter, processingResult, deliveryStatusId);
            }

            yield return processingResult;
        }
    }

    private void ProcessVolunteer(CampaignParticipantDetails volunteer, Campaign campaign, FilterCriteria dbFilter,
        ProcessingResults processingResult, int deliveryStatusId)
    {
        // TODO: add address for letter?
        processingResult.CampaignParticipants.Add(new CampaignParticipant
        {
            CampaignId = campaign.Id,
            CampaignTypeId = campaign.TypeId,
            ParticipantId = volunteer.Id,
            DeliveryStatusId = deliveryStatusId,
            SentAt = DateTime.UtcNow,
            ContactEmail = volunteer.Email
        });

        if (dbFilter is { Study.IsRecruitingIdentifiableParticipants: true, StudyId: not null })
        {
            var reference = referenceGenerator.GenerateReference();
            processingResult.StudyParticipantEnrollments.Add(new StudyParticipantEnrollment
            {
                StudyId = (int)dbFilter.StudyId,
                ParticipantId = volunteer.Id,
                Reference = reference
            });
        }
    }


    private void UpdateProcessingResultReferences(ProcessingResults processingResult, int participantId,
        string newReference)
    {
        var studyParticipant =
            processingResult.StudyParticipantEnrollments.FirstOrDefault(e => e.ParticipantId == participantId);
        if (studyParticipant != null)
        {
            studyParticipant.Reference = newReference;
        }
    }

    private async Task QueueNotificationsAsync(List<CampaignParticipantDetails> volunteers, Campaign campaign,
        List<ProcessingResults> queue, string callback, CancellationToken cancellationToken)
    {
        foreach (var volunteer in volunteers)
        {
            var campaignParticipant = queue.SelectMany(e => e.CampaignParticipants)
                .FirstOrDefault(e => e.ParticipantId == volunteer.Id);

            if (campaignParticipant == null)
            {
                logger.LogWarning("Campaign participant not found for volunteer {VolunteerId}", volunteer.Id);
                continue;
            }

            var reference = queue.SelectMany(e => e.StudyParticipantEnrollments)
                .FirstOrDefault(e => e.ParticipantId == volunteer.Id)?.Reference;

            if (reference == null)
            {
                logger.LogWarning("Reference not found for volunteer {VolunteerId}", volunteer.Id);
                reference = $"no-study.{campaignParticipant.Id}";
            }

            var baseUri = new Uri(callback);
            var queryParams = new Dictionary<string, string>
            {
                { "reference", encryptionService.Encrypt(campaignParticipant.Id.ToString()) }
            };

            var uriWithQuery = new Uri(QueryHelpers.AddQueryString(baseUri.ToString(), queryParams));
            var link = uriWithQuery.ToString();
            var notification = new Notification
            {
                NotificationDatas = new List<NotificationData>
                {
                    new() { Key = "campaignParticipantId", Value = campaignParticipant.Id.ToString() },
                    new() { Key = "campaignTypeId", Value = campaignParticipant.CampaignTypeId.ToString() },
                    new() { Key = "firstName", Value = volunteer.FirstName },
                    new() { Key = "lastName", Value = volunteer.LastName },
                    new() { Key = "uniqueLink", Value = link },
                    new() { Key = "templateId", Value = campaign.TemplateId.ToString() },
                    new() { Key = "uniqueReference", Value = reference }
                }
            };

            switch (campaign.TypeId)
            {
                case (int)ContactMethod.Email:
                    notification.PrimaryIdentifier = volunteer.Email;
                    notification.NotificationDatas.Add(new NotificationData { Key = "email", Value = volunteer.Email });
                    break;

                case (int)ContactMethod.Letter:
                    notification.PrimaryIdentifier = campaignParticipant.Id.ToString(); //  TODO: KO what primary identifier?
                    notification.NotificationDatas.Add(new NotificationData { Key = "address_line_1", Value = volunteer.Address.AddressLine1 });
                    notification.NotificationDatas.Add(new NotificationData { Key = "address_line_2", Value = volunteer.Address.AddressLine2 });
                    notification.NotificationDatas.Add(new NotificationData { Key = "address_line_3", Value = volunteer.Address.AddressLine3 }); 
                    notification.NotificationDatas.Add(new NotificationData { Key = "address_line_4", Value = volunteer.Address.Postcode }); // last line treated as postcode


                    break;
            }

            await notificationContext.Notifications.AddAsync(notification, cancellationToken);
        }

        await notificationContext.SaveChangesAsync(cancellationToken);
    }
}

internal static class DbContextExtensions
{
    public static IQueryable<FilterCriteria> IncludeAllFilterProperties(this IQueryable<FilterCriteria> query)
    {
        return query
            .Include(fc => fc.FilterGender)
            .Include(fc => fc.FilterAreaOfInterest)
            .Include(fc => fc.FilterEthnicGroup)
            .Include(fc => fc.FilterPostcode)
            .Include(fc => fc.FilterSexSameAsRegisteredAtBirth)
            .Include(fc => fc.Study);
    }
}

internal class ProcessingResults
{
    public List<CampaignParticipant> CampaignParticipants { get; } = [];
    public List<StudyParticipantEnrollment> StudyParticipantEnrollments { get; } = [];
}
