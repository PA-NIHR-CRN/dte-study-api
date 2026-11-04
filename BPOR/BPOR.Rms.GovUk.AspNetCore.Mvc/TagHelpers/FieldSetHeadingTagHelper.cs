using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.GovUk.AspNetCore.Mvc.Models;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement("govuk-fieldset")]
public class FieldSetTagHelper(IHtmlHelper htmlHelper) : PartialTagHelperBase(htmlHelper)
{
    public string HeadingText { get; set; } = string.Empty;
    public ModelExpression? For { get; set; }
    public GovUkHeadingSize HeadingSize { get; set; } = GovUkHeadingSize.Large;
    
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (For != null && !string.IsNullOrEmpty(HeadingText))
        {
            throw new InvalidOperationException($"Either {nameof(HeadingText)} or {nameof(For)} must be specified, but not both.");
        }
        
        var resolvedHeadingText = For?.Metadata.DisplayName ?? HeadingText;
        
        output.TagName = null;
        var innerContent = await output.GetChildContentAsync();
        var content = await RenderPartialAsync("_FieldSet", new GovUkFieldSetModel(resolvedHeadingText, HeadingSize, innerContent));
        output.Content.SetHtmlContent(content);
    }
}