using System.Threading.Tasks;
using Application.Extensions;
using Application.Models.ResearcherStudies;
using Application.Responses;
using Application.Studies.V1.Commands.ResearcherStudies;
using Application.Studies.V1.Queries.ResearcherStudies;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Attributes;
using StudyApi.Requests.ResearcherStudies;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApi.Controllers.V1.ResearcherStudies
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/researcherstudies")]
    [AuthorizeResearcher]
    public class ResearcherStudiesController : Controller
    {
        private readonly IMediator _mediator;

        public ResearcherStudiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// [AuthorizeResearcher] Retrieves a researcher study record for the current logged in user.
        /// </summary>
        /// <response code="200">Researcher study returned</response>
        /// <response code="500">Server side error</response>
        /*[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ResearcherStudyModel>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("{studyId:long}")]
        public async Task<IActionResult> GetResearcherStudy(long studyId)
        {
            var response = await _mediator.Send(new GetResearcherStudyQuery(User.GetUserIdCognito(), studyId));

            return Ok(response);
        }
        */
        /// <summary>
        /// [AuthorizeResearcher] Retrieves a limited amount of researcher study records (paginated) showing who is allowed to access the study.
        /// </summary>
        /// <response code="200">Researcher studies returned</response>
        /// <response code="500">Server side error</response>
        /// <remarks>Will return data if the logged in user is a member/admin for this study</remarks>
        /*[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<PaginationListResponse<ResearcherStudyModel>>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("studies/{studyId:long}")]
        public async Task<IActionResult> GetResearcherStudiesByStudyId(long studyId, int limit = 10, string paginationToken = null)
        {
            var response = await _mediator.Send(new GetResearcherStudiesByStudyIdQuery(studyId, User.GetUserIdCognito(), limit, paginationToken));
        
            return Ok(response);
        }*/
        
        /// <summary>
        /// [AuthorizeResearcher] Creates a researcher study record enabling access the study.
        /// </summary>
        /// <response code="200">Researcher study created</response>
        /// <response code="500">Server side error</response>
       /* [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPost]
        public async Task<IActionResult> CreateResearcherStudy([FromBody] CreateResearcherStudyRequest request)
        {
            var response = await _mediator.Send(new CreateResearcherStudyCommand
            (
                User.GetUserIdCognito(),
                request.ResearcherId,
                request.ResearcherFirstname,
                request.ResearcherLastname,
                request.ResearcherEmail,
                request.StudyId,
                request.Role
            ));

            return Ok(response);
        }
       */
        /// <summary>
        /// [AuthorizeResearcher] Update a Researcher Study
        /// </summary>
        /// <remarks>Update a Researcher Study</remarks>
        /// <response code="200">Researcher Study updated when IsSuccess true</response>
        /// <response code="500">Server side error</response>
       /* [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPut("{studyId:long}/researcher/{researcherId}")]
        public async Task<IActionResult> UpdateResearcherStudy(long studyId, string researcherId,[FromBody] UpdateResearcherStudyRequest request)
        {
            var userId = User.GetUserIdCognito();

            var command = new UpdateResearcherStudyCommand
            (
                userId,
                researcherId,
                studyId,
                request.Role
            );

            return Ok(await _mediator.Send(command));
        }
       */
        /// <summary>
        /// [AuthorizeResearcher] Delete a Researcher Study
        /// </summary>
        /// <remarks>Delete a Researcher Study</remarks>
        /// <response code="200">Researcher Study deleted when IsSuccess true</response>
        /// <response code="500">Server side error</response>
       /* [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpDelete("{studyId:long}/researcher/{researcherId}")]
        public async Task<IActionResult> DeleteResearcherStudy(long studyId, string researcherId)
        {
            var userId = User.GetUserIdCognito();

            var command = new DeleteResearcherStudyCommand
            (
                userId,
                researcherId,
                studyId
            );

            return Ok(await _mediator.Send(command));
        }*/
    }
}