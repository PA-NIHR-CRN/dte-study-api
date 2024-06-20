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
using Microsoft.Extensions.Options;
using NIHR.Infrastructure;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;
using NIHR.Infrastructure.Interfaces;
using NIHR.Infrastructure.Settings;
using NIHR.Infrastructure.Models;
using Polly;
using NIHR.NotificationService.Context;

public class EmailCampaignService(
    ILogger<EmailCampaignService> logger,
    IRefDataService refDataService,
    ParticipantDbContext context,
    IEncryptionService encryptionService,
    IOptions<AppSettings> appSettings,
    IReferenceGenerator referenceGenerator,
    NotificationDbContext notificationContext,
    IPostcodeMapper locationApiClient,
    TimeProvider timeProvider)
    : IEmailCampaignService
{
    public async Task SendCampaignAsync(int emailCampaignId, CancellationToken cancellationToken = default)
    {
        var campaign = await context.EmailCampaigns
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == emailCampaignId, cancellationToken);
        var emailDeliveryStatusId = GetEmailDeliveryStatusId();

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

        await ProcessAndQueueVolunteersAsync(volunteers, campaign, dbFilter, emailDeliveryStatusId, cancellationToken);
    }

    private int GetEmailDeliveryStatusId()
    {
        return refDataService.GetEmailDeliveryStatusId(EmailDeliveryStatus.Pending) ??
               throw new InvalidOperationException("Email delivery status not found");
    }

    private async Task<FilterCriteria?> GetFilterCriteriaAsync(EmailCampaign campaign,
        CancellationToken cancellationToken)
    {
        return await context.FilterCriterias
            .AsNoTracking()
            .IncludeAllFilterProperties()
            .FirstOrDefaultAsync(fc => fc.Id == campaign.FilterCriteriaId, cancellationToken);
    }

    private async Task<List<EmailParticipantDetails>> GetFilteredVolunteersAsync(FilterCriteria dbFilter,
        int? targetGroupSize, CancellationToken cancellationToken)
    {
        var filter = FilterMapper.MapToFilterModel(dbFilter);

        // TODO: save the original search location co-ordinates in the FilterCriteria
        CoordinatesModel? location = null;
        if (dbFilter.FullPostcode is not null && dbFilter.SearchRadiusMiles is not null && dbFilter.SearchRadiusMiles > 0)
        {
            location = await locationApiClient.GetCoordinatesFromPostcodeAsync(dbFilter.FullPostcode, cancellationToken);
        }

        var volunteerQuery = context.Participants.FilterVolunteers(timeProvider, filter, location);

        return await volunteerQuery
            .Where(v => !string.IsNullOrEmpty(v.Email))
            .Randomise()
            .Take(targetGroupSize ?? int.MaxValue)
            .AsEmailCampaignParticipant()
            .ToListAsync(cancellationToken);
    }

    private async Task ProcessAndQueueVolunteersAsync(List<EmailParticipantDetails> volunteers, EmailCampaign campaign,
        FilterCriteria dbFilter, int emailDeliveryStatusId, CancellationToken cancellationToken)
    {
        const int batchSize = 1000;

        var startTime = DateTime.UtcNow;
        logger.LogInformation("Started processing volunteers at {Time}", startTime);

        var emailQueue = new List<ProcessingResults>();

        await foreach (var processingResult in ProcessVolunteersAsync(volunteers, campaign, dbFilter,
                           emailDeliveryStatusId, batchSize, cancellationToken))
        {
            await context.EmailCampaignParticipants.AddRangeAsync(processingResult.EmailCampaignParticipants,
                cancellationToken);
            await context.StudyParticipantEnrollment.AddRangeAsync(processingResult.StudyParticipantEnrollments,
                cancellationToken);
            emailQueue.Add(processingResult);
        }

        var endTime = DateTime.UtcNow;
        logger.LogInformation("Finished processing volunteers at {Time}, Duration: {Duration}ms", endTime,
            (endTime - startTime).TotalMilliseconds);

        await SaveChangesWithRetryAsync(emailQueue, cancellationToken);

        await QueueNotificationsAsync(volunteers, campaign, emailQueue, cancellationToken);
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
        List<EmailParticipantDetails> volunteers,
        EmailCampaign campaign,
        FilterCriteria dbFilter,
        int emailDeliveryStatusId,
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
                ProcessVolunteer(volunteer, campaign, dbFilter, processingResult, emailDeliveryStatusId);
            }

            yield return processingResult;
        }
    }

    private void ProcessVolunteer(EmailParticipantDetails volunteer, EmailCampaign campaign, FilterCriteria dbFilter,
        ProcessingResults processingResult, int emailDeliveryStatusId)
    {
        processingResult.EmailCampaignParticipants.Add(new EmailCampaignParticipant
        {
            EmailCampaignId = campaign.Id,
            ParticipantId = volunteer.Id,
            DeliveryStatusId = emailDeliveryStatusId,
            SentAt = DateTime.UtcNow,
            ContactEmail = volunteer.Email,
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

    private async Task QueueNotificationsAsync(List<EmailParticipantDetails> volunteers, EmailCampaign campaign,
        List<ProcessingResults> emailQueue, CancellationToken cancellationToken)
    {
        foreach (var volunteer in volunteers)
        {
            var emailCampaignParticipant = emailQueue.SelectMany(e => e.EmailCampaignParticipants)
                .FirstOrDefault(e => e.ContactEmail == volunteer.Email);

            if (emailCampaignParticipant == null)
            {
                logger.LogWarning("Email campaign participant not found for volunteer {VolunteerId}", volunteer.Id);
                continue;
            }

            var reference = emailQueue.SelectMany(e => e.StudyParticipantEnrollments)
                .FirstOrDefault(e => e.ParticipantId == volunteer.Id)?.Reference;

            if (reference == null)
            {
                logger.LogWarning("Reference not found for volunteer {VolunteerId}", volunteer.Id);
                continue;
            }

            var notification = new Notification
            {
                PrimaryIdentifier = volunteer.Email,
                NotificationDatas = new List<NotificationData>
                {
                    new() { Key = "email", Value = volunteer.Email },
                    new() { Key = "emailCampaignParticipantId", Value = emailCampaignParticipant.Id.ToString() },
                    new() { Key = "firstName", Value = volunteer.FirstName },
                    new() { Key = "lastName", Value = volunteer.LastName },
                    new()
                    {
                        Key = "uniqueLink",
                        Value =
                            $"{appSettings.Value.WebAppBaseUrl}/NotifyCallback/registerinterest?reference={encryptionService.Encrypt(reference)}"
                    },
                    new() { Key = "emailTemplateId", Value = campaign.EmailTemplateId.ToString() }
                }
            };

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
    public List<EmailCampaignParticipant> EmailCampaignParticipants { get; } = [];
    public List<StudyParticipantEnrollment> StudyParticipantEnrollments { get; } = [];
}
