using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GdsExtension.TagHelpers
{
    [HtmlTargetElement("gds-button")]
    public class ButtonTagHelper : TagHelper
    {
        public string Text { get; set; } = "Button";
        public string? Href { get; set; }
        public string Type { get; set; } = "submit";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            const string buttonClass = "govuk-button";

            bool isLink = !string.IsNullOrWhiteSpace(Href);
            output.TagName = isLink ? "a" : "button";
            output.Attributes.SetAttribute("class", buttonClass);

            if (isLink)
            {
                output.Attributes.SetAttribute("href", Href);
                output.Attributes.SetAttribute("role", "button");
            }
            else
            {
                output.Attributes.SetAttribute("type", Type);
            }

            output.Content.SetContent(Text);
        }
    }
}