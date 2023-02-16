using System.Linq;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Models.Studies;
using Application.Responses;
using Application.Responses.V1.Studies;
using Application.Studies.V1.Commands.Studies;
using Application.Studies.V1.Queries.Studies;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Attributes;
using StudyApi.Requests.Studies;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApi.Controllers.V1.Studies
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/studies")]
    [AuthorizeResearcher]
    public class StudiesController : Controller
    {
        private readonly IMediator _mediator;

        public StudiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// [AuthorizeResearcher] Retrieves a limited amount of study records (paginated) for the currently logged in researcher
        /// </summary>
        /// <response code="200">Studies returned</response>
        /// <response code="500">Server side error</response>
       /* [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<PaginationListResponse<StudyResponse>>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet] 
        public async Task<IActionResult> GetAllMyStudies(int limit = 10, string paginationToken = null)
        {
            var response = await _mediator.Send(new GetAllUsersStudiesQuery(User.GetUserIdCognito(), limit, paginationToken));

            return Ok(response);
        }*/
        
        /// <summary>
        /// [AuthorizeResearcher] Retrieves a study
        /// </summary>
        /// <response code="200">Study information retrieved</response>
        /// <response code="404">Study not found</response>
        /// <response code="500">Server side error</response>
       /* [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<StudyRoleResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = null)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("{studyId:long}")]
        public async Task<IActionResult> GetStudy(long studyId)
        {
            return Ok(await _mediator.Send(new GetStudyQuery(studyId, User.GetUserIdCognito())));
        }*/
        
        /// <summary>
        /// [AuthorizeResearcher] Retrieves a paged list of study sites
        /// </summary>
        /// <remarks>Will return a paged list of sites for a given study</remarks>
        /// <response code="200">Study sites information retrieved</response>
        /// <response code="500">Server side error</response>
       /* [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<PaginationListResponse<StudySiteResponse>>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("{studyId:long}/sites")]
        public async Task<IActionResult> GetStudySites(long studyId, int limit = 10, string paginationToken = null)
        {
            return Ok(await _mediator.Send(new GetStudySitesPagedQuery(studyId, limit, paginationToken)));
        }
       */
        /// <summary>
        /// [AuthorizeResearcher] Create pre screener questions for a study
        /// </summary>
        /// <response code="200">Created</response>
        /// <response code="500">Server side error</response>
        /*[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPost("prescreenerquestions")]
        public async Task<IActionResult> SavePreScreenerQuestions([FromBody] CreatePreScreenerQuestionnaireRequest request)
        {
            var researcherId = User.GetUserIdCognito();

            var command = new CreatePreScreenerQuestionnaireCommand
            (
                request.StudyId, 
                request.Version, 
                request.Questions.Select(x => new PreScreenerQuestionModel
                {
                    QuestionText = x.QuestionText, Explanation = x.Explanation, Reference = x.Reference,
                    AnswerOptionType = x.AnswerOptionType, AnswerOptions = x.AnswerOptions, ValidAnswerOptions = x.ValidAnswerOptions,
                    Sequence = x.Sequence
                }), 
                researcherId
            );
            
            return Ok(await _mediator.Send(command));
        }
        */
        /// <summary>
        /// [AuthorizeResearcher] Update a studies attributes
        /// </summary>
        /// <response code="200">Created</response>
        /// <response code="500">Server side error</response>
        /*[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPut("{studyId}")]
        public async Task<IActionResult> UpdateStudy(long studyId, [FromBody] UpdateStudyRequest request)
        {
            var researcherId = User.GetUserIdCognito();
            var command = new UpdateStudyCommand
            (
                studyId,
                researcherId,
                request.WhatImportant,
                request.HealthConditions
            );
            return Ok(await _mediator.Send(command));
        }
        */

        // TODO - GetMyStudiesByStatus - GSI Status, LeadResearcherId - paged
        // TODO - GetAllStudiesByStatus - GSI Status, SK = study# - paged
    }
}