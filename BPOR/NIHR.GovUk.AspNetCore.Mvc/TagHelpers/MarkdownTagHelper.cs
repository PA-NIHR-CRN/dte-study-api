using System.Net;
using Markdig;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NIHR.GovUk.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement("markdown")]
public class MarkdownTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        try
        {
            // Get the inner content (Markdown text)
            var rawHtmlMarkdown = (output.GetChildContentAsync().Result).GetContent();
            var unescapedMarkdown = WebUtility.HtmlDecode(rawHtmlMarkdown);
            // Convert Markdown to HTML using Markdig
            var htmlContent = Markdown.ToHtml(unescapedMarkdown ?? string.Empty);

            // Replace the tag with the generated HTML
            output.TagName = null; // Remove <markdown> wrapper
            output.Content.SetHtmlContent(htmlContent);
        }
        catch (Exception ex)
        {
            // In case of error, output the raw content
            output.TagName = "pre";
            output.Content.SetContent($"Markdown render error: {ex.Message}");
        }
    }
}