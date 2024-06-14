using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using BPOR.Rms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Interfaces;
using NIHR.NotificationService.Settings;
using LuhnNet;

namespace BPOR.Rms.Controllers;

[AllowAnonymous]
[ApiController]
[Route("NotifyCallback")]
public class NotifyCallbackController(
    ParticipantDbContext context,
    IOptions<NotificationServiceSettings> settings,
    IRefDataService refDataService,
    IEncryptionService encryptionService
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

        if (!int.TryParse(message.Reference, out var emailCampaignParticipantId))
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
        
        if (!Luhn.IsValid(decryptedReference))
        {
            return BadRequest("Invalid reference.");
        }
        var participant = await context.EmailCampaignParticipants
            .Where(x => x.ParticipantId == context.StudyParticipantEnrollment
                .Where(x => x.Reference == decryptedReference)
                .Select(x => x.ParticipantId)
                .FirstOrDefault()).FirstOrDefaultAsync(cancellationToken);

        if (participant == null)
        {
            return NotFound();
        }

        participant.RegisteredInterestAt = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}
