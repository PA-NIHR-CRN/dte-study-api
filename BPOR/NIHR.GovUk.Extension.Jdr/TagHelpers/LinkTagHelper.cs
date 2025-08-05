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
                    classes.RemoveAll(c => c.Equals("button", StringComparison.OrdinalIgnoreCase));

                    classes.Insert(0, "govuk-button");

                    output.Attributes.SetAttribute("class", string.Join(" ", classes));
                }
            }
        }
    }
}
