using System.Runtime.CompilerServices;
using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using BPOR.Rms.Models.Volunteer;
using BPOR.Rms.Services;
using BPOR.Rms.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Polly;
using BPOR.Rms.Models.Email;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using BPOR.Rms.VolunteerInformation;
using BPOR.Rms.VolunteerInformation.Settings;
using Microsoft.Extensions.Options;
using NIHR.NotificationService;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

public class CampaignService(
    ILogger<CampaignService> logger,
    IRefDataService refDataService,
    ParticipantDbContext context,
    IReferenceGenerator referenceGenerator,
    INotificationService<CampaignParticipantNotificationDeliveryHandler> notificationService,
    IVolunteerFilterService volunteerFilterService,
    IVipTokenGenerator tokenGenerator,
    IOptions<VsiSettings> vsiSettings
    )
    : ICampaignService
{
    public async Task SendCampaignAsync(CampaignServiceQueueItem item, CancellationToken cancellationToken = default)
    {
        var campaign = await context.Campaign
            .AsNoTracking()
            .Include(i => i.FilterCriteria).ThenInclude(i => i.Study)
            .FirstOrDefaultAsync(c => c.Id == item.Id, cancellationToken);
        var deliveryStatusId = GetDeliveryStatusId();

        var dbFilter = await GetFilterCriteriaAsync(campaign, cancellationToken);

        if (dbFilter == null)
        {
            logger.LogWarning("No filter criteria found for campaign {CampaignId}", campaign.Id);
            return;
        }

        var volunteers = await volunteerFilterService.GetFilteredVolunteersAsync(dbFilter, campaign.TargetGroupSize, cancellationToken);
        if (!volunteers.Any())
        {
            logger.LogWarning("No volunteers found for campaign {CampaignId}", campaign.Id);
            return;
        }

        await ProcessAndQueueVolunteersAsync(volunteers, campaign, dbFilter, deliveryStatusId, cancellationToken);
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

    private async Task ProcessAndQueueVolunteersAsync(List<CampaignParticipantDetails> volunteers, Campaign campaign,
        FilterCriteria dbFilter, int deliveryStatusId, CancellationToken cancellationToken)
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

        await QueueNotificationsAsync(volunteers, campaign, queue, cancellationToken);
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
            TokenIv = tokenGenerator.GenerateIvString()
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
        List<ProcessingResults> queue, CancellationToken cancellationToken)
    {
        List<UnkeyedSendNotificationRequest> notificationRequests = new();

        foreach (var volunteer in volunteers)
        {
            var validationResults = ValidateParticipantForCampaignType(volunteer, campaign.TypeId).ToArray();
            if (validationResults.Any())
            {
                string reason = string.Join("; ", validationResults.Select(i => i.ErrorMessage));
                logger.LogError("Participant ID:{ParticipantId} is not a valid notification target because {Reason}", volunteer.Id, reason);
                continue;
            }

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

            var queryParams = new Dictionary<string, string>
            {
                { "token", tokenGenerator.GenerateToken(VipTokenPurpose.Volunteer, campaignParticipant.Id, campaignParticipant.TokenIv) }
            };

            var link = QueryHelpers.AddQueryString(vsiSettings.Value.BporVipUri, queryParams);
            var notification = new UnkeyedSendNotificationRequest()
            {
                Reference = campaignParticipant.Id.ToString(),
                ContactMethod = campaignParticipant.CampaignTypeId,
                TemplateId = campaign.TemplateId.ToString(),
                Personalisation =
                {
                    [PersonalisationKeys.CampaignParticipantId] = campaignParticipant.Id.ToString(),
                    [PersonalisationKeys.FirstName] = volunteer.FirstName,
                    [PersonalisationKeys.LastName] = volunteer.LastName,
                    [PersonalisationKeys.UniqueLink] = link,
                    ["studyName"] = campaign.FilterCriteria.Study.StudyName
                }
            };

            switch (campaign.TypeId)
            {
                case GovUkNotifyContactMethod.Email:
                    notification.Personalisation[PersonalisationKeys.Email] = volunteer.Email;
                    break;

                case GovUkNotifyContactMethod.Letter:
                    var addressFields = new Dictionary<string, string?>
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
                            notification.Personalisation[field.Key] = field.Value;
                        }
                    }

                    notification.Personalisation["address_postcode"] = volunteer.Address.Postcode;
                    break;
            }
            
            notificationRequests.Add(notification);
        }

        await notificationService.SendNotifications(notificationRequests, cancellationToken);
    }

    private IEnumerable<ValidationResult> ValidateParticipantForCampaignType(CampaignParticipantDetails volunteer, GovUkNotifyContactMethod campaignTypeId)
    {
        if (string.IsNullOrWhiteSpace(volunteer.FirstName))
        {
            yield return new ValidationResult("FirstName cannot be null, empty or whitespace");
        }

        if (string.IsNullOrWhiteSpace(volunteer.LastName))
        {
            yield return new ValidationResult("LastName cannot be null, empty or whitespace");
        }

        switch (campaignTypeId)
        {
            case GovUkNotifyContactMethod.Email:
                if (string.IsNullOrWhiteSpace(volunteer.Email))
                {
                    yield return new ValidationResult("Email cannot be null, empty or whitespace");
                }

                break;
            case GovUkNotifyContactMethod.Letter:
                if (volunteer.Address == null)
                {
                    yield return new ValidationResult("Address cannot be null");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(volunteer.Address.AddressLine1))
                    {
                        yield return new ValidationResult("AddressLine1 cannot be null, empty or whitespace");
                    }

                    if (string.IsNullOrWhiteSpace(volunteer.Address.Town))
                    {
                        yield return new ValidationResult("Town cannot be null, empty or whitespace");
                    }

                    if (string.IsNullOrWhiteSpace(volunteer.Address.Postcode))
                    {
                        yield return new ValidationResult("Postcode cannot be nnull, empty or whitespaceull");
                    }
                }
                break;
            default:
                yield return new ValidationResult( "Invalid campaign type id");
                break;
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
            .Include(fc => fc.FilterHasLongTermCondition)
            .Include(fc => fc.Study);
    }
}

internal class ProcessingResults
{
    public List<CampaignParticipant> CampaignParticipant { get; } = [];
    public List<StudyParticipantEnrollment> StudyParticipantEnrollments { get; } = [];
}