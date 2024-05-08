using System;
using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;

namespace NIHR.Infrastructure.Configuration
{
    public class AwsSecretsManagerConfigurationSource : JsonStreamConfigurationSource
    {
        private readonly string _secretName;
        private readonly Func<IAmazonSecretsManager> _secretsManagerClientFactory;
        private readonly ILogger _logger;

        public AwsSecretsManagerConfigurationSource(string secretName,
            Func<IAmazonSecretsManager> secretsManagerClientFactory, ILogger logger)
        {
            _secretName = secretName;
            _secretsManagerClientFactory = secretsManagerClientFactory;
            _logger = logger;
        }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            _logger.LogCritical("Building AwsSecretsManagerConfigurationProvider for secret {SecretName}", _secretName);
            return new AwsSecretsManagerConfigurationProvider(_secretsManagerClientFactory(), _secretName, _logger);
        }
    }
}
