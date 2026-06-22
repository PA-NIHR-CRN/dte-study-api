using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using NIHR.GovUk.AspNetCore.Mvc.Models;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers
{
    public class GovukButtonTagHelper : TagHelper
    {
        public GovUkButtonType ButtonType { get; set; } = GovUkButtonType.Default;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.AddClass("govuk-button", HtmlEncoder.Default);
            
            switch (ButtonType)
            {
                case GovUkButtonType.Default:
                    break;
                case GovUkButtonType.Secondary:
                    output.AddClass("govuk-button--secondary", HtmlEncoder.Default);
                    break;
                case GovUkButtonType.Submit:
                    output.AddClass("govuk-button--submit", HtmlEncoder.Default);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
