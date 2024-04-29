using BPOR.Domain.Entities;
using BPOR.Domain.Entities.RefData;
using BPOR.Infrastructure.Interfaces;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Mappers;
using BPOR.Rms.Models.Filter;
using NIHR.Infrastructure.EntityFrameworkCore;
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
            // Retrieve filter criteria and apply it
            var dbFilter = await context.FilterCriterias.FindAsync(campaign.FilterCriteriaId);
            var filter = FilterMapper.MapToFilterModel(dbFilter);
            var volunteers = await filterService.FilterVolunteersAsync(filter, cancellationToken);

            // TODO implement this logic
            // prioritise volunteers who have not been contacted before or not contacted in a while
            var prioritisedVolunteers = volunteers;

            // Randomise the order of the volunteers
            var selectedVolunteers = randomiser.GetRandomisedCollection(prioritisedVolunteers, campaign.TargetGroupSize.Value);

            var emailAddresses = selectedVolunteers.Select(v => v.Email).ToList();

            // Send emails and update records
            foreach (var volunteer in selectedVolunteers)
            {
                await notificationService.SendBatchEmailAsync(new SendBatchEmailRequest
                {
                    EmailAddresses = emailAddresses,
                    EmailTemplateId = campaign.EmailTemplateId,
                }, cancellationToken);
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
