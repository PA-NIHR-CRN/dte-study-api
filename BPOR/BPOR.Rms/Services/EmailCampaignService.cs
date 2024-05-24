using System.Runtime.CompilerServices;
using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using BPOR.Rms.Mappers;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Volunteer;
using BPOR.Rms.Services;
using BPOR.Rms.Settings;
using BPOR.Rms.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

public class EmailCampaignService(
    ILogger<EmailCampaignService> logger,
    IOptions<AppSettings> appSettings,
    IRefDataService refDataService,
    INotificationService notificationService,
    IFilterService filterService,
    INotificationTaskQueue taskQueue,
    ParticipantDbContext context,
    IReferenceGenerator referenceGenerator)
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
        var volunteerQuery = await filterService.FilterVolunteersAsync(filter, cancellationToken);

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
            await AddParticipantsToContextAsync(processingResult, cancellationToken);
            emailQueue.Add(processingResult);
        }

        var endTime = DateTime.UtcNow;
        logger.LogInformation("Finished processing volunteers at {Time}, Duration: {Duration}ms", endTime,
            (endTime - startTime).TotalMilliseconds);

        await SaveChangesWithRetryAsync(emailQueue, cancellationToken);
        await QueueEmailSendingTaskAsync(emailQueue, campaign, cancellationToken);
    }

    private async Task SaveChangesWithRetryAsync(List<ProcessingResults> emailQueue,
        CancellationToken cancellationToken)
    {
        var retryCount = 0;
        const int maxRetries = 3;

        while (retryCount < maxRetries)
        {
            try
            {
                await context.SaveChangesAsync(cancellationToken);
                break;
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                logger.LogError(ex, "Error saving email campaign participants, attempt {Attempt}", retryCount + 1);
                retryCount++;
                UpdateUniqueConstraintViolations(emailQueue);
            }
        }
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

            var references = volunteerChunk.ToDictionary(
                volunteer => volunteer,
                volunteer => referenceGenerator.GenerateReference());

            foreach (var volunteer in volunteerChunk)
            {
                ProcessVolunteer(volunteer, campaign, dbFilter, processingResult, emailDeliveryStatusId,
                    references[volunteer]);
            }

            yield return processingResult;
        }
    }

    private void ProcessVolunteer(
        EmailParticipantDetails volunteer,
        EmailCampaign campaign,
        FilterCriteria dbFilter,
        ProcessingResults processingResult,
        int emailDeliveryStatusId,
        string reference)
    {
        var emailCampaignParticipant = new EmailCampaignParticipant
        {
            EmailCampaignId = campaign.Id,
            ParticipantId = volunteer.Id,
            DeliveryStatusId = emailDeliveryStatusId,
            SentAt = DateTime.UtcNow,
            ContactEmail = volunteer.Email,
        };

        processingResult.EmailCampaignParticipants.Add(emailCampaignParticipant);
        processingResult.EmailAddresses.Add(volunteer.Email);

        var personalisation = new Dictionary<string, dynamic?>
        {
            { "email", volunteer.Email },
            { "emailCampaignParticipantId", emailCampaignParticipant.Id },
            { "firstName", volunteer.FirstName },
            { "lastName", volunteer.LastName },
            { "uniqueLink", $"{appSettings.Value.BaseUrl}/NotifyCallback/registerinterest?reference={reference}" },
        };
        processingResult.PersonalisationData[volunteer.Email] = personalisation;

        if (dbFilter is { Study.IsRecruitingIdentifiableParticipants: true, StudyId: not null })
        {
            processingResult.StudyParticipantEnrollments.Add(new StudyParticipantEnrollment
            {
                StudyId = (int)dbFilter.StudyId,
                ParticipantId = volunteer.Id,
                Reference = reference
            });
        }
    }

    private async Task AddParticipantsToContextAsync(ProcessingResults results, CancellationToken cancellationToken)
    {
        await context.EmailCampaignParticipants.AddRangeAsync(results.EmailCampaignParticipants, cancellationToken);
        await context.StudyParticipantEnrollment.AddRangeAsync(results.StudyParticipantEnrollments, cancellationToken);
    }

    private async Task QueueEmailSendingTaskAsync(List<ProcessingResults> emailQueue, EmailCampaign campaign,
        CancellationToken cancellationToken)
    {
        foreach (var results in emailQueue)
        {
            await taskQueue.QueueBackgroundWorkItemAsync(async token =>
            {
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, token);
                await notificationService.SendBatchEmailAsync(new SendBatchEmailRequest
                {
                    EmailAddresses = results.EmailAddresses,
                    PersonalisationData = results.PersonalisationData,
                    EmailTemplateId = campaign.EmailTemplateId
                }, linkedCts.Token);
            });
        }
    }

    private void UpdateProcessingResultReferences(ProcessingResults processingResult, int participantId,
        string newReference)
    {
        foreach (var personalisation in processingResult.PersonalisationData.Values.Where(personalisation =>
                     personalisation.ContainsKey("emailCampaignParticipantId") &&
                     (int)personalisation["emailCampaignParticipantId"] == participantId))
        {
            personalisation["uniqueLink"] =
                $"{appSettings.Value.BaseUrl}/NotifyCallback/registerinterest?reference={newReference}";
        }

        var studyParticipant =
            processingResult.StudyParticipantEnrollments.FirstOrDefault(e => e.ParticipantId == participantId);
        if (studyParticipant != null)
        {
            studyParticipant.Reference = newReference;
        }
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
    public List<string> EmailAddresses { get; } = new();
    public Dictionary<string, Dictionary<string, dynamic?>> PersonalisationData { get; } = new();
    public List<EmailCampaignParticipant> EmailCampaignParticipants { get; } = new();
    public List<StudyParticipantEnrollment> StudyParticipantEnrollments { get; } = new();
}
