using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudyApi.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/healthcheck")]
public class HealthCheckController: Controller
{
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    public IActionResult HealthCheck()
    {
        return Ok();
    }
}
