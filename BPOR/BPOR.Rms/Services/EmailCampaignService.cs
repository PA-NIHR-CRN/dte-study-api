using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using BPOR.Rms.Mappers;
using BPOR.Rms.Settings;
using LuhnNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

namespace BPOR.Rms.Services;

public class EmailCampaignService(
    ILogger<EmailCampaignService> logger,
    IServiceScopeFactory serviceScopeFactory, IOptions<AppSettings> appSettings)
    : IEmailCampaignService
{
    public async Task SendCampaignAsync(EmailCampaign campaign,
        CancellationToken cancellationToken = default)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ParticipantDbContext>();
        var filterService = scope.ServiceProvider.GetRequiredService<IFilterService>();
        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
        var randomiser = scope.ServiceProvider.GetRequiredService<IRandomiser>();
        var refDataService = scope.ServiceProvider.GetRequiredService<IRefDataService>();

        await context.EmailCampaigns.AddAsync(campaign, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        // Retrieve filter criteria and apply it
        var dbFilter = context.FilterCriterias
            .Include(fc => fc.FilterGender)
            .Include(fc => fc.FilterAreaOfInterest)
            .Include(fc => fc.FilterEthnicGroup)
            .Include(fc => fc.FilterPostcode)
            .Include(fc => fc.FilterSexSameAsRegisteredAtBirth)
            .FirstOrDefault(fc => fc.Id == campaign.FilterCriteriaId);

        if (dbFilter != null)
        {
            var filter = FilterMapper.MapToFilterModel(dbFilter);
            var volunteers = await filterService.FilterVolunteersAsync(filter, cancellationToken);

            List<Participant> finalVolunteers = new List<Participant>();

            if (campaign.TargetGroupSize != null && volunteers.Any())
            {
                var volunteersToRandomise = volunteers.Where(v => !string.IsNullOrEmpty(v.Email));
                var randomisedVolunteers =
                    randomiser.GetRandomisedCollection(volunteersToRandomise, campaign.TargetGroupSize.Value);
                finalVolunteers = randomisedVolunteers.ToList();

                var emailAddresses = finalVolunteers.Select(v => v.Email).ToList();

                var studyId = context.FilterCriterias.Where(fc => fc.Id == campaign.FilterCriteriaId)
                    .Select(fc => fc.StudyId).FirstOrDefault();
                var isStudyRecruitingIdentifiable = context.Studies.Where(s => s.Id == studyId)
                    .Select(s => s.IsRecruitingIdentifiableParticipants).FirstOrDefault();
                var personalisationData = new Dictionary<string, Dictionary<string, dynamic>>();

                foreach (var volunteer in finalVolunteers)
                {
                    if (!string.IsNullOrEmpty(volunteer.Email))
                    {
                        var reference = GenerateVolunteerReference(context);
                        var emailCampaignParticipant = new EmailCampaignParticipant
                        {
                            EmailCampaignId = campaign.Id,
                            ParticipantId = volunteer.Id,
                            DeliveryStatusId = refDataService.GetEmailDeliveryStatusId(EmailDeliveryStatus.Pending),
                            SentAt = DateTime.UtcNow,
                            ContactEmail = volunteer.Email,
                        };
                        context.EmailCampaignParticipants.Add(emailCampaignParticipant);

                        await context.SaveChangesAsync(cancellationToken);

                        var personalisation = new Dictionary<string, dynamic>
                        {
                            { "email", volunteer.Email },
                            { "emailCampaignParticipantId", emailCampaignParticipant.Id },
                            { "firstName", volunteer.FirstName },
                            { "lastName", volunteer.LastName },
                            { "uniqueLink", $"{appSettings.Value.BaseUrl}/NotifyCallback/registerinterest?reference={reference}" },
                        };
                        personalisationData.Add(volunteer.Email, personalisation);


                        if (isStudyRecruitingIdentifiable && studyId != null)
                        {
                            context.StudyParticipantEnrollment.Add(new StudyParticipantEnrollment
                            {
                                StudyId = studyId.Value,
                                ParticipantId = volunteer.Id,
                                Reference = reference
                            });
                        }
                    }
                }

                await notificationService.SendBatchEmailAsync(new SendBatchEmailRequest
                {
                    EmailAddresses = emailAddresses,
                    PersonalisationData = personalisationData,
                    EmailTemplateId = campaign.EmailTemplateId
                }, cancellationToken);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private string GenerateVolunteerReference(ParticipantDbContext context)
    {
        // Generate random 15 digit number
        Random rand = new Random();
        string randomNumber = "";

        for (int i = 0; i < 14; i++)
        {
            randomNumber += rand.Next(0, 10);
        }

        randomNumber = rand.Next(1, 10) + randomNumber;

        string volunteerReference = randomNumber;

        var checkDigit = Luhn.CalculateCheckDigit(volunteerReference);

        volunteerReference = volunteerReference + checkDigit.ToString();

        var existingReference = context.StudyParticipantEnrollment.Any(e => e.Reference == volunteerReference);

        if (existingReference)
        {
            GenerateVolunteerReference(context);
        }

        return volunteerReference;
    }
}
