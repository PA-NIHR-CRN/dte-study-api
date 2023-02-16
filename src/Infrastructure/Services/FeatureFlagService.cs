using System;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Mappings.FeatureFlags;
using Application.Responses.V1.FeatureFlags;
using Dte.Common.Exceptions;
using Dte.Reference.Data.Api.Client;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class FeatureFlagService : IFeatureFlagService
    {
        private readonly IReferenceDataApiClient _client;
        private readonly ILogger<FeatureFlagService> _logger;

        public FeatureFlagService(IReferenceDataApiClient client, ILogger<FeatureFlagService> logger)
        {
            _client = client;
            _logger = logger;
        }
        
        public async Task<FeatureFlagResponse> GetPrivateBetaEmailWhitelistFeatureFlag()
        {
            try
            {
                return FeatureFlagMapper.MapTo(await _client.GetFeatureFlagAsync("StudyService", "PrivateBetaEmailWhitelist"));
            }
            catch (HttpServiceException ex)
            {
                _logger.LogError(ex, "HTTP Error getting feature flag for StudyService : PrivateBetaEmailWhitelist");
                return new FeatureFlagResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown Error getting feature flag for StudyService : PrivateBetaEmailWhitelist");
                return new FeatureFlagResponse();
            }
        }
    }
}