using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using BPOR.Rms.Mappers;
using LuhnNet;
using Microsoft.EntityFrameworkCore;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

namespace BPOR.Rms.Services;

public class EmailCampaignService(
    ParticipantDbContext context,
    IFilterService filterService,
    INotificationService notificationService,
    IRefDataService refDataService,
    IRandomiser randomiser)
    : IEmailCampaignService
{
    public async Task SendCampaignAsync(Domain.Entities.EmailCampaign campaign,
        CancellationToken cancellationToken = default)
    {
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
                        var reference = GenerateVolunteerReference();
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
                            { "reference", emailCampaignParticipant.Id },
                            { "firstName", volunteer.FirstName },
                            { "lastName", volunteer.LastName }
                        };
                        personalisationData.Add(volunteer.Email, personalisation);


                        if (isStudyRecruitingIdentifiable && studyId != null)
                        {
                            context.StudyParticipantEnrollment.Add(new StudyParticipantEnrollment
                            {
                                StudyId = studyId.Value,
                                ParticipantId = volunteer.Id,
                                Reference = GenerateVolunteerReference()
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

    private string GenerateVolunteerReference()
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
            GenerateVolunteerReference();
        }

        return volunteerReference;
    }
}
