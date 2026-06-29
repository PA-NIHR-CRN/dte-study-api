using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;


namespace BPOR.Rms.TagHelpers
{
    public class TemplateSelectTagHelper : TagHelper
    {
        public NotificationContactMethod ContactMethod { get; set; }
        public string SelectedTemplateId { get; set; }
        public List<Template> Templates { get; set; } = new();
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

            if (ViewContext.ModelState[For?.Name]?.Errors.Count > 0)
            {
                var errorSpan = new TagBuilder("span");
                errorSpan.AddCssClass("govuk-error-message");
                errorSpan.Attributes.Add("id", "SelectedTemplateId-error");
                errorSpan.InnerHtml.AppendHtml("<span class=\"govuk-visually-hidden\">Error:</span> " + ViewContext.ModelState[For?.Name]?.Errors[0].ErrorMessage);
                output.Content.AppendHtml(errorSpan);
            }

            var filteredTemplates = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Select template", Disabled = true, Selected = true }
            };
            filteredTemplates.AddRange(Templates
                .Where(t => t.ContactMethod == ContactMethod)
                .Select(t => new SelectListItem { Value = t.Id, Text = t.Name })
            );

            var selectList = Generator.GenerateSelect(
            ViewContext,
            For.ModelExplorer,
            SelectedTemplateId,
            For.Name,
            filteredTemplates,
            allowMultiple: false,
            htmlAttributes: new
            {
                @class = "govuk-select govuk-select-custom",
                aria_describedby = "SelectedTemplateId-hint",
                aria_errormessage = "SelectedTemplateId-error"
            });

            s.InnerHtml.AppendHtml(selectList);
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(s);
        }
    }
}

