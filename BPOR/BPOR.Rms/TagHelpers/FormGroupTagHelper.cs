using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace BPOR.Rms.TagHelpers
{
    public class FormGroupTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }

        public ModelExpression? For { get; set; }

        public bool IncludeChildErrors { get; set; }

        private readonly IHtmlGenerator _generator;

        public FormGroupTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (For == null)
            {
                return;
            }

            if (ViewContext == null)
            {
                return;
            }

            output.TagName = "div";
            output.AddClass("govuk-form-group", HtmlEncoder.Default);

            var label = _generator.GenerateLabel(ViewContext, For.ModelExplorer, For.Name, null, new { @class = "govuk-label govuk-label--l" });


            var modelName = label.Attributes["for"]?.Replace('_', '.') ?? string.Empty;

            List<string> modelNames = [modelName];

            if (IncludeChildErrors)
            {
                var childPrefix = modelName + ".";

                modelNames.AddRange(ViewContext.ViewData.ModelState.Where(x => x.Key.StartsWith(childPrefix)).Select(x => x.Key));
            }



            var modelState = ViewContext.ViewData.ModelState.Where(x => modelNames.Contains(x.Key));

            if (modelState?.Where(x => x.Value?.Errors.Count > 0).Any() ?? false)
            {
                output.AddClass("govuk-form-group--error", HtmlEncoder.Default);
            }

            output.PreContent.AppendHtml("""<h3 class="govuk-label-wrapper">""");

            output.PreContent.AppendHtml(label);

            output.PreContent.AppendHtml("""</h3>""");

            if (!string.IsNullOrEmpty(For.Metadata.Description))
            {
                output.PreContent.AppendHtml($"""
                    <div class="govuk-hint">
                        {_generator.Encode(For.Metadata.Description)}
                    </div>
                """);
            }

            foreach (var name in modelNames)
            {
                if (ViewContext.ViewData.ModelState[name]?.Errors.Count > 0)
                {
                    var validationMessage = _generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, name, null, null, null);

                    output.PreContent.AppendHtml($"""
                    <span class="govuk-error-message">
                        <span class="govuk-visually-hidden">Error:</span>
                """);

                    output.PreContent.AppendHtml(validationMessage);
                    output.PreContent.AppendHtml("</span>");
                }
            }
        }
    }
}
