using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BPOR.Rms.TagHelpers
{
    [HtmlTargetElement(Attributes = "back-link-for")]
    public class WithBackLinkTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; } = null!;

        public ModelExpression BackLinkFor { get; set; }

        public override int Order => 50;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var formId = context.UniqueId;
            if (context.AllAttributes.ContainsName("id"))
            {
                formId = context.AllAttributes["id"].Value.ToString();
            }
            else
            {
                output.Attributes.Add("id", formId);
            }

            var value = (BackLinkFor.Model as int?) ?? 1;
            ViewContext.ViewData.ShowBackLink();
            ViewContext.ViewData["_BackLinkForm"] = formId;
            ViewContext.ViewData["_BackLinkFor"] = BackLinkFor;
            ViewContext.ViewData["_BackLinkForValue"] = (value - 1).ToString();
        }
    }
}
