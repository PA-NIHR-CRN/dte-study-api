using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.GovUk.AspNetCore.Mvc.Models;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement("govuk-text-area")]
public class CharacterCountTagHelper(IHtmlHelper htmlHelper)
    : PartialTagHelperBase(htmlHelper)
{
    [HtmlAttributeName("asp-for")]
    public ModelExpression AspFor { get; set; } = null!;

    public string? Label { get; set; }

    public string? Hint { get; set; }

    public int MaxLength { get; set; }

    public int Rows { get; set; } = 5;

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        output.TagName = null;

        var name = AspFor.Name;
        var id = TagBuilder.CreateSanitizedId(name, "_");

        ViewContext.ViewData.ModelState.TryGetValue(
            name,
            out var modelState);

        var errorMessage = modelState?
            .Errors
            .FirstOrDefault()?
            .ErrorMessage;

        var model = new GovUkCharacterCountModel(
            Name: name,
            Id: id,
            Label: Label,
            Hint: Hint,
            Value: AspFor.Model?.ToString(),
            MaxLength: MaxLength,
            Rows: Rows,
            ErrorMessage: errorMessage);

        var content = await RenderPartialAsync(
            "_TextArea",
            model);

        output.Content.SetHtmlContent(content);
    }
}