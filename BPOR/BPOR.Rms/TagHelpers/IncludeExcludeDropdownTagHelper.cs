using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BPOR.Rms.TagHelpers
{
    public class IncludeExcludeDropdownTagHelper : TagHelper
    {
        public IEnumerable<SelectListItem>? Items { get; set; }

        public ModelExpression For { get; set; }


        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        protected IHtmlGenerator Generator { get; set; }

        public IncludeExcludeDropdownTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Items == null)
            {
                Items = [
                new SelectListItem { Value = string.Empty, Text = "No Preference" },
                new SelectListItem { Value = true.ToString(), Text = "Include" },
                new SelectListItem { Value = false.ToString(), Text = "Exclude" }
            ];
            }

            var label = Generator.GenerateLabel(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                labelText: null,
                htmlAttributes: null);

            label.AddCssClass("govuk-label");
            label.AddCssClass("exclusion_label");

            var selectList = Generator.GenerateSelect(
                ViewContext,
                For.ModelExplorer,
                null,
                For.Name,
                Items,
                false,
                null);

            selectList.AddCssClass("govuk-select");
            selectList.AddCssClass("govuk-select-custom");

            var s = new TagBuilder("div");
            s.AddCssClass("govuk-input_select__wrapper");

            s.InnerHtml.AppendHtml(label);
            s.InnerHtml.AppendHtml(selectList);

            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(s);
        }
    }
}
