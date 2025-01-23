using Contentful.Core;
using Contentful.Core.Search;
using CRNCC_2391.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Globalization;

namespace CRNCC_2391.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptionsSnapshot<ContentSettings> _contentSettings;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<ContentSettings> contentSettings)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _contentSettings = contentSettings;
        }

        
        public async Task<IActionResult> Index([FromServices] IContentfulClient contentfulClient, [FromKeyedServices("preview")] IContentfulClient contentfulPreviewClient, string? env_id = null, string? entry_sys_id = null, bool preview = false)
        {
            entry_sys_id = entry_sys_id ?? _contentSettings.Value.CampaignPageId;

            var client = preview ? contentfulPreviewClient : contentfulClient;

            return await GetContent(client, entry_sys_id);
        }

        private async Task<IActionResult> GetContent(IContentfulClient contentfulClient, string entry_sys_id)
        {
            _logger.LogDebug("Home.Index()");
            var rqf = _httpContextAccessor?.HttpContext?.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf?.RequestCulture.Culture ?? CultureInfo.GetCultureInfo("en-GB");

            var queryBuilder = QueryBuilder<CampaignPage>.New
            .Include(10)
            .LocaleIs(culture.ToString())

            .FieldEquals("sys.id", entry_sys_id);

            var model = (await contentfulClient.GetEntries(queryBuilder)).FirstOrDefault();
            return View("Index", model);
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
}
