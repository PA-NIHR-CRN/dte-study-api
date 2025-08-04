using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GdsExtension.TagHelpers
{
    [HtmlTargetElement("gds-accordion-item", ParentTag = "gds-accordion")]
    public class AccordionItemTagHelper : TagHelper
    {
        public string Heading { get; set; } = "";
        public string ItemId { get; set; } = Guid.NewGuid().ToString();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-accordion__section");

            var headingHtml = $@"
                <div class=""govuk-accordion__section-header"">
                    <h2 class=""govuk-accordion__section-heading"">
                        <span class=""govuk-accordion__section-button"" id=""{ItemId}-heading"">
                            {Heading}
                        </span>
                    </h2>
                </div>";

            var contentStart = $@"<div id=""{ItemId}-content"" class=""govuk-accordion__section-content"">";
            var contentEnd = "</div>";

            output.PreContent.SetHtmlContent(headingHtml + contentStart);
            output.PostContent.SetHtmlContent(contentEnd);
        }
    }
}
