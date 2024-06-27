using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Interfaces;
using NIHR.NotificationService.Settings;
using LuhnNet;
using static System.Int32;

namespace BPOR.Rms.Controllers;

[AllowAnonymous]
[ApiController]
[Route("NotifyCallback")]
public class NotifyCallbackController(
    ParticipantDbContext context,
    IOptions<NotificationServiceSettings> settings,
    IRefDataService refDataService,
    IEncryptionService encryptionService,
    TimeProvider timeProvider
) : ControllerBase
{
    [HttpPost]
    [Route("ReceiveCallback")]
    public async Task<IActionResult> ReceiveCallback([FromBody] NotifyCallbackMessage message,
        CancellationToken cancellationToken)
    {
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        if (token != settings.Value.BearerToken)
        {
            return Unauthorized();
        }

        if (message.Reference == "PreviewEmailReference")
        {
            return Ok();
        }

        if (!TryParse(message.Reference, out var emailCampaignParticipantId))
        {
            return BadRequest("Invalid reference.");
        }

        var participantEmail = await context.EmailCampaignParticipants
            .Where(x => x.Id == emailCampaignParticipantId)
            .FirstOrDefaultAsync(cancellationToken);

        if (participantEmail == null)
        {
            return NotFound();
        }

        switch (message.Status)
        {
            case "delivered":
                participantEmail.DeliveredAt = DateTime.UtcNow;
                participantEmail.DeliveryStatusId =
                    refDataService.GetEmailDeliveryStatusId(EmailDeliveryStatus.Delivered);
                break;
            case "permanent-failure":
            case "technical-failure":
                participantEmail.DeliveryStatusId = refDataService.GetEmailDeliveryStatusId(EmailDeliveryStatus.Failed);
                break;
            default:
                return BadRequest("Invalid status.");
        }

        await context.SaveChangesAsync(cancellationToken);

        return Ok();
    }

    [HttpGet]
    [Route("registerinterest")]
    public async Task<IActionResult> RegisterInterest([FromQuery] string reference, CancellationToken cancellationToken)
    {
        var decryptedReference = encryptionService.Decrypt(reference);

        if (!TryParse(decryptedReference, out var emailCampaignParticipantId) ||
            emailCampaignParticipantId == 0)
        {
            return BadRequest("Invalid reference.");
        }

        var participant = await context.EmailCampaignParticipants
            .Where(p => p.Id == emailCampaignParticipantId && p.RegisteredInterestAt == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (participant == null)
        {
            return NotFound();
        }

        participant.RegisteredInterestAt = timeProvider.GetLocalNow().DateTime;
        await context.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}

public class NotifyCallbackMessage
{
    public string Id { get; set; }
    public string Reference { get; set; }
    public string To { get; set; }
    public string Status { get; set; }
    public string Created_at { get; set; }
    public string Completed_at { get; set; }
    public string Sent_at { get; set; }
    public string Notification_type { get; set; }
    public string Template_id { get; set; }
    public int Template_version { get; set; }
}
