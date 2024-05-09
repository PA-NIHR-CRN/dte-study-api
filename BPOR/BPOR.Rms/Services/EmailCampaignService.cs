using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using BPOR.Rms.Mappers;
using BPOR.Rms.Models.Study;
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
            var dbFilter = await context.FilterCriterias.FindAsync(campaign.FilterCriteriaId, cancellationToken);
            if (dbFilter != null)
            {
                var filter = FilterMapper.MapToFilterModel(dbFilter);
                var volunteers = await filterService.FilterVolunteersAsync(filter, cancellationToken);
                var participantIds = volunteers.Select(v => v.Id).ToList();
                List<Participant> finalVolunteers = new List<Participant>();

                if (campaign.TargetGroupSize != null)
                {
                    List<string?> emailAddresses = new List<string?>();

                    var participantsNotContacted = participantIds.Where(participantId =>
                                                        !context.EmailCampaignParticipants.Any(ecp =>
                                                            ecp.ParticipantId == participantId &&
                                                            context.EmailCampaigns.Any(ec =>
                                                                ec.Id == ecp.EmailCampaignId &&
                                                                context.FilterCriterias.Any(fc =>
                                                                    fc.Id == ec.FilterCriteriaId &&
                                                                    fc.StudyId != null))));

                    // Priortise those who have not been contacted, randomise the remaining volunteers
                    if (participantsNotContacted.Count() < campaign.TargetGroupSize.Value)
                    {
                        var prioritisedVolunteers = volunteers.Where(v => participantsNotContacted.Contains(v.Id));
                        var volunteersToRandomise = volunteers.Where(v => !participantsNotContacted.Contains(v.Id));
                        var randomisedVolunteers = randomiser.GetRandomisedCollection(volunteersToRandomise, campaign.TargetGroupSize.Value - prioritisedVolunteers.Count());
                        var selectedVolunteers = randomisedVolunteers.Concat(prioritisedVolunteers);
                        emailAddresses = selectedVolunteers.Select(v => v.Email).ToList();
                        finalVolunteers = selectedVolunteers.ToList();
                    }
                    // If total participants not contacted exceeds target group size, randomise particpants not contacted based on target group size
                    else if (participantsNotContacted.Count() > campaign.TargetGroupSize.Value)
                    {
                        var prioritisedVolunteers = volunteers.Where(v => participantsNotContacted.Contains(v.Id));
                        var selectedVolunteers = randomiser.GetRandomisedCollection(prioritisedVolunteers, campaign.TargetGroupSize.Value);
                        emailAddresses = selectedVolunteers.Select(v => v.Email).ToList();
                        finalVolunteers = selectedVolunteers.ToList();
                    }
                    // If total participants not contacted matches target group size, send email to those not contacted and do not randomise
                    else if (participantsNotContacted.Count() == campaign.TargetGroupSize.Value)
                    {
                        var selectedVolunteers = volunteers.Where(v => participantsNotContacted.Contains(v.Id));
                        emailAddresses = selectedVolunteers.Select(v => v.Email).ToList();
                        finalVolunteers = selectedVolunteers.ToList();
                    }

                    await notificationService.SendBatchEmailAsync(new SendBatchEmailRequest
                    {
                        EmailAddresses = emailAddresses,
                        EmailTemplateId = campaign.EmailTemplateId,
                    }, cancellationToken);

                    var studyId = context.FilterCriterias.Where(fc => fc.Id == campaign.FilterCriteriaId).Select(fc => fc.StudyId).FirstOrDefault();
                    var isStudyRecruitingIdentifiable = context.Studies.Where(s => s.Id == studyId).Select(s => s.IsRecruitingIdentifiableParticipants).FirstOrDefault();

                    if (!isStudyRecruitingIdentifiable)
                    {
                        foreach (var volunteer in finalVolunteers)
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
                    else if (isStudyRecruitingIdentifiable && studyId != null)
                    {
                        foreach (var volunteer in finalVolunteers)
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
                                Reference = "1234" // TODO - generate ref number using algorithm

                            });
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
}
