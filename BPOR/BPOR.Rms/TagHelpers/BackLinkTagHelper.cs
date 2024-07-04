using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace BPOR.Rms.TagHelpers
{
    public class BackLinkTagHelper(IHtmlGenerator generator) : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; } = null!;

        public override int Order => 100;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var referer = ViewContext.HttpContext.Request.Headers.Referer.FirstOrDefault();
            var showBackLink = ViewContext.ViewData.IsBackLinkEnabled();
            if (ViewContext.ViewData["_BackLinkForm"] is not null)
            {
                var formId = ViewContext.ViewData["_BackLinkForm"] as string;
                var modelExpression = ViewContext.ViewData["_BackLinkFor"] as ModelExpression;
                var backValue = ViewContext.ViewData["_BackLinkForValue"];

                var hiddenElement = generator.GenerateHidden(ViewContext, modelExpression.ModelExplorer, modelExpression.Name, backValue, false, null);

                output.TagName = "button";
                output.TagMode = TagMode.StartTagAndEndTag;

                output.AddClass("govuk-back-link", HtmlEncoder.Default);
                output.Attributes.SetAttribute("form", formId);
                output.Attributes.SetAttribute("type", "submit");
                output.Attributes.SetAttribute("name", hiddenElement.Attributes["name"]);
                output.Attributes.SetAttribute("value", hiddenElement.Attributes["value"]);
                output.Content.SetContent("Back");
            }
            else if (referer != null && showBackLink == true)
            {
                output.TagName = "a";
                output.TagMode = TagMode.StartTagAndEndTag;

                output.AddClass("govuk-back-link", HtmlEncoder.Default);
                output.AddClass("govuk-link", HtmlEncoder.Default);
                output.Attributes.SetAttribute("href", referer);

                output.Content.SetContent("Back");
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}
