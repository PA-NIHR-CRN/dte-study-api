using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

namespace NIHR.NotificationService.GovUkNotify.Controllers;

[ApiKeyAuthentication]
[ApiController]
[Route("NotifyCallback")]
public class NotifyCallbackController(
    ILogger<NotifyCallbackController> logger,
    INotificationService notificationService
) : ControllerBase
{
    public const string RoleNameGovUkNotifyCallback = "GovUkNotifyCallback";

    [HttpPost]
    [Route("ReceiveCallback")]
    [Authorize(Roles = RoleNameGovUkNotifyCallback)]
    public async Task<IActionResult> ReceiveCallback([FromBody] NotifyCallbackMessage message,
        CancellationToken cancellationToken)
    {
        logger.LogDebug("NotifyCallbackMessage {@message}", message);

        if (message.Reference == "PreviewEmailReference")
        {
            return Ok();
        }

        await notificationService.ProcessDeliveryCallback(message, cancellationToken);

        return Ok();
    }
}