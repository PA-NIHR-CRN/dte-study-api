using Contentful.Core.Models;

namespace BPOR.Content.Models.Components
{
    public class Card : IContentfulComponent
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public Link Link { get; set; }

        public Asset Image { get; set; }
    }
}
