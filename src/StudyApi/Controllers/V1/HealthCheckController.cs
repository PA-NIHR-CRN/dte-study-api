using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudyApi.Controllers.V1;

public class HealthCheckController: Controller
{
    [HttpGet]
    [Route("healthcheck")]
    [AllowAnonymous]
    public IActionResult HealthCheck()
    {
        return Ok();
    }
}
