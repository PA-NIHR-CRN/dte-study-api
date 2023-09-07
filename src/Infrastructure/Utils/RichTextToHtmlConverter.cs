using System.Linq;
using System.Text;
using Application.Contracts;
using Application.Responses.V1;
using Application.Settings;

namespace Infrastructure.Utils;

public class RichTextToHtmlConverter : IRichTextToHtmlConverter
{
    private readonly EmailSettings _emailSettings;

    public RichTextToHtmlConverter(EmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
    }

    public string Convert(RichTextNode node)
    {
        StringBuilder html = new StringBuilder();
        var baseUrl = _emailSettings.WebAppBaseUrl;

        switch (node.NodeType)
        {
            case "document":
                foreach (var contentNode in node.Content)
                {
                    html.Append(Convert(contentNode));
                }

                break;

            case "heading-1":
                html.Append(
                    $"<span style='font-size: 24px; color: #193e72'><strong>{node.Content.FirstOrDefault()?.Value}</strong></span>");
                break;

            case "paragraph":
                foreach (var contentNode in node.Content)
                {
                    html.Append(
                        $"<p style='display: block; margin: 13px 0'>{Convert(contentNode)}</p>");
                }

                break;

            case "text":
                html.Append($"<span style='font-size: 16px'>{node.Value}</span>");
                break;

            case "hyperlink":
                var uri = node.Data["uri"].ToString();
                if (uri == null) break;
                html.Append("<span style='font-size: 16px'>");
                if (uri.StartsWith("mailto") || uri.StartsWith("http"))
                {
                    html.Append(
                        $"<a href='{uri}' style='color: #193e72; text-decoration: none'>{node.Content.FirstOrDefault()?.Value}</a>");
                }
                else 
                {
                    html.Append(
                        $"<a href='{baseUrl}{uri}' style='color: #193e72; text-decoration: none'>{baseUrl}{node.Content.FirstOrDefault()?.Value}</a>");
                }
                html.Append("</span>");
                break;
        }

        return html.ToString();
    }
}