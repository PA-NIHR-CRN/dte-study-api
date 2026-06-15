using BPOR.Domain.Entities;
using BPOR.Rms.Abstractions.Models;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Models.Volunteer;
using BPOR.Rms.VolunteerInformation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;
using Participant = BPOR.Rms.VolunteerInformation.Models.Volunteer.Participant;
using Study = BPOR.Rms.VolunteerInformation.Models.Volunteer.Study;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[ApiController]
[Route("[controller]")]
[ApiKeyAuthentication]
public class VolunteerController(IOptions<RrvTokenOptions> options) : ControllerBase
{
    private const int staticCheckValue = 155156040;
    private const string RoleRrvPrescreener = "RrvPrescreener";
    private const string RoleBporContent = "BporContent";

    [Authorize(Roles = RoleRrvPrescreener)]
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

    [Authorize(Roles = RoleBporContent)]
    [HttpGet("informationpage/{token}")]
    public async Task<ActionResult<GetVolunteerInformationPageResponse>> GetInformationPage(
        [FromServices] IVsiRepository repository,
        [FromServices] InternalVipTokenService tokenService,
        string token,
        CancellationToken cancellationToken)
    {
        if (!tokenService.TryValidateVipAccessToken(token, out var validationResult))
        {
            return BadRequest();
        }

        var vsiPage = await repository.GetPage(validationResult.studyId, cancellationToken);
        if (vsiPage == null)
        {
            return NotFound();
        }
        
        GetVolunteerInformationPageResponse response = new()
        {
            Audience = validationResult.audience,
            VolunteerInformation = vsiPage
        };

        return new JsonResult(response);
    }

    [Authorize(Roles = RoleRrvPrescreener)]
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