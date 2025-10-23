using Contentful.Core.Models;

namespace BPOR.Content.Models.Components
{
    public class RichTextSection : IContentfulComponent
    {
        public string contentTitle { get; set; }
        public Document text { get; set; }

        override public string getTitle(){ return contentTitle; }

    }
}
