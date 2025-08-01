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

        public Video VideoSection { get; set; }

        public MultiLinkSection TextSection1 { get; set; }

        public string Banner {  get; set; }
        public string AccordionSectionTitle {  get; set; }
        public string AccordionSectionDescription { get; set; }
        public AccordionSection AccordionSection1 {  get; set; }    
        public OnlineResourcesSection onlineResourcesSection {  get; set; }
        public ContactUsSection contactUsSection { get; set; }
    }

    public class MultiLinkSection
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public List<Link> JdrPageLinks { get; set; }
    }

    public class AccordionSection
    {
        public string title { get; set; }
        public string description { get; set; }
        public List<Accordion> accordions { get; set; }
    }

    public class Accordion
    {
        public string Title { get; set; }

        public string Content { get; set; }
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
