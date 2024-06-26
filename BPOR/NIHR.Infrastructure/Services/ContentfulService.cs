using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Ganss.Xss;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Interfaces;
using NIHR.Infrastructure.Settings;

namespace NIHR.Infrastructure.Services
{
    public class ContentfulService : IContentProvider
    {
        private readonly IContentfulClient _contentfulClient;
        private readonly IOptions<ContentfulSettings> _contentfulSettings;
        private readonly HtmlSanitizer _htmlSanitizer;

        public ContentfulService(IContentfulClient contentfulClient, IOptions<ContentfulSettings> contentfulSettings, HtmlSanitizer htmlSanitizer)
        {
            _contentfulClient = contentfulClient;
            _contentfulSettings = contentfulSettings;
            _htmlSanitizer = htmlSanitizer;
        }

        private async Task<ContentfulEntry> GetContentByKeyAsync(string contentKey)
        {
            var queryBuilder = new QueryBuilder<ContentfulEntry>().FieldExists("fields.key")
                .FieldEquals("fields.key", contentKey).ContentTypeIs(_contentfulSettings.Value.ContentType);
            var entries = await _contentfulClient.GetEntries(queryBuilder);

            var entry = entries.FirstOrDefault();

            return entry;
        }

        private async Task<ContentfulEntry> GetContentByEntryIdAsync(string entryId)
        {
            var content = await _contentfulClient.GetEntry<ContentfulEntry>(entryId);

            return content;
        }


        public async Task<string> GetContentAsync(string contentId, CancellationToken cancellationToken)
        {
            var content = await GetContentByKeyAsync(contentId) ?? await GetContentByEntryIdAsync(contentId);

            var htmlRenderer = new HtmlRenderer();
            var html = await htmlRenderer.ToHtml(content.Content);
            
            var sanitizedHtml = _htmlSanitizer.Sanitize(html);

            return sanitizedHtml;
        }
    }
}

public class ContentfulEntry
{
    public Document Content { get; set; }
}
