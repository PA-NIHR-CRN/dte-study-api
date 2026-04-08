using BPOR.Domain.Entities;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Reflection;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Controllers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text.Encodings.Web;
using BPOR.Rms.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BPOR.Rms.TagHelpers;

public class SummaryListItemTagHelper(ICurrentUserProvider<User> currentUserProvider, LinkGenerator linkGenerator) : TagHelper
{
    public ModelExpression For { get; set; }

    [HtmlAttributeName("show-when")]
    public bool Show { get; set; } = true;
    
    [HtmlAttributeName("show-admin-when")]
    public bool ShowAdmin { get; set; } = true;
    
    [HtmlAttributeName("show-researcher-when")]
    public bool ShowResearcher { get; set; } = true;
    
    [HtmlAttributeName("edit-when")]
    public bool Editable { get; set; } = true;

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var studyRelativeName = For.Name.Split('.').Last();
        var studyEdit = For.Metadata.ContainerType?.GetProperty(studyRelativeName)?.GetCustomAttribute<StudyEditAttribute>();
        var researcherEdit = For.Metadata.ContainerType?.GetProperty(studyRelativeName)?.GetCustomAttribute<ResearcherEditAttribute>();

        var viewModel = For.ModelExplorer.Container.Model as StudyDetailsViewModel;

        var visible = Show;
        visible &= !currentUserProvider.IsAdmin() || ShowAdmin;
        visible &= !currentUserProvider.IsResearcher() || ShowResearcher;

        if (visible)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("govuk-summary-list__row", HtmlEncoder.Default);

            var displayName = For.Metadata.DisplayName ?? For.Name;
            var displayValue = For.GetDisplayString();
            
            var title = new TagBuilder("dt");
            title.AddCssClass("govuk-summary-list__key");

            title.InnerHtml.Append(displayName);

            var value = new TagBuilder("dd");
            value.AddCssClass("govuk-summary-list__value");
            value.InnerHtml.Append(displayValue);

            output.Content.AppendHtml(title);
            output.Content.AppendHtml(value);

            var field = studyEdit?.FieldId;
            var controller = nameof(StudyController);
            
            if (researcherEdit is not null)
            {
                field = researcherEdit.FieldId;
                controller = nameof(ResearcherController);
            }
            
            if (field is not null &&
                viewModel is not null &&
                Editable && 
                currentUserProvider.IsAdmin())
            {
                var url = linkGenerator.GetUriByAction(ViewContext.HttpContext, "Edit", controller.Replace("Controller", ""), new { id = viewModel.Study.Id, field });

                var changeLink = new TagBuilder("dd");
                changeLink.AddCssClass("govuk-summary-list__actions");

                var changeLinkAnchor = new TagBuilder("a");
                changeLinkAnchor.AddCssClass("govuk-link");
                changeLinkAnchor.Attributes.Add("href", url);

                var changeLinkSpan = new TagBuilder("span");
                changeLinkSpan.AddCssClass("govuk-visually-hidden");
                changeLinkSpan.InnerHtml.Append(displayName);

                changeLinkAnchor.InnerHtml.Append("Change");
                changeLinkAnchor.InnerHtml.AppendHtml(changeLinkSpan);

                changeLink.InnerHtml.AppendHtml(changeLinkAnchor);

                output.Content.AppendHtml(changeLink);
            }
        }
        else
        {
            output.SuppressOutput();
        }
    }
}
