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
        public List<Accordion> AccordionSection {  get; set; }    

        public OrderedListSection OrderListSection { get; set; }

        public TextSection TextSection2 { get; set; }

        public Carousel CarouselSection { get; set; }

        public TextSection TextSection3 { get; set; }

        public Carousel CardSection { get; set; }

        public string BackToTopLinkLabel { get; set; }
    }

    public class MultiLinkSection
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public List<Link> JdrPageLinks { get; set; }
    }


    public class Accordion
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }


    //public class TextSection
    //{
    //    public string Heading { get; set; }

    //    public string Body { get; set; }
    //}

    //public class Video
    //{
    //    public string Heading { get; set; }

    //    public string Description { get; set; }
    //    public Link Link { get; set; }

    //    public string VideoUrl { get; set; }

    //    public string Transcript { get; set; }
    //}

    //public class Card
    //{
    //    public string Title { get; set; }

    //    public string Body { get; set; }

    //    public Link Link { get; set; }

    //    public Asset Image { get; set; }
    //}

    //public class Link
    //{
    //    public string Label { get; set; }
    //    public string Url { get; set; }
    //    public string AriaLabel { get; set; }
    //}
}
