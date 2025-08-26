using BPOR.Content.Models.Components;
using Contentful.Core.Configuration;

namespace BPOR.Content
{
    public class ModulesResolver : IContentTypeResolver
    {

        private Dictionary<string, Type> _types = new Dictionary<string, Type>()
        {
            { "video", typeof(Video) },
            { "card", typeof(Card) },
        };

        /// <summary>
        /// Method to get a type based on the specified content type id.
        /// </summary>
        /// <param name="contentTypeId">The content type id to resolve to a type.</param>
        /// <returns>The type for the content type id or null if none is found.</returns>
        public Type Resolve(string contentTypeId)
        {
            return _types.TryGetValue(contentTypeId, out var type) ? type : null;
        }
    }
}
