using System.Net.Http;
using Contentful.Core;
using Microsoft.Extensions.DependencyInjection;
using NIHR.Infrastructure.Settings;

namespace NIHR.Infrastructure.Extensions
{
    public static class ContentfulExtensions
    {
        public static void AddContentfulServices(this IServiceCollection services, ContentfulSettings contentfulSettings)
        {
            var httpClient = new HttpClient();
            services.AddSingleton<IContentfulClient>(sp => new ContentfulClient(httpClient, contentfulSettings.DeliveryApiKey,
                contentfulSettings.PreviewApiKey, contentfulSettings.SpaceId, contentfulSettings.UsePreviewApi));
        }
    }
}
