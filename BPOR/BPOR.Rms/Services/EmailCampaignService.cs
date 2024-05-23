using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using BPOR.Rms.Mappers;
using BPOR.Rms.Services;
using BPOR.Rms.Settings;
using LuhnNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

public class EmailCampaignService(
    ILogger<EmailCampaignService> logger,
    IOptions<AppSettings> appSettings,
    IRandomiser randomiser,
    IRefDataService refDataService,
    INotificationService notificationService,
    IFilterService filterService,
    IBackgroundTaskQueue taskQueue,
    ParticipantDbContext context)
    : IEmailCampaignService
{
    private static readonly Random _rand = new();

    public async Task SendCampaignAsync(EmailCampaign campaign, CancellationToken cancellationToken = default)
    {
        var emailDeliveryStatusId = refDataService.GetEmailDeliveryStatusId(EmailDeliveryStatus.Pending) ??
                                    throw new InvalidOperationException("Email delivery status not found");

        await context.EmailCampaigns.AddAsync(campaign, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var dbFilter = await GetFilterCriteriaAsync(campaign, cancellationToken);
        if (dbFilter == null)
        {
            logger.LogWarning("No filter criteria found for campaign {CampaignId}", campaign.Id);
            return;
        }

        var filter = FilterMapper.MapToFilterModel(dbFilter);
        var volunteerQuery = await filterService.FilterVolunteersAsync(filter, cancellationToken);

        if (campaign.TargetGroupSize == null || !volunteerQuery.Any())
        {
            logger.LogWarning("No volunteers found for campaign {CampaignId}", campaign.Id);
            return;
        }

        var finalVolunteers = randomiser.GetRandomisedCollection(
            volunteerQuery.Where(v => !string.IsNullOrEmpty(v.Email)).Select(v => new VolunteerProjection
            {
                Id = v.Id,
                Email = v.Email,
                FirstName = v.FirstName,
                LastName = v.LastName
            }).AsQueryable(),
            campaign.TargetGroupSize.Value
        );

        int[] batchSizes = { 50, 100, 200, 500, 1000, 2000, 5000 };
        var results = new Dictionary<int, double>();

        foreach (var batchSize in batchSizes)
        {
            var stopwatch = Stopwatch.StartNew();
            await foreach (var processingResult in ProcessVolunteersAsync(finalVolunteers, campaign, dbFilter,
                               emailDeliveryStatusId, batchSize, cancellationToken))
            {
                await AddParticipantsToContext(processingResult, cancellationToken);
                await QueueEmailSendingTaskAsync(processingResult, campaign, cancellationToken);
            }
            stopwatch.Stop();
            results[batchSize] = stopwatch.Elapsed.TotalMilliseconds;
        }

        var optimalBatchSize = results.MinBy(r => r.Value).Key;
        logger.LogInformation("Optimal batch size determined: {BatchSize}", optimalBatchSize);

        // Use the optimal batch size for actual processing
        var startTime = DateTime.UtcNow;
        logger.LogInformation("Started processing volunteers at {Time}", startTime);
        await foreach (var processingResult in ProcessVolunteersAsync(finalVolunteers, campaign, dbFilter,
                           emailDeliveryStatusId, optimalBatchSize, cancellationToken))
        {
            await AddParticipantsToContext(processingResult, cancellationToken);
            await QueueEmailSendingTaskAsync(processingResult, campaign, cancellationToken);
        }

        var endTime = DateTime.UtcNow;
        logger.LogInformation("Finished processing volunteers at {Time}, Duration: {Duration}ms", endTime,
            (endTime - startTime).TotalMilliseconds);

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<FilterCriteria?> GetFilterCriteriaAsync(EmailCampaign campaign,
        CancellationToken cancellationToken)
    {
        var dbFilter = await context.FilterCriterias
            .AsNoTracking()
            .Include(fc => fc.FilterGender)
            .Include(fc => fc.FilterAreaOfInterest)
            .Include(fc => fc.FilterEthnicGroup)
            .Include(fc => fc.FilterPostcode)
            .Include(fc => fc.FilterSexSameAsRegisteredAtBirth)
            .Include(fc => fc.Study)
            .FirstOrDefaultAsync(fc => fc.Id == campaign.FilterCriteriaId, cancellationToken);

        return dbFilter;
    }

    private async IAsyncEnumerable<ProcessingResults> ProcessVolunteersAsync(
        IEnumerable<VolunteerProjection> volunteers,
        EmailCampaign campaign,
        FilterCriteria dbFilter,
        int emailDeliveryStatusId,
        int batchSize,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var volunteerChunks = volunteers.Chunk(batchSize);

        foreach (var volunteerChunk in volunteerChunks)
        {
            var processingResult = new ProcessingResults();

            var tentativeReferences = volunteerChunk.ToDictionary(
                volunteer => volunteer,
                volunteer => GenerateTentativeVolunteerReference());

            await EnsureUniqueVolunteerReferencesAsync(tentativeReferences, batchSize, cancellationToken);

            var tasks = volunteerChunk.Select(volunteer => Task.Run(() =>
            {
                ProcessSingleVolunteer(volunteer, campaign, dbFilter, processingResult, emailDeliveryStatusId,
                    tentativeReferences[volunteer]);
            }, cancellationToken)).ToList();

            await Task.WhenAll(tasks);

            yield return processingResult;
        }
    }

    private string GenerateTentativeVolunteerReference()
    {
        var stringBuilder = new StringBuilder(15);
        stringBuilder.Append(_rand.Next(1, 10));
        for (int i = 0; i < 14; i++)
        {
            stringBuilder.Append(_rand.Next(0, 10));
        }

        var reference = stringBuilder.ToString();
        var checkDigit = Luhn.CalculateCheckDigit(reference);
        reference += checkDigit.ToString();

        return reference;
    }

    private async Task EnsureUniqueVolunteerReferencesAsync(Dictionary<VolunteerProjection, string> tentativeReferences, int batchSize,
        CancellationToken cancellationToken)
    {
        var references = new HashSet<string>(tentativeReferences.Values);

        while (true)
        {
            var existingReferences = context.StudyParticipantEnrollment.AsNoTracking()
                .Where(e => references.Contains(e.Reference))
                .Select(e => e.Reference);

            if (!existingReferences.Any())
            {
                break;
            }

            var existingReferencesSet = new HashSet<string>(existingReferences);

            var tasks = tentativeReferences.Keys.ToList().Chunk(batchSize).Select(volunteerChunk =>
                Task.Run(() =>
                {
                    foreach (var volunteer in volunteerChunk)
                    {
                        if (!existingReferencesSet.Contains(tentativeReferences[volunteer]))
                        {
                            continue;
                        }

                        var newReference = GenerateTentativeVolunteerReference();
                        while (existingReferencesSet.Contains(newReference))
                        {
                            newReference = GenerateTentativeVolunteerReference();
                        }

                        tentativeReferences[volunteer] = newReference;
                        lock (references)
                        {
                            references.Add(newReference);
                        }
                    }
                }, cancellationToken)).ToArray();

            await Task.WhenAll(tasks);
        }
    }

    private void ProcessSingleVolunteer(
        VolunteerProjection volunteer,
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

    private async Task AddParticipantsToContext(ProcessingResults results, CancellationToken cancellationToken)
    {
        await context.EmailCampaignParticipants.AddRangeAsync(results.EmailCampaignParticipants, cancellationToken);
        await context.StudyParticipantEnrollment.AddRangeAsync(results.StudyParticipantEnrollments, cancellationToken);
    }

    private async Task QueueEmailSendingTaskAsync(ProcessingResults results, EmailCampaign campaign,
        CancellationToken cancellationToken)
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


internal class ProcessingResults
{
    public ConcurrentBag<string> EmailAddresses { get; } = [];
    public ConcurrentDictionary<string, Dictionary<string, dynamic?>> PersonalisationData { get; } = new();
    public ConcurrentBag<EmailCampaignParticipant> EmailCampaignParticipants { get; } = [];
    public ConcurrentBag<StudyParticipantEnrollment> StudyParticipantEnrollments { get; } = [];
}

internal class VolunteerProjection
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
