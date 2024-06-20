using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Home;
using BPOR.Rms.Settings;
using Dte.Common.Contracts;
using Dte.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NIHR.Infrastructure.Interfaces;

namespace BPOR.Rms.Controllers;

[AllowAnonymous]
public class HomeController(
    ILogger<HomeController> logger,
    IContentProvider contentProvider,
    IRichTextToHtmlService richTextToHtmlService,
    IOptions<ContentSettings> contentSettings
    ) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        if (Request.Query.ContainsKey("sign-in") || (User?.Identity?.IsAuthenticated ?? false))
        {
            return RedirectToAction("Index", "Study");
        }
        
        var response = await contentProvider.GetContentAsync(contentSettings.Value.ResearcherWelcomePage, cancellationToken);

        var richTextDocument = JsonConvert.DeserializeObject<RichTextDocument>(response);

        var model = new HomeViewModel
        {
            Content = richTextToHtmlService.Convert(richTextDocument.Content)
        };


        return View(model);
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

// TODO move to a shared project, is dte common becoming defunct?
public class RichTextDocument
{
    public RichTextNode Content { get; set; }
}
