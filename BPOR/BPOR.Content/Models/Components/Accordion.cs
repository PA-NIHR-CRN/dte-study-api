using Contentful.Core.Models;

namespace BPOR.Content.Models.Components
{
    public class Accordion : IContentfulComponent
    {
       
        public string Title { get; set; }
        public string Content { get; set; }
        public IContentfulComponent contentNew { get; set; }

        override public string getTitle() { return Title; }

    }
}
