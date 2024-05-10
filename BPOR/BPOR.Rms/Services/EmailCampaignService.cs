using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using BPOR.Rms.Mappers;
using BPOR.Rms.Models.Study;
using LuhnNet;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.Interfaces;
using NIHR.Infrastructure.Models;

namespace BPOR.Rms.Services;

public class EmailCampaignService(
    ParticipantDbContext context,
    IFilterService filterService,
    INotificationService notificationService,
    IRefDataService refDataService,
    IRandomiser randomiser)
    : IEmailCampaignService
{
    public async Task SendCampaignAsync(Domain.Entities.EmailCampaign campaign, CancellationToken cancellationToken = default)
    {
        try
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
                                            .Where(fc => fc.Id == campaign.FilterCriteriaId).FirstOrDefault();
            if (dbFilter != null)
            {
                var filter = FilterMapper.MapToFilterModel(dbFilter);
                var volunteers = await filterService.FilterVolunteersAsync(filter, cancellationToken);

                List<Participant> finalVolunteers = new List<Participant>();

                if (campaign.TargetGroupSize != null && volunteers.Count() > 0)
                {
                    List<string?> emailAddresses = new List<string?>();

                    var volunteersToRandomise = volunteers.Where(v => !String.IsNullOrEmpty(v.Email));
                    var randomisedVolunteers = randomiser.GetRandomisedCollection(volunteersToRandomise, campaign.TargetGroupSize.Value);
                    emailAddresses = randomisedVolunteers.Select(v => v.Email).ToList();
                    finalVolunteers = randomisedVolunteers.ToList();

                    await notificationService.SendBatchEmailAsync(new SendBatchEmailRequest
                    {
                        EmailAddresses = emailAddresses,
                        EmailTemplateId = campaign.EmailTemplateId,
                    }, cancellationToken);

                    var studyId = context.FilterCriterias.Where(fc => fc.Id == campaign.FilterCriteriaId).Select(fc => fc.StudyId).FirstOrDefault();
                    var isStudyRecruitingIdentifiable = context.Studies.Where(s => s.Id == studyId).Select(s => s.IsRecruitingIdentifiableParticipants).FirstOrDefault();

                    if (!isStudyRecruitingIdentifiable || studyId == null)
                    {
                        foreach (var volunteer in finalVolunteers)
                        {
                            if (!String.IsNullOrEmpty(volunteer.Email))
                            {
                                context.EmailCampaignParticipants.Add(new Domain.Entities.EmailCampaignParticipant
                                {
                                    EmailCampaignId = campaign.Id,
                                    ParticipantId = volunteer.Id,
                                    DeliveryStatusId = refDataService.GetEmailDeliveryStatusId(EmailDeliveryStatus.Pending),
                                    SentAt = DateTime.UtcNow,
                                    ContactEmail = volunteer.Email,
                                });
                            }
                        }
                    }
                    else if (isStudyRecruitingIdentifiable && studyId != null)
                    {
                        foreach (var volunteer in finalVolunteers)
                        {
                            if (!String.IsNullOrEmpty(volunteer.Email))
                            {
                                context.EmailCampaignParticipants.Add(new Domain.Entities.EmailCampaignParticipant
                                {
                                    EmailCampaignId = campaign.Id,
                                    ParticipantId = volunteer.Id,
                                    DeliveryStatusId = refDataService.GetEmailDeliveryStatusId(EmailDeliveryStatus.Pending),
                                    SentAt = DateTime.UtcNow,
                                    ContactEmail = volunteer.Email,
                                });

                                context.StudyParticipantEnrollment.Add(new StudyParticipantEnrollment
                                {
                                    StudyId = studyId.Value,
                                    ParticipantId = volunteer.Id,
                                    Reference = GenerateVolunteerReference()
                                });
                            }
                        }
                    }
                }
            }


            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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
