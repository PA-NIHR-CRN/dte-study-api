using System.Threading.Tasks;
using Application.Responses.V1.FeatureFlags;

namespace Application.Contracts
{
    public interface IFeatureFlagService
    {
        Task<FeatureFlagResponse> GetPrivateBetaEmailWhitelistFeatureFlag();
    }
}