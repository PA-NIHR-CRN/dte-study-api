using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.GovUk.AspNetCore.Mvc.Models;
using NIHR.Infrastructure.AspNetCore;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement("gov-uk-summary-list-item")]
public class GovUkSummaryListItemTagHelper(IHtmlHelper htmlHelper) : PartialTagHelperBase(htmlHelper)
{
    public ModelExpression? For { get; set; }

    public string? Name { get; set; }

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        output.TagName = null;

        var summaryContext = new SummaryListItemContext();

        context.Items[typeof(SummaryListItemContext)] =
            summaryContext;

        var innerContent =
            await output.GetChildContentAsync();

        var content = await RenderPartialAsync(
            "_SummaryListItem",
            new GovUkSummaryListItemModel(
                Name ?? For?.Metadata.DisplayName ?? string.Empty,
                For?.GetDisplayString(),
                summaryContext.ValueContent,
                innerContent));

        output.Content.SetHtmlContent(content);
    }
}