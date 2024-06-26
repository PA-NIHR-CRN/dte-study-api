using Microsoft.AspNetCore.Razor.TagHelpers;
using NIHR.Infrastructure.Interfaces;

namespace BPOR.Rms.TagHelpers;

public class WithContentTagHelper(IContentProvider contentProvider) : TagHelper
{
    [HtmlAttributeName("key")]
    public string? Key { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (string.IsNullOrWhiteSpace(Key))
        {
            return;
        }

        var html = await contentProvider.GetContentAsync(Key);
        
        output.TagName = "div";
        output.Attributes.Add("class", "has-cms-content");
        output.Content.SetHtmlContent($"<span class=\"is-cms-content\">{html}</span>");
    }
}
