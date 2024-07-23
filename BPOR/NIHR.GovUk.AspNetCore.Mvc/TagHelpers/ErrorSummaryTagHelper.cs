using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

public class ErrorSummaryTagHelper(IHtmlHelper htmlHelper) : PartialTagHelperBase(htmlHelper)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        var modelErrors = ViewContext.ModelState
            .Where(ms => ms.Value.Errors.Count > 0)
            .DistinctBy(x => x.Value.Errors.FirstOrDefault()?.ErrorMessage);

        var modelProperties = ViewContext.ViewData.Model?.GetType().GetProperties();

        var errors = new List<KeyValuePair<int, KeyValuePair<string, ModelStateEntry>>>();
        foreach (var errorKeyValuePair in modelErrors)
        {
            var modelProperty = modelProperties?.FirstOrDefault(o => o.Name == errorKeyValuePair.Key);
            var displayAttribute = modelProperty?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
            var propertyOrder = displayAttribute?.Order ?? 0;
            errors.Add(new(propertyOrder, errorKeyValuePair));
        }

        var orderedErrors = errors.OrderBy(o => o.Key)
            .Select(x => x.Value);
        
        var content = await RenderPartialAsync("_ErrorSummary", orderedErrors);
        output.Content.SetHtmlContent(content);
    }
}
