using BPOR.Rms.Api.Models.Volunteer;
using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteerController : ControllerBase
{
    [HttpGet("information/{token}")]
    public async Task<ActionResult<GetInformationResponse>> GetInformation(string token, CancellationToken cancellationToken)
    {
        var result = new GetInformationResponse
        {
            ParticipantEmail = "test@test-domain.test",
            ParticipantId = 123,
            ParticipantName = "Alice Smith"
        };
        
        return Ok(result);
    }
}