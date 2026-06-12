using BPOR.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Interfaces;
using static System.Int32;

namespace BPOR.Rms.Controllers;

[AllowAnonymous]
[ApiController]
[Route("NotifyCallback")]
public class NotifyCallbackController(
    ParticipantDbContext context,
    IEncryptionService encryptionService,
    ILogger<NotifyCallbackController> logger,
    IOptions<RmsSettings> rmsSettings,
    TimeProvider timeProvider
) : ControllerBase
{
    [HttpGet]
    [Route("registerinterest", Name = nameof(RegisterInterest))]
    public async Task<IActionResult> RegisterInterest([FromQuery] string reference, CancellationToken cancellationToken)
    {
        try
        {
            var decryptedReference = encryptionService.Decrypt(reference);

            if (TryParse(decryptedReference, out var campaignParticipantId) &&
                campaignParticipantId != 0)
            {
                var participantQuery = context.CampaignParticipant
                    .Where(p => p.Id == campaignParticipantId);

                var participant = await participantQuery.FirstOrDefaultAsync(o => o.RegisteredInterestAt == null, cancellationToken);
                if (participant is not null)
                {
                    participant.RegisteredInterestAt = timeProvider.GetLocalNow().DateTime;
                    await context.SaveChangesAsync(cancellationToken);
                }
                var informationUrl = await participantQuery.Where(p => p.Id == campaignParticipantId)
                    .Select(o => o.Campaign.FilterCriteria.Study.InformationUrl)
                    .FirstOrDefaultAsync(cancellationToken);

                if (!string.IsNullOrWhiteSpace(informationUrl) && Uri.IsWellFormedUriString(informationUrl, UriKind.Absolute))
                {
                    return Redirect(informationUrl);
                }
                else
                {
                    logger.LogWarning("Study information Url is empty or malformed: '{informationUrl}'. campaignParticipantId: {campaignParticipantId}", informationUrl, campaignParticipantId);
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