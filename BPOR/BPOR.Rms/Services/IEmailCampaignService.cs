using BPOR.Domain.Entities;

namespace BPOR.Rms.Services;

public interface IEmailCampaignService
{
    Task SendCampaignAsync(int emailCampaignId, CancellationToken cancellationToken = default);
}
