using System.Threading.Tasks;
using Amazon.Runtime;
using Application.Responses.V1.Studies;
using Application.Studies.V1.Queries.Studies;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApi.Controllers.V1.Studies
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/studies")]
    [AllowAnonymous]
    public class StudyInfoController : Controller
    {
        private readonly IMediator _mediator;

        public StudyInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// [AllowAnonymous] Retrieves a limited amount of study information
        /// </summary>
        /// <remarks>Get basic study information to show unauthenticated participants before they choose to register interest in this study</remarks>
        /// <response code="200">Study information retrieved</response>
        /// <response code="404">Study not found</response>
        /// <response code="500">Server side error</response>
        /*[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<StudyInfoResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = null)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("{studyId:long}/info")]
        public async Task<IActionResult> GetStudyInfo(long studyId)
        {
            return Ok(await _mediator.Send(new GetStudyInfoQuery(studyId)));
        }
        */
        /// <summary>
        /// [AllowAnonymous] Retrieves a limited amount of study information
        /// </summary>
        /// <remarks>Get basic study information to show unauthenticated participants before they choose to register interest in this study</remarks>
        /// <response code="200">Study information retrieved</response>
        /// <response code="404">Study not found</response>
        /// <response code="500">Server side error</response>
        /*[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<PaginatedResponse<StudySiteInfoResponse>>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = null)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("{studyId:long}/sites/info")]
        public async Task<IActionResult> GetStudySiteInfo(long studyId, int limit = 10, string paginationToken = null)
        {
            return Ok(await _mediator.Send(new GetStudySitesInfoPagedQuery(studyId, limit, paginationToken)));
        }
        */
    }
}