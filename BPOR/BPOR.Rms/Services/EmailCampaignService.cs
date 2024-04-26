using BPOR.Domain.Entities;
using BPOR.Infrastructure.Interfaces;

namespace BPOR.Rms.Services;

public class EmailCampaignService : IEmailCampaignService
{
    private readonly ParticipantDbContext _context;
    private readonly IFilterService _filterService;

    public EmailCampaignService(ParticipantDbContext context, IFilterService filterService)
    {
        _context = context;
        _filterService = filterService;
    }

    public async Task SendCampaignAsync(EmailCampaign campaign)
    {
        try
        {
            var dbFilter = await _context.FilterCriterias.FindAsync(campaign.FilterCriteriaId);
            //
            //
            // var participants = await _filterService.FilterVolunteersAsync(filter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
        //
        // if (filter == null)
        // {
        //     throw new ArgumentException("Filter not found");
        // }

        // use the filter criteria to search for participants
        // var users = await _context.Participants
        //     .Where(sr => sr.Study.FilterCriterias.Any(fc => fc.Id == FilterCriteriaId))
        //     .Select(sr => sr.User)
        //     .ToListAsync();
        // foreach (var user in users)
        // {
        //     var email = new Email
        //     {
        //         To = user.ContactEmail,
        //         Subject = filter.Name,
        //         Body = filter.Description
        //     };
        //
        //     await _emailService.SendEmailAsync(email);
        // }
    }
}
