using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Rms.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Interfaces;
using NIHR.NotificationService.Settings;
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
    ILogger<NotifyCallbackController> logger,
    IOptions<RmsSettings> rmsSettings,
    TimeProvider timeProvider
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

        if (!TryParse(message.Reference, out var emailCampaignParticipantId))
        {
            logger.LogError("Invalid callback reference {reference}", message.Reference);
            return BadRequest("Invalid reference.");
        }

        var participantEmail = await context.EmailCampaignParticipants
            .Where(x => x.Id == emailCampaignParticipantId)
            .FirstOrDefaultAsync(cancellationToken);

        if (participantEmail == null)
        {
            logger.LogError("EmailCampaignParticipant not found for Id {emailCampaignParticipantId}.", emailCampaignParticipantId);
            return NotFound();
        }

        switch (message.Status)
        {
            case "accepted":
            case "received":
            case "cancelled":
            case "pending-virus-check":
            case "virus-scan-failed":
            case "validation-failed":
            case "created":
            case "sending":
            case "pending":
            case "sent":
                break;
            case "delivered":
                participantEmail.DeliveredAt = DateTime.UtcNow;
                participantEmail.DeliveryStatusId =
                    refDataService.GetEmailDeliveryStatusId(EmailDeliveryStatus.Delivered);
                break;
            case "temporary-failure":
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
    [Route("registerinterest", Name = nameof(RegisterInterest))]
    public async Task<IActionResult> RegisterInterest([FromQuery] string reference, CancellationToken cancellationToken)
    {
        try
        {
            var decryptedReference = encryptionService.Decrypt(reference);

            if (TryParse(decryptedReference, out var emailCampaignParticipantId) &&
                emailCampaignParticipantId != 0)
            {
                var participantQuery = context.EmailCampaignParticipants
                    .Where(p => p.Id == emailCampaignParticipantId);

                var participant = await participantQuery.FirstOrDefaultAsync(o => o.RegisteredInterestAt == null, cancellationToken);
                if (participant is not null)
                {
                    participant.RegisteredInterestAt = timeProvider.GetLocalNow().DateTime;
                    await context.SaveChangesAsync(cancellationToken);
                }
                var informationUrl = await participantQuery.Where(p => p.Id == emailCampaignParticipantId)
                    .Select(o => o.EmailCampaign.FilterCriteria.Study.InformationUrl)
                    .FirstOrDefaultAsync(cancellationToken);

                if (!string.IsNullOrWhiteSpace(informationUrl) && Uri.IsWellFormedUriString(informationUrl, UriKind.Absolute))
                {
                    return Redirect(informationUrl);
                }
                else
                {
                    logger.LogWarning("Study information Url is empty or malformed: '{informationUrl}'. emailCampaignParticipantId: {emailCampaignParticipantId}", informationUrl, emailCampaignParticipantId);
                }
            }
        }
        catch (Exception ex)
        {
            // This is a public facing endpoint.
            // We don't want to surface exceptions
            // to the general public.
            // Use the fallback URL if anything goes wrong.
            logger.LogError(ex, ex.Message);
        }

        return Redirect(rmsSettings.Value.StudyInformationFallbackUrl);
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
