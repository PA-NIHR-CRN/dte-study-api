using System.Threading.Tasks;
using Application.ReferenceData.V1.Queries;
using Application.Responses.V1.FeatureFlags;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApi.Controllers.V1.ReferenceData
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/referencedata")]
    public class ReferenceDataController : Controller
    {
        private readonly IMediator _mediator;

        public ReferenceDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// [AllowAnonymous] Get value if feature flag is enabled by service, feature names
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="500">Server side error</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<FeatureFlagResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("featureflag/service/{serviceName}/feature/{featureName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFeatureFlag(string serviceName, string featureName)
        {
            return Ok(await _mediator.Send(new GetFeatureFlagQuery(serviceName, featureName)));
        }
    }
}