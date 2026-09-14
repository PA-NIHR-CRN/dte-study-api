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
    public bool ShowName { get; set; } = true;

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
        
        var errors = For == null ? [] : GetErrors(For.Name);
        
        var content = await RenderPartialAsync(
            "_SummaryListItem",
            new GovUkSummaryListItemModel(
                ShowName,
                Name ?? For?.Metadata.DisplayName ?? string.Empty,
                For?.GetDisplayString(),
                summaryContext.ValueContent,
                innerContent,
                errors.FirstOrDefault()));

        output.Content.SetHtmlContent(content);
    }
    
    private IReadOnlyList<string> GetErrors(string fullName)
    {
        if (!ViewContext.ViewData.ModelState.TryGetValue(
                fullName,
                out var modelState))
        {
            return [];
        }

        return modelState.Errors
            .Select(error => error.ErrorMessage)
            .Where(message => !string.IsNullOrWhiteSpace(message))
            .ToList();
    }
    
    private string GetFullName(ModelExpression expression)
    {
        return ViewContext.ViewData.TemplateInfo
            .GetFullHtmlFieldName(expression.Name);
    }
}