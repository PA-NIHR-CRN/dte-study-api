using BPOR.Content.Models.Components;
using Contentful.AspNetCore.TagHelpers;
using Contentful.Core.Models;

namespace BPOR.Content
{
    public class JdrHealthCarePage
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string Keywords { get; set; }

        public Card Hero { get; set; }

        public IContentfulComponent MediaSection { get; set; }

        public string Banner {  get; set; }
        public AccordionSection AccordionSection {  get; set; }    
        public OnlineResourcesSection onlineResourcesSection {  get; set; }
        public ContactUsSection contactUsSection { get; set; }
    }


    public class AccordionSection
    {
        public string title { get; set; }
        public string description { get; set; }
        public List<Accordion> accordions { get; set; }
    }


    public class ContactUsSection
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public Link link { get; set; }
    }

    public class OnlineResourcesSection
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public Asset image {  get; set; }
        public Link link { get; set; }
    }


}
