using System.Text;
using Dte.Common;
using Dte.Common.Contracts;
using Dte.Common.Models;
using Microsoft.Extensions.Options;

namespace BPOR.Rms.Services;

public class RichTextToHtmlService(IOptions<AppSettings> appSettings) : IRichTextToHtmlService
{
    private readonly AppSettings _appSettings = appSettings.Value;

    public string Convert(RichTextNode node)
    {
        if (node == null)
        {
            return string.Empty;
        }

        var sb = new StringBuilder();

        switch (node.NodeType)
        {
            case "document":
                foreach (var contentNode in node.Content)
                {
                    sb.Append(Convert(contentNode));
                }

                break;

            case "heading-1":
                sb.Append(
                    $"<h1 class='govuk-heading-xl'>{node.Content.FirstOrDefault()?.Value}</h1>");
                break;
            case "heading-2":
                sb.Append(
                    $"<h2 class='govuk-heading-xl'>{node.Content.FirstOrDefault()?.Value}</h2>");
                break;
            case "heading-3":
                sb.Append(
                    $"<h3 class='govuk-heading-xl'>{node.Content.FirstOrDefault()?.Value}</h3>");
                break;
            case "heading-4":
                sb.Append(
                    $"<h4 class='govuk-heading-xl'>{node.Content.FirstOrDefault()?.Value}</h4>");
                break;

            case "paragraph":
                sb.Append("<p class='govuk-body'>");
                foreach (var contentNode in node.Content)
                {
                    sb.Append(Convert(contentNode));
                }

                sb.Append("</p>");
                break;

            case "text":
                sb.Append($"<span>{node.Value}</span>");
                break;
            case "hr":
                sb.Append("<hr />");
                break;

            case "hyperlink":
                var uri = node.Data["uri"].ToString();
                if (uri == null) break;
                sb.Append("<span>");
                if (uri.StartsWith("mailto") || uri.StartsWith("http"))
                {
                    sb.Append(
                        $"<a href='{uri}' style='color: #193e72; text-decoration: none'>{node.Content.FirstOrDefault()?.Value}</a>");
                }
                else
                {
                    sb.Append(
                        $"<a href='{_appSettings.WebAppBaseUrl}{uri}' style='color: #193e72; text-decoration: none'>{_appSettings.WebAppBaseUrl}{node.Content.FirstOrDefault()?.Value}</a>");
                }

                sb.Append("</span>");
                break;
        }

        return sb.ToString();
    }
}
