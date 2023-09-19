using System.Threading.Tasks;
using Application.Extensions;
using Application.Mappings.Participants;
using Application.Participants.V1.Commands.Participants;
using Application.Participants.V1.Queries.Participants;
using Application.Responses.V1.Participants;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Mappers.Participants;
using StudyApi.Requests.Participants;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApi.Controllers.V1.Participants
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/participants")]
    [Authorize("AnyAuthenticatedUser")]
    public class ParticipantsController : Controller
    {
        private readonly IMediator _mediator;

        public ParticipantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// [Authorize("AnyAuthenticatedUser")] Get own participant details only
        /// </summary>
        /// <response code="200">Participant details retrieved</response>
        /// <response code="500">Server side error</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ParticipantDetailsResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("details")]
        public async Task<IActionResult> GetParticipantDetails()
        {
            var participantId = User.GetParticipantId();
            if (string.IsNullOrWhiteSpace(participantId))
            {
                return Unauthorized();
            }

            return Ok(await _mediator.Send(new GetParticipantDetailsQuery(participantId)));
        }


        /// <summary>
        /// [Authorize("AnyAuthenticatedUser")] Update participant details for the logged in participant
        /// </summary>
        /// <response code="200">Participant details updated</response>
        /// <response code="500">Server side error</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPut("details")]
        public async Task<IActionResult> UpdateParticipantDetails([FromBody] UpdateParticipantDetailsRequest request)
        {
            var command = new UpdateParticipantDetailsCommand
            (
                User.GetParticipantId(),
                request.Firstname,
                request.Lastname
            );

            return Ok(await _mediator.Send(command));
        }
        
        /// <summary>
        /// [Authorize("AnyAuthenticatedUser")] Update participant selected locale for the logged in participant
        /// </summary>
        /// <response code="200">Participant details updated</response>
        /// <response code="500">Server side error</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPut("selectedlocale")]
        public async Task<IActionResult> UpdateParticipantSelectedLocale([FromBody] UpdateParticipantSelectedLocaleRequest request)
        {
            var command = new UpdateParticipantSelectedLocaleCommand
            (
                User.GetParticipantId(),
                request.SelectedLocale
            );

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// [Authorize("AnyAuthenticatedUser")] Get own participant demographics only
        /// </summary>
        /// <response code="200">Participant demographics retrieved</response>
        /// <response code="500">Server side error</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ParticipantDemographicsResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("demographics")]
        public async Task<IActionResult> GetParticipantDemographics()
        {
            var participantId = User.GetParticipantId();

            if (string.IsNullOrWhiteSpace(participantId))
            {
                return Unauthorized();
            }

            return Ok(await _mediator.Send(new GetParticipantDemographicsQuery(participantId)));
        }

        /// <summary>
        /// [Authorize("AnyAuthenticatedUser")] Create participant demographics
        /// </summary>
        /// <response code="200">Participant demographics created</response>
        /// <response code="500">Server side error</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPost("demographics")]
        public async Task<IActionResult> CreateParticipantDemographics(
            [FromBody] CreateParticipantDemographicsRequest request)
        {
            var command = new CreateParticipantDemographicsCommand
            (
                User.GetParticipantId(),
                request.MobileNumber,
                request.LandlineNumber,
                ParticipantRequestMapper.MapTo(request.Address),
                request.SexRegisteredAtBirth,
                request.GenderIsSameAsSexRegisteredAtBirth,
                request.EthnicGroup,
                request.EthnicBackground,
                request.Disability,
                request.DisabilityDescription,
                request.HealthConditionInterests
            );

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// [Authorize("AnyAuthenticatedUser")] Update participant demographics
        /// </summary>
        /// <response code="200">Participant demographics updated</response>
        /// <response code="500">Server side error</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPut("demographics")]
        public async Task<IActionResult> UpdateParticipantDemographics(
            [FromBody] UpdateParticipantDemographicsRequest request)
        {
            var command = new UpdateParticipantDemographicsCommand
            (
                User.GetParticipantId(),
                request.MobileNumber,
                request.LandlineNumber,
                ParticipantRequestMapper.MapTo(request.Address),
                request.SexRegisteredAtBirth,
                request.GenderIsSameAsSexRegisteredAtBirth,
                request.EthnicGroup,
                request.EthnicBackground,
                request.Disability,
                request.DisabilityDescription,
                request.HealthConditionInterests,
                request.DateOfBirth
            );

            return Ok(await _mediator.Send(command));
        }
    }
}