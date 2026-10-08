using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.GovUk.AspNetCore.Mvc.Models;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement("govuk-checkbox")]
public class CheckboxTagHelper(IHtmlHelper htmlHelper) : PartialTagHelperBase(htmlHelper)
{
    private readonly IHtmlHelper _htmlHelper = htmlHelper;

    [HtmlAttributeName("asp-for")]
    public ModelExpression AspFor { get; set; } = null!;

    public string? Label { get; set; }

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        var childContent = await output.GetChildContentAsync();

        output.TagName = null;

        var fullName = ViewContext.ViewData.TemplateInfo
            .GetFullHtmlFieldName(AspFor.Name);

        var id = TagBuilder.CreateSanitizedId(
            fullName,
            _htmlHelper.IdAttributeDotReplacement);

        var isChecked = AspFor.Model is true;

        ViewContext.ViewData.ModelState.TryGetValue(
            fullName,
            out var modelState);

        var errorMessage = modelState?
            .Errors
            .Select(error => error.ErrorMessage)
            .FirstOrDefault(message => !string.IsNullOrWhiteSpace(message));

        var model = new GovUkCheckboxModel(
            Name: fullName,
            Id: id,
            Label: Label,
            LabelHtml: childContent,
            Checked: isChecked,
            ErrorMessage: errorMessage);

        var content = await RenderPartialAsync("_Checkbox", model);

        output.Content.SetHtmlContent(content);
    }
}