using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BPOR.Rms.Models;
using Dte.Common.Models;
using Microsoft.AspNetCore.Authorization;

namespace BPOR.Rms.Controllers;

[AllowAnonymous]
public class HomeController(
    ILogger<HomeController> logger
    ) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        if (Request.Query.ContainsKey("sign-in") || (User?.Identity?.IsAuthenticated ?? false))
        {
            return RedirectToAction("Index", "Study");
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
