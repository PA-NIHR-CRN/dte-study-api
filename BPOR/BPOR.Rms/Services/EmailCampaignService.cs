using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using BPOR.Rms.Mappers;
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
    public async Task SendCampaignAsync(EmailCampaign campaign, CancellationToken cancellationToken = default)
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

                // TODO implement this logic
                // prioritise volunteers who have not been contacted before or not contacted in a while
                var prioritisedVolunteers = volunteers;

                // Randomise the order of the volunteers
                if (campaign.TargetGroupSize != null)
                {
                    var selectedVolunteers =
                        randomiser.GetRandomisedCollection(prioritisedVolunteers, campaign.TargetGroupSize.Value);

                    var emailAddresses = selectedVolunteers.Select(v => v.Email).ToList();

                    await notificationService.SendBatchEmailAsync(new SendBatchEmailRequest
                    {
                        EmailAddresses = emailAddresses,
                        EmailTemplateId = campaign.EmailTemplateId,
                    }, cancellationToken);
            
                    var sentAt = DateTime.UtcNow;

                    foreach (var volunteer in selectedVolunteers)
                    {
                        context.EmailCampaignParticipants.Add(new EmailCampaignParticipant
                        {
                            EmailCampaignId = campaign.Id,
                            ParticipantId = volunteer.Id,
                            DeliveryStatusId = refDataService.GetEmailDeliveryStatusId(EmailDeliveryStatus.Pending),
                            SentAt = sentAt,
                            ContactEmail = volunteer.Email,
                        });
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
