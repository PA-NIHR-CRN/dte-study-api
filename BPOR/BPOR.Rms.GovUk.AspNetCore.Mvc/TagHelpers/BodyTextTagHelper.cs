using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers
{
    public class BodyTextTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p";
            output.AddClass("govuk-body", HtmlEncoder.Default);
        }
    }
}
