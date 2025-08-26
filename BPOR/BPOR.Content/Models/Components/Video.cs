using Contentful.Core.Models;

namespace BPOR.Content.Models.Components
{
    public class Video : IContentfulComponent
    {
        public string Heading { get; set; }
        public SystemProperties sys { get; set; }

        public string Description { get; set; }
        public string LowerDescription { get; set; }
        public Link Link { get; set; }

        public string VideoUrl { get; set; }

        public string Transcript { get; set; }
    }
}
