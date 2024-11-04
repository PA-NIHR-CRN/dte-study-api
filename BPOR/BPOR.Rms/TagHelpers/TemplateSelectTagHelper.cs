using System.Net.NetworkInformation;
using System.Text;
using Amazon.DynamoDBv2;
using BPOR.Rms.Models.Email;
using BPOR.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Notify.Models.Responses;


namespace BPOR.Rms.TagHelpers
{
    public class TemplateSelectTagHelper : TagHelper
    {
        public ContactMethods ContactMethod { get; set; }

        public string SelectedTemplateId { get; set; }

        public List<TemplateResponse> templates { get; set; } = new List<TemplateResponse>();

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

            var filteredTemplates = templates
            .Where(t => (ContactMethod == ContactMethods.Email && t.@type == "email") ||
                        (ContactMethod == ContactMethods.Letter && t.@type == "letter"))
            .Select(t => new SelectListItem { Value = t.id.ToString(), Text = t.name })
            .ToList();


            var labelText = ContactMethod == ContactMethods.Email ? "Select an email template" : "Select a letter template";
            var label = Generator.GenerateLabel(
                    ViewContext,
                    For.ModelExplorer,
                    For.Name,
                    labelText,
                    htmlAttributes: null);
                label.AddCssClass("govuk-label");

                var selectList = Generator.GenerateSelect(
                ViewContext,
                For.ModelExplorer,
                SelectedTemplateId,
                For.Name,
                filteredTemplates,
                allowMultiple: false,
                htmlAttributes: null);
                selectList.AddCssClass("govuk-select");

                s.InnerHtml.AppendHtml(label);
                s.InnerHtml.AppendHtml(selectList);

                output.TagMode = TagMode.StartTagAndEndTag;
                output.Content.SetHtmlContent(s);
        }
    }
}

