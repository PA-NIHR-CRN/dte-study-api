using System.Threading.Tasks;
using Application.Extensions;
using Application.Participants.V1.Commands.ParticipantRegistrations;
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
        [HttpGet("{participantId}/details")]
        public async Task<IActionResult> GetParticipantDetails(string participantId)
        {
            if (participantId != User.GetUserIdCognito())
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
                User.GetUserIdCognito(),
                request.Firstname,
                request.Lastname,
                request.ConsentRegistration
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
        [HttpGet("{participantId}/demographics")]
        public async Task<IActionResult> GetParticipantDemographics(string participantId)
        {
            if(participantId!= User.GetUserIdCognito())
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
        public async Task<IActionResult> CreateParticipantDemographics([FromBody] CreateParticipantDemographicsRequest request)
        {
            var command = new CreateParticipantDemographicsCommand
            (
                User.GetUserIdCognito(),
                request.MobileNumber,
                request.LandlineNumber,
                ParticipantRequestMapper.MapTo(request.Address),
                request.DateOfBirth,
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
        public async Task<IActionResult> UpdateParticipantDemographics([FromBody] UpdateParticipantDemographicsRequest request)
        {
            var command = new UpdateParticipantDemographicsCommand
            (
                User.GetUserIdCognito(),
                request.MobileNumber,
                request.LandlineNumber,
                ParticipantRequestMapper.MapTo(request.Address),
                request.DateOfBirth,
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
        /// Check if the logged in participant is suitable for this study
        /// </summary>
        /// <response code="200">Study information retrieved</response>
        /// <response code="500">Server side error</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ParticipantSuitabilityResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("suitability/{studyId:long}")]
        public async Task<IActionResult> GetParticipantSuitability(long studyId)
        {
            return Ok(await _mediator.Send(new GetParticipantSuitabilityQuery(User.GetUserIdCognito(), studyId)));
        }
        
        /// <summary>
        /// Create a participant registration for a Study Site
        /// </summary>
        /// <response code="200">Participant registration created</response>
        /// <response code="500">Server side error</response>
        /// <remarks>This endpoint will return Success if created else will return a list of errors if the participants has already applied for a study site</remarks>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPost("ParticipantRegistrations")]
        public async Task<IActionResult> CreateParticipantRegistration([FromBody]CreateParticipantRegistrationRequest request)
        {
            var command = new CreateParticipantRegistrationCommand
            (
                request.StudyId, 
                request.SiteId, 
                request.ParticipantId
            );
            
            return Ok(await _mediator.Send(command));
        }
    }
}