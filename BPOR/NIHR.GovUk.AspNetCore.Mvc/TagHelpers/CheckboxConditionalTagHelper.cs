using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.GovUk.AspNetCore.Mvc.Models;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement("checkbox-conditional")]
public class CheckboxConditionalTagHelper(IHtmlHelper htmlHelper) : PartialTagHelperBase(htmlHelper)
{
    public string Id { get; set; } = string.Empty;

    [HtmlAttributeName("is-hidden")]
    public bool IsHidden { get; set; } = true;
    
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;

        var innerContent = await output.GetChildContentAsync();

        var model = new GovUkCheckboxConditionalModel(
            Id: Id,
            IsHidden: IsHidden,
            InnerContent: innerContent);

        var content = await RenderPartialAsync("_CheckboxConditional", model);

        output.Content.SetHtmlContent(content);
    }
}