using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.Extension.Jdr.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "class")]
    public class SkipLinkTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output.Attributes.TryGetAttribute("class", out var classAttr))
            {
                var classList = classAttr.Value?.ToString()?
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToList() ?? new List<string>();

                if (classList.Contains("skip-link", StringComparer.OrdinalIgnoreCase))
                {
                    output.Attributes.SetAttribute("class", "govuk-skip-link");

                    if (!output.Attributes.ContainsName("href"))
                    {
                        output.Attributes.SetAttribute("href", "#main-content");
                    }

                    if (!output.Content.IsModified)
                    {
                        output.Content.SetContent("Skip to main content");
                    }
                }
            }
        }
    }
}
