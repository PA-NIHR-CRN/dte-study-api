using Contentful.Core.Models;

namespace BPOR.Content.Models.Components
{
    public class GenericText : IContentfulComponent
    {
        public string contentTitle { get; set; }
        public string text { get; set; }

        override public string getTitle(){ return contentTitle; }

    }
}
