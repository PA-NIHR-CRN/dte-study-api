using BPOR.Domain.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Configuration;

namespace BPOR.Registration.Api.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/configuration")]
public class ConfigurationController(
    IConfiguration configuration,
    IOptionsMonitorCache<DevelopmentSettings> devSettingsMonitorCache) : Controller
{
    [HttpPost]
    [Route("reload")]
    [AllowAnonymous]
    public async Task<IActionResult> ReloadConfigAsync(CancellationToken cancellationToken)
    {
        if (configuration is IConfigurationRoot configurationRoot)
        {
            var secretsProviders = configurationRoot.Providers.OfType<AwsSecretsManagerConfigurationProvider>();

            foreach (var provider in secretsProviders)
            {
                await provider.ForceReloadAsync();
            }

            devSettingsMonitorCache.Clear();
        }

        return Accepted();
    }
}
