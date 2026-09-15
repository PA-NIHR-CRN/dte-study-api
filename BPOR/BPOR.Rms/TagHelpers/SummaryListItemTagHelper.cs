using BPOR.Domain.Entities;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text.Encodings.Web;
using NIHR.Infrastructure.AspNetCore;

namespace BPOR.Rms.TagHelpers;

public class SummaryListItemTagHelper(ICurrentUserProvider<User> currentUserProvider, LinkGenerator linkGenerator) : TagHelper
{
    public ModelExpression For { get; set; }
    
    [HtmlAttributeName("change-url")]
    public string? ChangeUrl { get; set; }

    [HtmlAttributeName("show-when")]
    public bool Show { get; set; } = true;
    
    [HtmlAttributeName("hide-when-null")]
    public bool HideWhenNull { get; set; } = false;
    
    [HtmlAttributeName("hide-from-researcher-when-null")]
    public bool HideFromResearcherWhenNull { get; set; } = false;
    
    [HtmlAttributeName("edit-when")]
    public bool Editable { get; set; } = true;
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var visible = Show;
        visible &= !HideWhenNull || For.Model != null;
        visible &= !currentUserProvider.IsResearcher() || !HideFromResearcherWhenNull || For.Model != null;

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
            
            if (!string.IsNullOrEmpty(ChangeUrl) &&
                Editable && 
                currentUserProvider.IsAdmin())
            {
                var changeLink = new TagBuilder("dd");
                changeLink.AddCssClass("govuk-summary-list__actions");

                var changeLinkAnchor = new TagBuilder("a");
                changeLinkAnchor.AddCssClass("govuk-link");
                changeLinkAnchor.Attributes.Add("href", ChangeUrl);

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
