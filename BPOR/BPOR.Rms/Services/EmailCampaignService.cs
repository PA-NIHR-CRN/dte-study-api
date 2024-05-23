using System.Collections.Concurrent;
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
    IServiceProvider serviceProvider)
    : IEmailCampaignService
{
    private static readonly Random _rand = new();
    private static readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task SendCampaignAsync(EmailCampaign campaign, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();
        var scopedContext = scope.ServiceProvider.GetRequiredService<ParticipantDbContext>();

        // Log the start time
        var startTime = DateTime.UtcNow;
        logger.LogInformation("Started adding campaign to database at {Time}", startTime);

        // Add campaign to database
        await scopedContext.EmailCampaigns.AddAsync(campaign, cancellationToken);
        await scopedContext.SaveChangesAsync(cancellationToken);

        var endTime = DateTime.UtcNow;
        logger.LogInformation("Finished adding campaign to database at {Time}, Duration: {Duration}ms", endTime, (endTime - startTime).TotalMilliseconds);

        // Fetch filter criteria
        var dbFilter = await GetFilterCriteriaAsync(scopedContext, campaign, cancellationToken);
        if (dbFilter == null)
        {
            logger.LogWarning("No filter criteria found for campaign {CampaignId}", campaign.Id);
            return;
        }

        // Filter volunteers
        var filter = FilterMapper.MapToFilterModel(dbFilter);
        var volunteerQuery = await filterService.FilterVolunteersAsync(filter, cancellationToken);

        if (campaign.TargetGroupSize == null || !volunteerQuery.Any())
        {
            logger.LogWarning("No volunteers found for campaign {CampaignId}", campaign.Id);
            return;
        }

        // Randomize volunteers
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

        // Process volunteers asynchronously
        await foreach (var processingResult in ProcessVolunteersAsync(finalVolunteers, campaign, dbFilter, cancellationToken))
        {
            // Save processed participants to database
            await SaveParticipantsToDatabaseAsync(scopedContext, processingResult, cancellationToken);
            
            // Queue background work item for sending emails
            await QueueEmailSendingTaskAsync(processingResult, campaign, cancellationToken);
        }
    }

    private async Task<FilterCriteria?> GetFilterCriteriaAsync(ParticipantDbContext scopedContext, EmailCampaign campaign, CancellationToken cancellationToken)
    {
        var startTime = DateTime.UtcNow;
        logger.LogInformation("Started fetching filter criteria at {Time}", startTime);

        var dbFilter = await scopedContext.FilterCriterias
            .AsNoTracking()
            .Include(fc => fc.FilterGender)
            .Include(fc => fc.FilterAreaOfInterest)
            .Include(fc => fc.FilterEthnicGroup)
            .Include(fc => fc.FilterPostcode)
            .Include(fc => fc.FilterSexSameAsRegisteredAtBirth)
            .Include(fc => fc.Study)
            .FirstOrDefaultAsync(fc => fc.Id == campaign.FilterCriteriaId, cancellationToken);

        var endTime = DateTime.UtcNow;
        logger.LogInformation("Finished fetching filter criteria at {Time}, Duration: {Duration}ms", endTime, (endTime - startTime).TotalMilliseconds);

        return dbFilter;
    }

    private async IAsyncEnumerable<ProcessingResults> ProcessVolunteersAsync(
        IEnumerable<VolunteerProjection> volunteers,
        EmailCampaign campaign,
        FilterCriteria dbFilter,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var emailAddresses = new ConcurrentBag<string>();
        var personalisationData = new ConcurrentDictionary<string, Dictionary<string, dynamic?>>();
        var emailCampaignParticipants = new ConcurrentBag<EmailCampaignParticipant>();
        var studyParticipantEnrollments = new ConcurrentBag<StudyParticipantEnrollment>();

        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount,
            CancellationToken = cancellationToken
        };

        var startTime = DateTime.UtcNow;
        logger.LogInformation("Started processing volunteers at {Time}", startTime);

        await Task.Run(() =>
        {
            Parallel.ForEach(volunteers, parallelOptions, async volunteer =>
            {
                await ProcessSingleVolunteer(volunteer, campaign, dbFilter, emailAddresses, personalisationData,
                    emailCampaignParticipants, studyParticipantEnrollments, cancellationToken);
            });
        }, cancellationToken);

        var endTime = DateTime.UtcNow;
        logger.LogInformation("Finished processing volunteers at {Time}, Duration: {Duration}ms", endTime, (endTime - startTime).TotalMilliseconds);

        yield return new ProcessingResults
        {
            EmailAddresses = emailAddresses.ToList(),
            PersonalisationData = personalisationData,
            EmailCampaignParticipants = emailCampaignParticipants,
            StudyParticipantEnrollments = studyParticipantEnrollments
        };
    }

    private async Task ProcessSingleVolunteer(
        VolunteerProjection volunteer,
        EmailCampaign campaign,
        FilterCriteria dbFilter,
        ConcurrentBag<string> emailAddresses,
        ConcurrentDictionary<string, Dictionary<string, dynamic?>> personalisationData,
        ConcurrentBag<EmailCampaignParticipant> emailCampaignParticipants,
        ConcurrentBag<StudyParticipantEnrollment> studyParticipantEnrollments,
        CancellationToken cancellationToken)
    {
        using var taskScope = serviceProvider.CreateScope();
        var taskContext = taskScope.ServiceProvider.GetRequiredService<ParticipantDbContext>();

        var reference = await GenerateVolunteerReference(taskContext, cancellationToken);

        var emailCampaignParticipant = new EmailCampaignParticipant
        {
            EmailCampaignId = campaign.Id,
            ParticipantId = volunteer.Id,
            DeliveryStatusId = refDataService.GetEmailDeliveryStatusId(EmailDeliveryStatus.Pending),
            SentAt = DateTime.UtcNow,
            ContactEmail = volunteer.Email,
        };

        emailCampaignParticipants.Add(emailCampaignParticipant);
        emailAddresses.Add(volunteer.Email);

        var personalisation = new Dictionary<string, dynamic?>
        {
            { "email", volunteer.Email },
            { "emailCampaignParticipantId", emailCampaignParticipant.Id },
            { "firstName", volunteer.FirstName },
            { "lastName", volunteer.LastName },
            { "uniqueLink", $"{appSettings.Value.BaseUrl}/NotifyCallback/registerinterest?reference={reference}" },
        };
        personalisationData[volunteer.Email] = personalisation;

        if (dbFilter is { Study.IsRecruitingIdentifiableParticipants: true, StudyId: not null })
        {
            studyParticipantEnrollments.Add(new StudyParticipantEnrollment
            {
                StudyId = (int)dbFilter.StudyId,
                ParticipantId = volunteer.Id,
                Reference = reference
            });
        }
    }

    private async Task SaveParticipantsToDatabaseAsync(ParticipantDbContext context, ProcessingResults results, CancellationToken cancellationToken)
    {
        var startTime = DateTime.UtcNow;
        logger.LogInformation("Started saving participants to database at {Time}", startTime);

        await context.EmailCampaignParticipants.AddRangeAsync(results.EmailCampaignParticipants, cancellationToken);
        await context.StudyParticipantEnrollment.AddRangeAsync(results.StudyParticipantEnrollments, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var endTime = DateTime.UtcNow;
        logger.LogInformation("Finished saving participants to database at {Time}, Duration: {Duration}ms", endTime, (endTime - startTime).TotalMilliseconds);
    }

    private async Task QueueEmailSendingTaskAsync(ProcessingResults results, EmailCampaign campaign, CancellationToken cancellationToken)
    {
        var startTime = DateTime.UtcNow;
        logger.LogInformation("Started queuing background work item at {Time}", startTime);

        await taskQueue.QueueBackgroundWorkItemAsync(async token =>
        {
            await notificationService.SendBatchEmailAsync(new SendBatchEmailRequest
            {
                EmailAddresses = results.EmailAddresses,
                PersonalisationData = results.PersonalisationData,
                EmailTemplateId = campaign.EmailTemplateId
            }, token);
        });

        var endTime = DateTime.UtcNow;
        logger.LogInformation("Finished queuing background work item at {Time}, Duration: {Duration}ms", endTime, (endTime - startTime).TotalMilliseconds);
    }

    private static async Task<string> GenerateVolunteerReference(ParticipantDbContext context, CancellationToken cancellationToken)
    {
        string volunteerReference;
        bool referenceExists;

        do
        {
            var stringBuilder = new StringBuilder(15);
            stringBuilder.Append(_rand.Next(1, 10));
            for (int i = 0; i < 14; i++)
            {
                stringBuilder.Append(_rand.Next(0, 10));
            }

            volunteerReference = stringBuilder.ToString();
            var checkDigit = Luhn.CalculateCheckDigit(volunteerReference);
            volunteerReference += checkDigit.ToString();

            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                referenceExists = await context.StudyParticipantEnrollment
                    .AnyAsync(e => e.Reference == volunteerReference, cancellationToken);
            }
            finally
            {
                _semaphore.Release();
            }

        } while (referenceExists);

        return volunteerReference;
    }
}

public class ProcessingResults
{
    public List<string> EmailAddresses { get; set; }
    public ConcurrentDictionary<string, Dictionary<string, dynamic?>> PersonalisationData { get; set; }
    public ConcurrentBag<EmailCampaignParticipant> EmailCampaignParticipants { get; set; }
    public ConcurrentBag<StudyParticipantEnrollment> StudyParticipantEnrollments { get; set; }
}

public class VolunteerProjection
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
