using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.GovUk.AspNetCore.Mvc.Models;
using NIHR.GovUk.AspNetCore.Mvc.Views;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

public class InsetTextTagHelper(IHtmlHelper htmlHelper) : PartialTagHelperBase(htmlHelper)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {        
        output.TagName = "div";
        output.AddClass("govuk-inset-text", HtmlEncoder.Default);
    }
}