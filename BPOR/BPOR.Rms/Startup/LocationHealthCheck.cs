using Microsoft.Extensions.Diagnostics.HealthChecks;
using NIHR.Infrastructure;

namespace BPOR.Rms.Startup
{
    internal class LocationHealthCheck : IHealthCheck
    {
        private readonly IPostcodeMapper _postcodeMapper;

        public LocationHealthCheck(IPostcodeMapper postcodeMapper)
        {
            _postcodeMapper = postcodeMapper;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var location = await _postcodeMapper.GetCoordinatesFromPostcodeAsync("SW1A 1AA", cancellationToken);

            if(location is not null)
            {
                return HealthCheckResult.Healthy();
            }

            return HealthCheckResult.Degraded();
        }
    }
}