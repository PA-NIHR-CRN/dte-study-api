using BPOR.Domain.Entities;
using BPOR.Infrastructure.Authentication;
using BPOR.Rms.Api.Models.Volunteer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.DefaultScheme)]
public class VolunteerController(ParticipantDbContext context, IRrvTokenGenerator rrvTokenGenerator) : ControllerBase
{
    private const int staticCheckValue = 155156040;

    [HttpGet("generatetesttoken/{participantId:long}")]
    public ActionResult<GetTestTokenResponse> GetTestTokenAsync(long participantId, bool staticToken = false)
    {
        if (staticToken)
        {
            return Ok(new GetTestTokenResponse
            {
                Token = new Guid(staticCheckValue, 0, 0, BitConverter.GetBytes(participantId)).ToString("N")
            });
        }
        
        var token = rrvTokenGenerator.GenerateToken(participantId);
        return Ok(new GetTestTokenResponse
        {
            Token = token
        });
    }
    
    [HttpGet("information/{token}")]
    public async Task<ActionResult<GetInformationResponse>> GetInformation(string token, CancellationToken cancellationToken)
    {
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
            if (!rrvTokenGenerator.TryValidateToken(token, out campaignParticipantId))
            {
                // TODO: Include error detail?
                return BadRequest();
            }
        }

        var result = await context.CampaignParticipant
            .Where(cp => cp.Id == campaignParticipantId)
            .Select (i => new GetInformationResponse()
            {
                CampaignParticipantId = i.Id,
                ParticipantEmail = i.Participant.Email,
                ParticipantId = i.ParticipantId,
                ParticipantName = i.Participant.FirstName + " " + i.Participant.LastName,
                StudyId = (long)i.Campaign.FilterCriteria.StudyId // TODO: Why would this ever be null?
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
}