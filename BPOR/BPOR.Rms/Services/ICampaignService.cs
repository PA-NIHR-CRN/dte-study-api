using BPOR.Domain.Entities;
using BPOR.Rms.Models.Email;

namespace BPOR.Rms.Services;

public interface ICampaignService
{
    Task SendCampaignAsync(ServiceQueueItem item, CancellationToken cancellationToken = default);
}
