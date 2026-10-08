using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.GovUk.AspNetCore.Mvc.Models;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

public class RadioTagHelper(IHtmlHelper htmlHelper)
    : PartialTagHelperBase(htmlHelper)
{
    public string Value { get; set; } = string.Empty;

    public bool Autofocus { get; set; }

    public string? ConditionalId { get; set; }

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        var radiosContext = context.GetRequired<RadiosContext>(
            RadioSet.ContextVariableName,
            "govuk-radio must be nested inside govuk-radios");

        output.TagName = null;

        var innerContent = await output.GetChildContentAsync();

        var isSelected = string.Equals(
            radiosContext.ForValue?.ToString(),
            Value,
            StringComparison.OrdinalIgnoreCase);

        var model = new GovUkRadioModel(
            Name: radiosContext.ForName,
            Value: Value,
            IsSelected: isSelected,
            Autofocus: Autofocus,
            InnerContent: innerContent,
            ConditionalId: ConditionalId);

        var content = await RenderPartialAsync("_Radio", model);

        output.Content.SetHtmlContent(content);
    }
}