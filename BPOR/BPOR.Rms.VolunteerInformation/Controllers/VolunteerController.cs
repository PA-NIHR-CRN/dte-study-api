using BPOR.Domain.Entities;
using BPOR.Infrastructure.Authentication;
using BPOR.Rms.VolunteerInformation.Models.Volunteer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Participant = BPOR.Rms.VolunteerInformation.Models.Volunteer.Participant;
using Study = BPOR.Rms.VolunteerInformation.Models.Volunteer.Study;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.DefaultScheme)]
public class VolunteerController(IOptions<RrvTokenOptions> options) : ControllerBase
{
    private const int staticCheckValue = 155156040;

    [HttpGet("generatetesttoken/{participantId:long}")]
    public ActionResult<GetTestTokenResponse> GetTestTokenAsync(        
        [FromServices] IRrvTokenGenerator rrvTokenGenerator, 
        long participantId)
    {
        if (!options.Value.EnableRrvApi)
        {
            return NotFound();
        }
        
        return Ok(new GetTestTokenResponse
        {
            Token = new Guid(staticCheckValue, 0, 0, BitConverter.GetBytes(participantId)).ToString("N")
        });
    }
    
    [HttpGet("information/{token}")]
    public async Task<ActionResult<GetInformationResponse>> GetInformation(
        [FromServices] ParticipantDbContext context, 
        [FromServices] IRrvTokenGenerator rrvTokenGenerator, 
        string token,
        CancellationToken cancellationToken)
    {
        if (!options.Value.EnableRrvApi)
        {
            return NotFound();
        }
        
        long campaignParticipantId;
        
        if (Guid.TryParse(token, out var guid))
        {
            var bytes = guid.ToByteArray();
            int check = BitConverter.ToInt32(bytes, 0);
            if (check != staticCheckValue)
            {
                return BadRequest("Invalid static Token");
            }
            campaignParticipantId = BitConverter.ToInt64(bytes, 8);
        }
        else
        {
            // TODO: Include error detail?
            return BadRequest();
        }

        var result = await context.CampaignParticipant
            .Where(cp => cp.Id == campaignParticipantId)
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
                    StudyId = (long)i.Campaign.FilterCriteria.StudyId, // TODO: Why would this ever be null?
                    PrescreenerUrl = i.Campaign.FilterCriteria.Study.PreScreenerUrl
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