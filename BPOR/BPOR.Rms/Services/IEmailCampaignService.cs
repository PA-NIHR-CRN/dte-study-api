using BPOR.Domain.Entities;
using BPOR.Rms.Models.Email;

namespace BPOR.Rms.Services;

public interface ICampaignService
{
    Task SendCampaignAsync(EmailServiceQueueItem item, CancellationToken cancellationToken = default);
}
