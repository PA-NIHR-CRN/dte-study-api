using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.Extension.Jdr.TagHelpers
{
    [HtmlTargetElement("gds-accordion")]
    public class AccordionTagHelper : TagHelper
    {
        public string Id { get; set; } = "accordion";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-accordion");
            output.Attributes.SetAttribute("data-module", "govuk-accordion");
            output.Attributes.SetAttribute("data-remember-expanded", "false");
            output.Attributes.SetAttribute("id", Id);
        }
    }
}
