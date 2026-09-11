using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement("date-input")]
public class DateInputTagHelper(IHtmlHelper htmlHelper)
    : PartialTagHelperBase(htmlHelper)
{
    [HtmlAttributeName("for")]
    public ModelExpression For { get; set; } = null!;
    
    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
       
        output.TagName = null;
        
        var content = await RenderPartialAsync(
            "_DateInput",
            For.Model);
        
 

        output.Content.SetHtmlContent(content);
    }
}