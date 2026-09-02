using CpmsCore.Web.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

namespace BPOR.Rms.Controllers;

public class TestController(ILogger<TestController> logger) : Controller
{
    [Authorize(AuthenticationSchemes = $"{AccessTokenAuthenticationOptions.AuthenticationScheme}, {CookieAuthenticationDefaults.AuthenticationScheme}")]
    [AuthorizeAnyPolicy(PolicyNames.IsResearcherCreatingStudy, PolicyNames.IsAdmin)]
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
