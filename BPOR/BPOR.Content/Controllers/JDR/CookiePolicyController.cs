using BPOR.Content.Models;
using Contentful.Core;
using Contentful.Core.Search;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Globalization;

namespace BPOR.Content.Controllers
{
    public class CookiePolicyController : Controller
    {
        private readonly ILogger<CookiePolicyController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptionsSnapshot<ContentSettings> _contentSettings;

        public CookiePolicyController(ILogger<CookiePolicyController> logger, IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<ContentSettings> contentSettings)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _contentSettings = contentSettings;
        }

        
        public async Task<IActionResult> Index([FromServices] IContentfulClient contentfulClient, [FromKeyedServices("preview")] IContentfulClient contentfulPreviewClient, string? env_id = null, string? entry_sys_id = null, bool preview = false)
        {
            entry_sys_id = entry_sys_id ?? _contentSettings.Value.CookiePolicyId;
            contentfulClient.ContentTypeResolver = new ModulesResolver();

            var client = preview ? contentfulPreviewClient : contentfulClient;
            ViewData["site"] = "JDR";
            return await GetContent(client, entry_sys_id);
        }

        private async Task<IActionResult> GetContent(IContentfulClient contentfulClient, string entry_sys_id)
        {
            _logger.LogDebug("CookiePolicy.Index()");
            var rqf = _httpContextAccessor?.HttpContext?.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf?.RequestCulture.Culture ?? CultureInfo.GetCultureInfo("en-GB");

            var queryBuilder = QueryBuilder<CookiePolicyPage>.New
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
