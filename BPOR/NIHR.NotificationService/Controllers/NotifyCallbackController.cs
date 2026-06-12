using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using NIHR.NotificationService.Settings;

namespace NIHR.NotificationService.Controllers;

[AllowAnonymous]
[ApiController]
[Route("NotifyCallback")]
public class NotifyCallbackController(
    IOptions<NotificationServiceSettings> settings,
    ILogger<NotifyCallbackController> logger,
    INotificationService notificationService
) : ControllerBase
{
    [HttpPost]
    [Route("ReceiveCallback")]
    public async Task<IActionResult> ReceiveCallback([FromBody] NotifyCallbackMessage message,
        CancellationToken cancellationToken)
    {
        logger.LogDebug("NotifyCallbackMessage {@message}", message);
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        if (token != settings.Value.BearerToken)
        {
            return Unauthorized();
        }

        if (message.Reference == "PreviewEmailReference")
        {
            return Ok();
        }

        await notificationService.ProcessDeliveryCallback(message, cancellationToken);

        return Ok();
    }
}