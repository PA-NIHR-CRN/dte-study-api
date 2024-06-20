using System.Threading;
using System.Threading.Tasks;
using Contentful.Core;
using NIHR.Infrastructure.Interfaces;

namespace NIHR.Infrastructure.Services
{
    public class ContentfulService : IContentProvider
    {
        private readonly IContentfulClient _contentfulClient;

        public ContentfulService(IContentfulClient contentfulClient)
        {
            _contentfulClient = contentfulClient;
        }

        public async Task<string> GetContentAsync(string contentId, CancellationToken cancellationToken)
        {
            var content =  await _contentfulClient.GetEntry<dynamic>(contentId, cancellationToken: cancellationToken);

            return content.ToString();
        }
    }
}
