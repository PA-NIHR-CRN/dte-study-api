using System;
using System.IO;
using Microsoft.Extensions.Configuration.Json;
using System.Text;
using System.Threading.Tasks;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace NIHR.Infrastructure.Configuration
{
    public class AwsSecretsManagerConfigurationProvider : JsonStreamConfigurationProvider
    {
        private readonly string _secretName;
        private readonly Func<IAmazonSecretsManager> _secretsManagerClientFactory;

        public AwsSecretsManagerConfigurationProvider(JsonStreamConfigurationSource source, string secretName,
            Func<IAmazonSecretsManager> secretsManagerClientFactory) : base(source)
        {
            _secretName = secretName;
            _secretsManagerClientFactory = secretsManagerClientFactory;
        }

        public override void Load()
        {
            var secretsManager = _secretsManagerClientFactory.Invoke();

            var response = secretsManager.GetSecretValueAsync(new GetSecretValueRequest { SecretId = _secretName })
                .GetAwaiter().GetResult();

            var jsonStream = response.SecretBinary;

            if (response.SecretString != null)
            {
                jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(response.SecretString));
            }

            if (jsonStream != null)
            {
                base.Load(jsonStream); // Stream will be disposed correctly in here.
            }
            else
            {
                throw new AmazonSecretsManagerException(
                    $"Failed to load Secrets Manager secret with key {_secretName}.");
            }
        }

        public Task ForceReloadAsync()
        {
            Load();
            return Task.CompletedTask;
        }
    }
}
