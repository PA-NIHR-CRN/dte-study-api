using BPOR.Rms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NIHR.NotificationService.Settings;

namespace BPOR.Rms.Callback.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CallbackController(
    IOptions<NotificationServiceSettings> settings,
    IHttpClientFactory httpClientFactory)
    : ControllerBase
{
    private readonly NotificationServiceSettings _settings = settings.Value;

    private static readonly Dictionary<string, string> _environmentUrls = new()
    {
        { "dev", "https://dev.studies.bepartofresearch.nihr.ac.uk/" },
        { "test", "https://test.studies.bepartofresearch.nihr.ac.uk/" },
        { "uat", "https://uat.studies.bepartofresearch.nihr.ac.uk/" },
        { "oat", "https://oat.studies.bepartofresearch.nihr.ac.uk/" },
        { "prod", "https://studies.bepartofresearch.nihr.ac.uk/" }
    };

    [HttpPost("DeliveryStatus")]
    public async Task<IActionResult> DeliveryStatus([FromBody] NotifyCallbackMessage message,
        CancellationToken cancellationToken)
    {
        if (!IsValidToken(Request.Headers.Authorization.ToString()))
        {
            return Unauthorized();
        }

        var targetUrl = GetTargetUrl(message.Reference);
        if (string.IsNullOrEmpty(targetUrl))
        {
            return BadRequest("Invalid reference format or unknown environment.");
        }

        var responseMessage = await ForwardCallbackRequest(message, targetUrl, cancellationToken);
        var responseContent = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        
        return StatusCode((int)responseMessage.StatusCode, responseContent);
    }

    private bool IsValidToken(string token)
    {
        return token.Replace("Bearer ", "") == _settings.BearerToken;
    }

    public static string GetTargetUrl(string reference)
    {
        var referenceParts = reference.Split('-');
        if (referenceParts.Length == 0 || !_environmentUrls.TryGetValue(referenceParts[0], out var targetUrl))
        {
            return string.Empty;
        }

        return $"{targetUrl}NotifyCallback/ReceiveCallback";
    }

    private async Task<HttpResponseMessage> ForwardCallbackRequest(NotifyCallbackMessage message, string targetUrl, CancellationToken cancellationToken)
    {
        var client = httpClientFactory.CreateClient();
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, targetUrl);
        requestMessage.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(message), System.Text.Encoding.UTF8, "application/json");

        CopyRequestHeaders(Request.Headers, requestMessage);

        return await client.SendAsync(requestMessage, cancellationToken);
    }

    private static void CopyRequestHeaders(IHeaderDictionary sourceHeaders, HttpRequestMessage targetRequest)
    {
        foreach (var header in sourceHeaders)
        {
            if (!targetRequest.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
            {
                targetRequest.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }
        }
    }
}
