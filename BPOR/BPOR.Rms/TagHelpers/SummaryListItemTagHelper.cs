using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BPOR.Rms.TagHelpers;

[HtmlTargetElement("summary-list-item", Attributes = "asp-for")]
public class SummaryListItemTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")]
    public ModelExpression For { get; set; }

    [HtmlAttributeName("asp-action-url")]
    public string ActionUrl { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.SetAttribute("class", "govuk-summary-list__row");
        
        var displayName = For.Metadata.DisplayName ?? For.Name;
        var displayValue = For.Model?.ToString() ?? string.Empty;

        var title = new TagBuilder("dt");
        title.AddCssClass("govuk-summary-list__key");
        title.InnerHtml.Append(displayName);

        var value = new TagBuilder("dd");
        value.AddCssClass("govuk-summary-list__value");
        value.InnerHtml.Append(displayValue);

        output.Content.AppendHtml(title);
        output.Content.AppendHtml(value);

        if (!string.IsNullOrWhiteSpace(ActionUrl))
        {
            var changeLink = new TagBuilder("dd");
            changeLink.AddCssClass("govuk-summary-list__actions");
            
            var changeLinkAnchor = new TagBuilder("a");
            changeLinkAnchor.AddCssClass("govuk-link");
            changeLinkAnchor.Attributes.Add("href", ActionUrl);
            
            var changeLinkSpan = new TagBuilder("span");
            changeLinkSpan.AddCssClass("govuk-visually-hidden");
            changeLinkSpan.InnerHtml.Append(displayName);
            
            changeLinkAnchor.InnerHtml.Append("Change");
            changeLinkAnchor.InnerHtml.AppendHtml(changeLinkSpan);
            
            changeLink.InnerHtml.AppendHtml(changeLinkAnchor);

            output.Content.AppendHtml(changeLink);
        }
    }
    
}
