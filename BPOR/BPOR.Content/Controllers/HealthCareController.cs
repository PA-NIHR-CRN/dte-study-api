using Amazon.SecretsManager.Model.Internal.MarshallTransformations;
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
    public class HealthCareController : Controller
    {
        private readonly ILogger<HealthCareController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptionsSnapshot<ContentSettings> _contentSettings;

        public HealthCareController(ILogger<HealthCareController> logger, IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<ContentSettings> contentSettings)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _contentSettings = contentSettings;
        }

        
        public async Task<IActionResult> Index([FromServices] IContentfulClient contentfulClient, [FromKeyedServices("preview")] IContentfulClient contentfulPreviewClient, string? env_id = null, string? entry_sys_id = null, string id = null, bool preview = false)
        {
            entry_sys_id = entry_sys_id ?? _contentSettings.Value.JdrHealthCareId;

            var client = preview ? contentfulPreviewClient : contentfulClient;
            ViewData["site"] = "JDR";

            return await GetContent(client, entry_sys_id);
        }

        public async Task<IActionResult> article([FromServices] IContentfulClient contentfulClient, [FromKeyedServices("preview")] IContentfulClient contentfulPreviewClient, string id, string? env_id = null, string? entry_sys_id = null, string? article = null , bool preview = false) {


            var client = preview ? contentfulPreviewClient : contentfulClient;
            ViewData["site"] = "JDR";

            return await GetContentByPageTitle(client, id);
        }
        private async Task<IActionResult> GetContentByPageTitle(IContentfulClient contentfulClient, string PageTitle)
        {
            _logger.LogDebug("healthcare.article()");
            var rqf = _httpContextAccessor?.HttpContext?.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf?.RequestCulture.Culture ?? CultureInfo.GetCultureInfo("en-GB");

                var queryBuilder = QueryBuilder<JdrHealthCarePage>.New
            .Include(10)
            .LocaleIs(culture.ToString())
            .ContentTypeIs("jdrHealthCare")
            .FieldEquals("fields.title", PageTitle);

            var model = (await contentfulClient.GetEntries(queryBuilder)).FirstOrDefault();
            return View("Index", model);

        }
            private async Task<IActionResult> GetContent(IContentfulClient contentfulClient, string entry_sys_id)
        {
            _logger.LogDebug("healthcare.index()");
            var rqf = _httpContextAccessor?.HttpContext?.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf?.RequestCulture.Culture ?? CultureInfo.GetCultureInfo("en-GB");

                var queryBuilder = QueryBuilder<JdrHealthCarePage>.New
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
