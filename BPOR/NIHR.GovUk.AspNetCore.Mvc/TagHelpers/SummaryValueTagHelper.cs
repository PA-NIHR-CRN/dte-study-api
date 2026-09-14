using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement("summary-value", ParentTag = "gov-uk-summary-list-item")]
public class SummaryValueTagHelper : TagHelper
{
    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        if (context.Items.TryGetValue(
                typeof(SummaryListItemContext),
                out var item) &&
            item is SummaryListItemContext summaryContext)
        {
            summaryContext.ValueContent =
                await output.GetChildContentAsync();
        }

        output.SuppressOutput();
    }
}