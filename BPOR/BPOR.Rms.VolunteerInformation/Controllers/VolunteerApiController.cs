using BPOR.Domain.Entities;
using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.Abstractions.Models;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;
using Participant = BPOR.Rms.Abstractions.Models.Participant;
using Study = BPOR.Rms.Abstractions.Models.Study;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[ApiController]
[Route("Volunteer")]
[ApiKeyAuthentication]
public class VolunteerController : ControllerBase
{
    [Authorize(Roles = Roles.RoleRrvPrescreener)]
    [HttpGet("generatetesttoken/{campaignParticipantId:long}")]
    public async Task<ActionResult<GetTestTokenResponse>> GetTestTokenAsync(       
        [FromServices] ParticipantDbContext db,
        [FromServices] IVipTokenGenerator vipTokenGenerator, 
        long campaignParticipantId,
        CancellationToken cancellationToken)
    {
        var campaignParticipant = await db.CampaignParticipant
            .Include(i => i.Campaign).ThenInclude(i => i.FilterCriteria)
            .SingleOrDefaultAsync(i => i.Id == campaignParticipantId, cancellationToken);

        if (campaignParticipant == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(campaignParticipant.Token))
        {
            campaignParticipant.Token = vipTokenGenerator.GenerateToken(new VipToken(VipTokenPurpose.Volunteer,
                campaignParticipant.CampaignId, campaignParticipant.ParticipantId,
                campaignParticipant.Campaign.FilterCriteria.StudyId.Value));
            await db.SaveChangesAsync(cancellationToken);
        }
        
        return Ok(new GetTestTokenResponse
        {
            Token = campaignParticipant.Token
        });
    }
    

    [Authorize(Roles = Roles.RoleBporContent)]
    [HttpGet("trackevent/{token}")]
    public async Task<IActionResult> TrackEvent(
        [FromServices] ICampaignParticipantRepository repository, 
        [FromServices] IVipTokenGenerator tokenGenerator,
        string token,
        CampaignParticipantEventType eventType,
        CancellationToken cancellationToken)
    {
        var validatedToken = await tokenGenerator.ValidateToken(token, cancellationToken);
        if (validatedToken == null)
        {
            return Unauthorized();
        }
        
        if (validatedToken.Purpose == VipTokenPurpose.Volunteer)
        {
            await repository.TrackEvent(validatedToken.CampaignId, validatedToken.ParticipantId,
                eventType, cancellationToken);
        }

        return Ok();
    }
    
    [Authorize(Roles = Roles.RoleBporContent)]
    [HttpGet("informationpage/{token}")]
    public async Task<ActionResult<GetVolunteerInformationPageResponse>> GetInformationPage(
        [FromServices] ParticipantDbContext context, 
        [FromServices] IStudyRepository studyRepository,
        [FromServices] IVipRepository repository,
        [FromServices] IVipTokenGenerator tokenGenerator,
        string token,
        CancellationToken cancellationToken)
    {
        var validatedToken = await tokenGenerator.ValidateToken(token, cancellationToken);
        if (validatedToken == null)
        {
            return Unauthorized();
        }
        
        VolunteerInformationAudience audience;
        
        switch (validatedToken.Purpose)
        {
            case VipTokenPurpose.Volunteer:
                audience = VolunteerInformationAudience.Volunteer;
                break;
            case VipTokenPurpose.ResearcherPreview:
                audience = VolunteerInformationAudience.Researcher;
                break;
            case VipTokenPurpose.AdminPreview:
                audience = VolunteerInformationAudience.Admin;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        var study = await studyRepository.GetStudy(validatedToken.StudyId, cancellationToken);
        if (study == null)
        {
            return NotFound();
        }

        var vsiPage = await repository.GetPage(validatedToken.StudyId, cancellationToken);
        if (vsiPage == null)
        {
            return NotFound();
        }
        
        string? fullPrescreenerUrl;
        if (vsiPage.PreScreenerUrl == null)
        {
            fullPrescreenerUrl = null;
        }
        else
        {
            fullPrescreenerUrl = validatedToken.Purpose switch
            {
                VipTokenPurpose.Volunteer => QueryHelpers.AddQueryString(vsiPage.PreScreenerUrl, new Dictionary<string, string>()
                                                                {
                                                                    ["token"] = token
                                                                }!),
                VipTokenPurpose.ResearcherPreview => "#",
                VipTokenPurpose.AdminPreview => "#",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        GetVolunteerInformationPageResponse response = new()
        {
            Audience = audience,
            VolunteerInformation = vsiPage,
            StudyName = study.StudyName,
            FullPrescreenerLink = fullPrescreenerUrl
        };

        return new JsonResult(response);
    }

    [Authorize(Roles = Roles.RoleRrvPrescreener)]
    [HttpGet("information/{token}")]
    public async Task<ActionResult<GetInformationResponse>> GetInformation(
        [FromServices] ParticipantDbContext context, 
        [FromServices] IVipTokenGenerator vipTokenGenerator, 
        string token,
        CancellationToken cancellationToken)
    {
        var validatedToken = await vipTokenGenerator.ValidateToken(token, cancellationToken);
        if (validatedToken is not { Purpose: VipTokenPurpose.Volunteer })
        {
            return Unauthorized();
        }

        var result = await context.CampaignParticipant
            .Where(cp => cp.CampaignId == validatedToken.CampaignId && cp.ParticipantId == validatedToken.ParticipantId)
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