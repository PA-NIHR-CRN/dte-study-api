using BPOR.Infrastructure.Interfaces;
using BPOR.Infrastructure.Mappers;
using BPOR.Infrastructure.Responses.V1.Participants;
using BPOR.Registration.Api.Extensions;
using BPOR.Registration.Api.Mappers;
using BPOR.Registration.Api.Requests.Participants;
using Dte.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BPOR.Registration.Api.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/participants")]
[Authorize("AnyAuthenticatedUser")]
public class ParticipantController(IParticipantService participantService) : Controller
{
    /// <summary>
    /// [Authorize("AnyAuthenticatedUser")] Get own participant details only
    /// </summary>
    /// <response code="200">Participant details retrieved</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ParticipantResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpGet("details")]
    public async Task<IActionResult> GetParticipant(CancellationToken cancellationToken)
    {
        var participantId = User.GetParticipantId();
        if (string.IsNullOrWhiteSpace(participantId))
        {
            return Unauthorized();
        }

        return Ok(await participantService.GetParticipantAsync(participantId, cancellationToken));
    }


    /// <summary>
    /// [Authorize("AnyAuthenticatedUser")] Update participant details for the logged in participant
    /// </summary>
    /// <response code="200">Participant details updated</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPut("details")]
    public async Task<IActionResult> UpdateParticipant([FromBody] UpdateParticipantRequest request, CancellationToken cancellationToken)
    {
        var participant = await participantService.GetParticipantAsync(User.GetParticipantId(), cancellationToken);
        if (participant == null)
        {
            return NotFound();
        }

        participant = request.MapTo(participant);
        return Ok(await participantService.UpdateParticipantAsync(participant, cancellationToken));
    }

    /// <summary>
    /// [Authorize("AnyAuthenticatedUser")] Update participant selected locale for the logged in participant
    /// </summary>
    /// <response code="200">Participant details updated</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPut("selectedlocale")]
    public async Task<IActionResult> UpdateParticipantSelectedLocale(
        [FromBody] UpdateParticipantSelectedLocaleRequest request, CancellationToken cancellationToken)
    {
        var participant = await participantService.GetParticipantAsync(User.GetParticipantId(), cancellationToken);
        if (participant == null)
        {
            return NotFound();
        }
        
        participant.SelectedLocale = request.SelectedLocale;
        return Ok(await participantService.UpdateParticipantAsync(participant, cancellationToken));
    }
    
    /// <summary>
    /// [AnyAuthenticatedUser] Delete own user account
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [Authorize("AnyAuthenticatedUser")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpDelete("deleteparticipantaccount")]
    public async Task<IActionResult> DeleteParticipantAccount(CancellationToken cancellationToken)
    {
        var participantId = User.GetParticipantId();
        if (string.IsNullOrWhiteSpace(participantId))
        {
            return Unauthorized();
        }

        await participantService.DeleteParticipantAsync(participantId, cancellationToken);
        return Ok();
    }
}
