using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.GovUk.AspNetCore.Mvc.Models;

namespace BPOR.Rms.TagHelpers;

public class GovUkSummaryListItemTagHelper(IHtmlHelper htmlHelper) : PartialTagHelperBase(htmlHelper)
{
    public ModelExpression For { get; set; }
    
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        var innerContent = await output.GetChildContentAsync();
        var content = await RenderPartialAsync("_SummaryListItem", 
            new GovUkSummaryListItemModel(For?.Name, For?.Model, innerContent));
        output.Content.SetHtmlContent(content);
    }
}
