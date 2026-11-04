using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.GovUk.AspNetCore.Mvc.Models;
using NIHR.GovUk.AspNetCore.Mvc.Views;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement("govuk-tag")]
public class TagTagHelper(IHtmlHelper htmlHelper) : PartialTagHelperBase(htmlHelper)
{
    public GovUkTagColour Colour { get; set; } = GovUkTagColour.Default;
    
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {        
        output.TagName = "strong";
        output.AddClass("govuk-tag", HtmlEncoder.Default);
        output.AddClass(GovUkStylingHelper.GetTagColourClass(Colour), HtmlEncoder.Default);
    }
}