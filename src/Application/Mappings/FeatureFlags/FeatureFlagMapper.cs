using Application.Responses.V1.FeatureFlags;

namespace Application.Mappings.FeatureFlags
{
    public static class FeatureFlagMapper
    {
        public static FeatureFlagResponse MapTo(Dte.Reference.Data.Api.Client.Responses.FeatureFlagResponse source)
        {
            return new FeatureFlagResponse
            {
                Enabled = source.Enabled,
                Found = source.Found
            };
        }
    }
}