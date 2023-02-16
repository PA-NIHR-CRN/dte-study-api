using System.Threading.Tasks;
using Application.Extensions;
using Application.Models.Researchers;
using Application.Studies.V1.Commands.ResearcherStudies;
using Application.Studies.V1.Commands.Studies;
using Application.Studies.V1.Commands.StudyRegistrations;
using Application.Studies.V1.Queries.StudyRegistrations;
using Domain.Entities.Studies;
using Domain.Entities.StudyRegistrations;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudyApi.Attributes;
using StudyApi.Common;
using StudyApi.Requests.StudyRegistrations;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApi.Controllers.V1.Studies
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/studyregistrations")]
    [AuthorizeResearcher]
    public class StudyRegistrationsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StudyRegistrationsController> _logger;

        public StudyRegistrationsController(IMediator mediator, ILogger<StudyRegistrationsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // TODO - need attribute that will check against ResearcherStudy table and  check user is authorised to do action
       /* [HttpGet("{studyId:long}")]
        public async Task<IActionResult> GetStudyRegistration(long studyId)
        {
            return Ok(await _mediator.Send(new GetStudyRegistrationQuery(studyId)));
        }
        */
        // TODO - need attribute that will check against ResearcherStudy table and  check user is authorised to do action
       /* [HttpGet("status/{status}")]
        public async Task<IActionResult> GetStudyRegistrationsByStatus(StudyRegistrationStatus status)
        {
            return Ok(await _mediator.Send(new GetStudyRegistrationsByStatusQuery(status)));
        }
        */
        /// <summary>
        /// [AuthorizeResearcher] Create a Study Registration
        /// </summary>
        /// <remarks>Create a Study Registration</remarks>
        /// <response code="200">Study Registration created when IsSuccess true</response>
        /// <response code="500">Server side error</response>
        /*[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPost]
        public async Task<IActionResult> CreateStudyRegistration([FromBody]CreateStudyRegistrationRequest request)
        {
            var userId = User.GetUserIdCognito();
            
            var command = new CreateStudyRegistrationCommand
            (
                request.StudyId,
                request.Title,
                new ResearcherModel
                {
                    Id = userId,
                    Firstname = request.Researcher.Firstname,
                    Lastname = request.Researcher.Lastname,
                    Email = request.Researcher.Email
                }
            );

            return Ok(await _mediator.Send(command));
        }
        */
        /// <summary>
        /// [AuthorizeResearcher] Approve a Study Registration
        /// </summary>
        /// <remarks>Approve a Study Registration only by a DTE Admin</remarks>
        /// <response code="200">Study Registration approved when IsSuccess true</response>
        /// <response code="500">Server side error</response>
        /*[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [Authorize(AppRoles.Admin)]
        [HttpPost("{studyId:long}/approve")]
        public async Task<IActionResult> ApproveStudyRegistration(long studyId, [FromBody] ApproveStudyRegistrationRequest request)
        {
            return Ok(await _mediator.Send(new ApproveStudyRegistrationCommand(studyId, request.Title, request.CpmsId, request.IsrctnId, User.GetUserIdCognito())));
        }
        */
        /// <summary>
        /// [AuthorizeResearcher] Reject a Study Registration
        /// </summary>
        /// <remarks>Reject a Study Registration only by a DTE Admin</remarks>
        /// <response code="200">Study Registration rejected when IsSuccess true</response>
        /// <response code="500">Server side error</response>
       /* [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [Authorize(AppRoles.Admin)]
        [HttpPost("{studyId:long}/reject")]
        public async Task<IActionResult> RejectStudyRegistration(long studyId)
        {
            return Ok(await _mediator.Send(new RejectStudyRegistrationCommand(studyId, User.GetUserIdCognito())));
        }
        */
        /// <summary>
        /// [AuthorizeResearcher] Start a review for a Study Registration, status changes to WaitingForApproval
        /// </summary>
        /// <remarks>Start a review for a Study Registration only by a DTE Admin</remarks>
        /// <response code="200">When IsSuccess true</response>
        /// <response code="500">Server side error</response>
       /* [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [Authorize(AppRoles.Admin)]
        [HttpPost("{studyId:long}/startreview")]
        public async Task<IActionResult> StartReviewStudyRegistration(long studyId)
        {
            return Ok(await _mediator.Send(new StartReviewStudyRegistrationCommand(studyId, User.GetUserIdCognito())));
        }
       */
    }
}