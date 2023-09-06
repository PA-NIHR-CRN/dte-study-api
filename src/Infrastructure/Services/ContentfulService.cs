using System.Globalization;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1;
using Contentful.Core;
using Contentful.Core.Search;

namespace Infrastructure.Services
{
    public class ContentfulService: IContentfulService
    {
        private readonly IContentfulClient _client;

        public ContentfulService(IContentfulClient client)
        {
            _client = client;
        }

        public async Task<ContentfulEmail> GetContentfulEmailAsync(string entryId, CultureInfo locale)
        {
            var entry = await _client.GetEntry(entryId, new QueryBuilder<ContentfulEmail>().LocaleIs(locale.ToString()));
            return entry;
        }
    }
}