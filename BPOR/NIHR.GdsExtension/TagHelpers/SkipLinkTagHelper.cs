using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GdsExtension.TagHelpers
{
    [HtmlTargetElement("gds-skip-link")]
    public class GdsSkipLinkTagHelper : TagHelper
    {
        public string Target { get; set; } = "#main";

        public string Text { get; set; } = "Skip to main content";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("href", Target);
            output.Attributes.SetAttribute("class", "govuk-skip-link");
            output.Attributes.SetAttribute("data-module", "govuk-skip-link");

            output.Content.SetContent(Text);
        }
    }
}
