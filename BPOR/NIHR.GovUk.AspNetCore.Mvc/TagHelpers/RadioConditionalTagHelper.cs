using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.GovUk.AspNetCore.Mvc.Models;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

public class RadioConditionalTagHelper(IHtmlHelper htmlHelper)
    : PartialTagHelperBase(htmlHelper)
{
    public string Id { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        var radiosContext = context.GetRequired<RadiosContext>(
            RadioSet.ContextVariableName,
            "radio-conditional must be nested inside radio-set");

        output.TagName = null;

        var innerContent = await output.GetChildContentAsync();

        var isSelected = string.Equals(
            radiosContext.ForValue?.ToString(),
            Value,
            StringComparison.OrdinalIgnoreCase);

        var model = new GovUkRadioConditionalModel(
            Id: Id,
            IsHidden: !isSelected,
            InnerContent: innerContent);

        var content = await RenderPartialAsync(
            "_RadioConditional",
            model);

        output.Content.SetHtmlContent(content);
    }
}