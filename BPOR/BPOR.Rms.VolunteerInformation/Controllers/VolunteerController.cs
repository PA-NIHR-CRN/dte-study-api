using BPOR.Domain.Entities;
using BPOR.Infrastructure.Authentication;
using BPOR.Rms.VolunteerInformation.Models.Volunteer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Participant = BPOR.Rms.VolunteerInformation.Models.Volunteer.Participant;
using Study = BPOR.Rms.VolunteerInformation.Models.Volunteer.Study;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.DefaultScheme)]
public class VolunteerController() : ControllerBase
{
    [HttpGet("generatetesttoken/{participantId:long}")]
    public async Task<ActionResult<GetTestTokenResponse>> GetTestTokenAsync(        
        [FromServices] ParticipantDbContext context, 
        long campaignParticipantId,
        CancellationToken cancellationToken)
    {
        var campaignParticipant = await context.CampaignParticipant.SingleOrDefaultAsync(
            cp => cp.Id == campaignParticipantId, cancellationToken);

        if (campaignParticipant == null)
        {
            return NotFound();
        }

        if (campaignParticipant.Token == null)
        {
            campaignParticipant.Token = UniqueTokenGenerator.CreateUniqueToken(campaignParticipantId);
            await context.SaveChangesAsync(cancellationToken);
        }
        
        return Ok(new GetTestTokenResponse
        {
            Token = campaignParticipant.Token
        });
    }
    
    [HttpGet("information/{token}")]
    public async Task<ActionResult<GetInformationResponse>> GetInformation(
        [FromServices] ParticipantDbContext context, 
        [FromServices] IRrvTokenGenerator rrvTokenGenerator, 
        string token,
        CancellationToken cancellationToken)
    {
        var result = await context.CampaignParticipant
            .Where(cp => cp.Token == token)
            .Select (i => new GetInformationResponse()
            {
                CampaignParticipantId = i.Id,
                Participant = new Participant()
                {
                    Email = i.Participant.Email,
                    Id = i.ParticipantId,
                    Name = i.Participant.FirstName + " " + i.Participant.LastName
                },
                Study = new Study()
                {
                    PrescreenerId = i.Campaign.FilterCriteria.Study.PreScreenerUrl
                }
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
}