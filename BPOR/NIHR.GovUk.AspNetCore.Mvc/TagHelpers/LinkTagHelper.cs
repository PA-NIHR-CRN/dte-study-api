using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement("a")]
    public class LinkTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var classAttr = output.Attributes["class"];
            var existingClasses = classAttr?.Value?.ToString()?.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList()
                                  ?? new List<string>();

            if (!existingClasses.Contains("govuk-link", StringComparer.OrdinalIgnoreCase))
            {
                existingClasses.Add("govuk-link");
            }

            output.Attributes.SetAttribute("class", string.Join(" ", existingClasses));
        }
    }
}
