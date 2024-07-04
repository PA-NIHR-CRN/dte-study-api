using BPOR.Domain.Entities;
using BPOR.Rms.Models.Email;

namespace BPOR.Rms.Services;

public interface IEmailCampaignService
{
    Task SendCampaignAsync(EmailServiceQueueItem item, CancellationToken cancellationToken = default);
}
