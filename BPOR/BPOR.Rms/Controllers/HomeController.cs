using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BPOR.Rms.Models;
using Microsoft.AspNetCore.Authorization;

namespace BPOR.Rms.Controllers;

[AllowAnonymous]
public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    [HttpGet]
    public IActionResult Index()
    {
        if (User?.Identity?.IsAuthenticated ?? false)
        {
            return RedirectToAction("Index", "Study");
        }
        else
        {
            if (Request.Query.ContainsKey("sign-in"))
            {
                return RedirectToAction("Index", "Study");
            }
            else if (Request.Query.ContainsKey("start"))
            {
                return RedirectToAction("Create", "Account");
            }
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
