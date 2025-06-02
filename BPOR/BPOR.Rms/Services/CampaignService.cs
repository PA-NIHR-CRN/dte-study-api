using BPOR.Domain.Enums;
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
using BPOR.Domain.Extensions;

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
    public async Task SendCampaignAsync(CampaignServiceQueueItem item, CancellationToken cancellationToken = default)
    {
        var campaign = await context.Campaign
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
        return refDataService.GetDeliveryStatusId(DeliveryStatusNames.Pending) ??
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
        const double minPageSizeForRetry = 0.05;
        const double timeoutPageSizeFactor = 0.5;

        var filter = FilterMapper.MapToFilterModel(dbFilter);

        // TODO: save the original search location co-ordinates in the FilterCriteria
        if (dbFilter.FullPostcode is not null && dbFilter.SearchRadiusMiles is not null && dbFilter.SearchRadiusMiles > 0)
        {
            var location = await locationApiClient.GetCoordinatesFromPostcodeAsync(dbFilter.FullPostcode, cancellationToken);
            filter.PostcodeSearch.PostcodeRadiusSearch.Location = new Point(location.Longitude, location.Latitude) { SRID = ParticipantLocationConfiguration.LocationSrid };
        }

        var result = new List<CampaignParticipantDetails>();

        int seed = Random.Shared.Next();

        double pageMin = 0.0;
        double pageSize = 0.1;

        while (pageMin < 1.0 && (!targetGroupSize.HasValue || targetGroupSize.Value > result.Count))
        {

            double pageMax = pageMin + pageSize;
            var volunteerQuery = context
                .GetRandomSampleOfParticipants(seed, pageMin, pageMax)
                .FilterVolunteers(timeProvider, filter)
                .Where(v => !string.IsNullOrEmpty(v.Email)); // TODO: This is suspect? Why are we always excluding participants with no email address?

            if (targetGroupSize.HasValue)
            {
                volunteerQuery = volunteerQuery.Take(targetGroupSize.Value - result.Count);                   
            }

            try
            {
                result.AddRange(await volunteerQuery.AsCampaignParticipant().ToArrayAsync(cancellationToken));
                pageMin = pageMax;
            }
            catch (TimeoutException)
            {
                if (pageSize < minPageSizeForRetry)
                {
                    logger.LogError("Volunteer Filter query timed out for filter {filterId} and page size {pageSize}.", dbFilter.Id, pageSize);
                    throw;
                }
                else
                {
                    logger.LogWarning("Volunteer Filter query timed out for filter {filterId} and page size {pageSize} - this operation will be retried with a smaller window", dbFilter.Id, pageSize);
                    pageSize *= timeoutPageSizeFactor;
                }
            }

        }
        
        return result;
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
            await context.CampaignParticipant.AddRangeAsync(processingResult.CampaignParticipant,
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
        processingResult.CampaignParticipant.Add(new CampaignParticipant
        {
            CampaignId = campaign.Id,
            CampaignTypeId = campaign.TypeId,
            ParticipantId = volunteer.Id,
            DeliveryStatusId = deliveryStatusId,
            SentAt = DateTime.UtcNow,
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
            var campaignParticipant = queue.SelectMany(e => e.CampaignParticipant)
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
                    new() { Key = PersonalisationKeys.CampaignParticipantId, Value = campaignParticipant.Id.ToString() },
                    new() { Key = PersonalisationKeys.CampaignTypeId, Value = ((int)campaignParticipant.CampaignTypeId).ToString() },
                    new() { Key = PersonalisationKeys.FirstName, Value = volunteer.FirstName },
                    new() { Key = PersonalisationKeys.LastName, Value = volunteer.LastName },
                    new() { Key = PersonalisationKeys.UniqueLink, Value = link },
                    new() { Key = PersonalisationKeys.TemplateId, Value = campaign.TemplateId.ToString() },
                    new() { Key = PersonalisationKeys.UniqueReference, Value = reference }
                }
            };

            switch (campaign.TypeId)
            {
                case ContactMethodId.Email:
                    notification.PrimaryIdentifier = volunteer.Email;
                    notification.NotificationDatas.Add(new NotificationData { Key = PersonalisationKeys.Email, Value = volunteer.Email });
                    break;

                case ContactMethodId.Letter:
                    if (string.IsNullOrWhiteSpace(volunteer.Address.AddressLine1) ||
                        string.IsNullOrWhiteSpace(volunteer.Address.Town) ||
                        string.IsNullOrWhiteSpace(volunteer.Address.Postcode))
                    {
                        throw new InvalidOperationException("Letter notifications require at least 3 address lines");
                    }

                    notification.PrimaryIdentifier = $"ParticipantAddress({volunteer.Address.Id})";

                    var addressFields = new Dictionary<string, string>
                    {
                        { "address_line_1", volunteer.Address.AddressLine1 },
                        { "address_line_2", volunteer.Address.AddressLine2 },
                        { "address_line_3", volunteer.Address.AddressLine3 },
                        { "address_line_4", volunteer.Address.AddressLine4 },
                        { "address_line_5", volunteer.Address.Town },
                        { "address_line_6", volunteer.Address.Postcode }
                    };

                    foreach (var field in addressFields)
                    {
                        if (!string.IsNullOrWhiteSpace(field.Value))
                        {
                            notification.NotificationDatas.Add(new NotificationData
                            {
                                Key = field.Key,
                                Value = field.Value
                            });
                        }
                    }

                    notification.NotificationDatas.Add(new NotificationData { Key = "address_postcode", Value = volunteer.Address.Postcode });

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
    public List<CampaignParticipant> CampaignParticipant { get; } = [];
    public List<StudyParticipantEnrollment> StudyParticipantEnrollments { get; } = [];
}