using Application.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace StudyApi.Controllers.V1;

public class HealthCheckController: Controller
{
    [HttpGet]
    [Route("healthcheck")]
    public IActionResult HealthCheck()
    {
        return Ok();
    }
}
