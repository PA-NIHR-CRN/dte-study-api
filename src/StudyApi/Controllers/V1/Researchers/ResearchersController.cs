using System.Threading.Tasks;
using Application.Researchers.V1.Queries;
using Application.Responses.V1.Researchers;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Attributes;
using StudyApi.Requests.Researchers;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApi.Controllers.V1.Researchers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/researchers")]
    [AuthorizeResearcher]
    public class ResearchersController : Controller
    {
        private readonly IMediator _mediator;

        public ResearchersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// [AuthorizeResearcher] Retrieves researcher details from IDG
        /// </summary>
        /// <response code="200">Researcher returned</response>
        /// <response code="500">Server side error</response>
        /*[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ResearcherResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetResearcherFromIdg([FromRoute] GetResearcherFromIdgRequest request)
        {
            return  Ok(await _mediator.Send(new GetResearcherFromIdgQuery(request.Email)));
        }*/
    }
}