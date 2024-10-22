using System.Text;
using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BPOR.Rms.TagHelpers
{
    public enum ContactPreferenceType
    {
        Email,
        Letter
    }

    public class TemplateSelectTagHelper : TagHelper
    {
        public ContactPreferenceType ContactPreference { get; set; }

        public string SelectedTemplateId { get; set; }

        public IEnumerable<SelectListItem>? EmailTemplates { get; set; }

        public IEnumerable<SelectListItem>? LetterTemplates { get; set; }

        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; set; }

        public TemplateSelectTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            var s = new TagBuilder("div");
            s.AddCssClass("govuk-input_select__wrapper");


            if (ContactPreference == ContactPreferenceType.Email)
            {
                var label = Generator.GenerateLabel(
                    ViewContext,
                    For.ModelExplorer,
                    For.Name,
                    labelText: "Select an email template",
                    htmlAttributes: null);

                label.AddCssClass("govuk-label");

                var selectList = Generator.GenerateSelect(
                ViewContext,
                For.ModelExplorer,
                null,
                For.Name,
                EmailTemplates,
                allowMultiple: false,
                htmlAttributes: null);

                selectList.AddCssClass("govuk-select");

                s.InnerHtml.AppendHtml(label);
                s.InnerHtml.AppendHtml(selectList);
            }
            else if (ContactPreference == ContactPreferenceType.Letter)
            {
                var label = Generator.GenerateLabel(
                   ViewContext,
                   For.ModelExplorer,
                   For.Name,
                   labelText: "Select a letter template",
                   htmlAttributes: null);

                label.AddCssClass("govuk-label");

                var selectList = Generator.GenerateSelect(
                ViewContext,
                For.ModelExplorer,
                null,
                For.Name,
                LetterTemplates,
                allowMultiple: false,
                htmlAttributes: null);

                selectList.AddCssClass("govuk-select");

                s.InnerHtml.AppendHtml(label);
                s.InnerHtml.AppendHtml(selectList);
            }
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(s);
        }
    }
}

