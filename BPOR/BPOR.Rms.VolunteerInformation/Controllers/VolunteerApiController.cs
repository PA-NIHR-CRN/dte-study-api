using BPOR.Domain.Entities;
using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.Abstractions.Models;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Models.Volunteer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;
using Participant = BPOR.Rms.VolunteerInformation.Models.Volunteer.Participant;
using Study = BPOR.Rms.VolunteerInformation.Models.Volunteer.Study;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[ApiController]
[Route("Volunteer")]
[ApiKeyAuthentication]
public class VolunteerController(IOptions<RrvTokenOptions> options) : ControllerBase
{
    private const string RoleRrvPrescreener = "RrvPrescreener";
    private const string RoleBporContent = "BporContent";
    
    // Fixed IV for RRV testing.
    private readonly byte[] testIv = [0xae, 0x64, 0xa4, 0x66, 0x24, 0x34, 0x0e, 0x59, 0x35, 0xb7, 0xbb, 0x6c, 0x62, 0x19, 0x42, 0xa5];

    [Authorize(Roles = RoleRrvPrescreener)]
    [HttpGet("generatetesttoken/{participantId:long}")]
    public ActionResult<GetTestTokenResponse> GetTestTokenAsync(        
        [FromServices] IVipTokenGenerator vipTokenGenerator, 
        long participantId)
    {
        if (!options.Value.EnableRrvApi)
        {
            return NotFound();
        }
        
        return Ok(new GetTestTokenResponse
        {
            Token = vipTokenGenerator.GenerateToken(VipTokenPurpose.Volunteer, participantId, testIv),
        });
    }

    [Authorize(Roles = RoleBporContent)]
    [HttpGet("informationpage/{token}")]
    public async Task<ActionResult<GetVolunteerInformationPageResponse>> GetInformationPage(
        [FromServices] ParticipantDbContext context, 
        [FromServices] IStudyRepository studyRepository,
        [FromServices] IVsiRepository repository,
        [FromServices] IVipTokenGenerator tokenGenerator,
        string token,
        CancellationToken cancellationToken)
    {
        if (!tokenGenerator.TryValidateToken(token, out var validatedToken))
        {
            return Unauthorized();
        }

        int? studyId;
        VolunteerInformationAudience audience;
        
        switch (validatedToken.purpose)
        {
            case VipTokenPurpose.Volunteer:
                audience = VolunteerInformationAudience.Volunteer;
                studyId = await context.CampaignParticipant
                    .Where(cp => cp.Id == validatedToken.Id)
                    .Select(i => i.Campaign.FilterCriteria.StudyId)
                    .SingleOrDefaultAsync(cancellationToken);
                break;
            case VipTokenPurpose.Researcher:
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

        var fullPrescreenerUrl = study.PreScreenerUrl == null
            ? null
            : QueryHelpers.AddQueryString(study.PreScreenerUrl, new Dictionary<string, string>()
            {
                ["token"] = token
            }!);
        
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
        
        if (!vipTokenGenerator.TryValidateToken(token, out var validatedToken) || validatedToken.purpose != VipTokenPurpose.Volunteer)
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