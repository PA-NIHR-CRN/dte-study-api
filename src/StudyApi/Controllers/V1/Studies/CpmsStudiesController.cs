using System.Threading.Tasks;
using Application.Studies.V1.Queries.Cpms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudyApi.Controllers.V1.Studies
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/studies")]
    [Authorize("AnyAuthenticatedUser")]
    public class CpmsStudiesController : Controller
    {
        private readonly IMediator _mediator;

        public CpmsStudiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// [Authorize("AnyAuthenticatedUser")] Get study from the CPMS API
        /// </summary>
        /// <param name="studyId"></param>
        /// <returns></returns>
        //[HttpGet("cpms/{studyId:long}")]
        //public async Task<IActionResult> GetCpmsStudy(long studyId)
        //{
        //    return Ok(await _mediator.Send(new GetCpmsStudyQuery(studyId)));
        //}
    }
}