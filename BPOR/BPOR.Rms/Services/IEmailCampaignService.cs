using BPOR.Domain.Entities;

namespace BPOR.Rms.Services;

public interface IEmailCampaignService
{
    Task SendCampaignAsync(EmailCampaign campaign, CancellationToken cancellationToken);
}
