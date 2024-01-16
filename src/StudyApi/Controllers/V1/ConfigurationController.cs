using Application.Settings;
using Dte.Common.Lambda.SecretsManagement.AwsSecretsManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApi.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/configuration")]
public class ConfigurationController: Controller
{
    private readonly IConfiguration _configuration;
    private readonly IOptionsMonitorCache<DevSettings> _devSettingsMonitorCache;

    public ConfigurationController(IConfiguration configuration, IOptionsMonitorCache<DevSettings> devSettingsMonitorCache)
    {
        _configuration = configuration;
        _devSettingsMonitorCache = devSettingsMonitorCache;
    }

    [HttpPost]
    [Route("reload")]
    [AllowAnonymous]
    public async Task<IActionResult> ReloadConfigAsync(CancellationToken cancellationToken)
    {
        if (_configuration is IConfigurationRoot configurationRoot)
        {
            var secretsProviders = configurationRoot.Providers
                .Where(x => x is SecretsManagerConfigurationProvider)
                .Select(x => x as SecretsManagerConfigurationProvider);

            foreach (var provider in secretsProviders)
            {
                await provider.ForceReloadAsync(cancellationToken);
            }
            _devSettingsMonitorCache.Clear();

        }

        return Accepted();
    }
}
