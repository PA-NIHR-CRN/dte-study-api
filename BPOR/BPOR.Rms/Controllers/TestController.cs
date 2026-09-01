using CpmsCore.Web.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

namespace BPOR.Rms.Controllers;

public class TestController(ILogger<TestController> logger) : Controller
{
    private readonly ILogger<TestController> _logger = logger;

    [Authorize(AuthenticationSchemes = $"{AccessTokenAuthenticationOptions.DefaultScheme}, {CookieAuthenticationDefaults.AuthenticationScheme}")]
    [AuthorizeAnyPolicy("IsResearcherCreatingStudy", "IsAdmin")]
    [Route("[controller]/[action]/{id:int}")]
    public IActionResult Test1(int id)
    {
        return Accepted();
    }
    
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }
}
