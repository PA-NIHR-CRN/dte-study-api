using BPOR.Domain.Entities;
using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.Abstractions.Models;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Settings;
using BPOR.Rms.VolunteerInformation.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;
using Participant = BPOR.Rms.Abstractions.Models.Participant;
using Study = BPOR.Rms.Abstractions.Models.Study;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[ApiController]
[Route("Volunteer")]
[ApiKeyAuthentication]
public class VolunteerController(IOptions<VipSettings> options) : ControllerBase
{
    private const string RoleRrvPrescreener = "RrvPrescreener";
    private const string RoleBporContent = "BporContent";
    
    // Fixed IV for RRV testing.
    private readonly byte[] testIv = [0xae, 0x64, 0xa4, 0x66, 0x24, 0x34, 0x0e, 0x59, 0x35, 0xb7, 0xbb, 0x6c, 0x62, 0x19, 0x42, 0xa5];

    [Authorize(Roles = RoleRrvPrescreener)]
    [HttpGet("generatetesttoken/{campaignParticipantId:long}")]
    public async Task<ActionResult<GetTestTokenResponse>> GetTestTokenAsync(       
        [FromServices] ParticipantDbContext db,
        [FromServices] IVipTokenGenerator vipTokenGenerator, 
        long campaignParticipantId,
        CancellationToken cancellationToken)
    {
        if (!options.Value.EnableRrvApi)
        {
            return NotFound();
        }

        var campaignParticipant = await db.CampaignParticipant.SingleOrDefaultAsync(
            i => i.Id == campaignParticipantId, cancellationToken);

        if (campaignParticipant == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(campaignParticipant.Token))
        {
            campaignParticipant.Token = vipTokenGenerator.GenerateVolunteerToken();
            await db.SaveChangesAsync(cancellationToken);
        }
        
        return Ok(new GetTestTokenResponse
        {
            Token = campaignParticipant.Token
        });
    }
    

    [Authorize(Roles = RoleBporContent)]
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

        int? studyId;
        VolunteerInformationAudience audience;
        
        switch (validatedToken.Purpose)
        {
            case VipTokenPurpose.Volunteer:
                audience = VolunteerInformationAudience.Volunteer;
                studyId = await context.CampaignParticipant
                    .Where(cp => cp.Id == validatedToken.Id)
                    .Select(i => i.Campaign.FilterCriteria.StudyId)
                    .SingleOrDefaultAsync(cancellationToken);
                break;
            case VipTokenPurpose.ResearcherPreview:
                audience = VolunteerInformationAudience.Researcher;
                studyId = (int)validatedToken.Id;
                break;
            case VipTokenPurpose.AdminPreview:
                audience = VolunteerInformationAudience.Admin;
                studyId = (int)validatedToken.Id;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (studyId == null)
        {
            return NotFound();
        }

        var study = await studyRepository.GetStudy(studyId.Value, cancellationToken);
        if (study == null)
        {
            return NotFound();
        }

        var vsiPage = await repository.GetPage(studyId.Value, cancellationToken);
        if (vsiPage == null)
        {
            return NotFound();
        }
        
        string? fullPrescreenerUrl;
        if (study.PreScreenerUrl == null)
        {
            fullPrescreenerUrl = null;
        }
        else
        {
            fullPrescreenerUrl = validatedToken.Purpose switch
            {
                VipTokenPurpose.Volunteer => QueryHelpers.AddQueryString(study.PreScreenerUrl, new Dictionary<string, string>()
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

    [Authorize(Roles = RoleRrvPrescreener)]
    [HttpGet("information/{token}")]
    public async Task<ActionResult<GetInformationResponse>> GetInformation(
        [FromServices] ParticipantDbContext context, 
        [FromServices] IVipTokenGenerator vipTokenGenerator, 
        string token,
        CancellationToken cancellationToken)
    {
        if (!options.Value.EnableRrvApi)
        {
            return NotFound();
        }

        var validatedToken = await vipTokenGenerator.ValidateToken(token, cancellationToken);
        if (validatedToken is not { Purpose: VipTokenPurpose.Volunteer })
        {
            return Unauthorized();
        }

        var result = await context.CampaignParticipant
            .Where(cp => cp.Id == validatedToken.Id)
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