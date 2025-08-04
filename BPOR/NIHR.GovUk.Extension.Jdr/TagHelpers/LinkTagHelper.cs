using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.Extension.Jdr.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "class")]
    public class LinkTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output.Attributes.TryGetAttribute("class", out var classAttr))
            {
                var classes = classAttr.Value?.ToString()
                    ?.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToList() ?? new List<string>();

                if (classes.Contains("button", StringComparer.OrdinalIgnoreCase))
                {
                    output.Attributes.SetAttribute("class", "govuk-button");
                }
            }
        }
    }
}