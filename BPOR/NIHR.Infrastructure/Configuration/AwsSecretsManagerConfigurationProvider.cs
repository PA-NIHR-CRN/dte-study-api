using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NIHR.Infrastructure.Configuration
{
    public class AwsSecretsManagerConfigurationProvider : ConfigurationProvider
    {
        private readonly IAmazonSecretsManager _client;
        private readonly string _secretName;

        public AwsSecretsManagerConfigurationProvider(IAmazonSecretsManager client, string secretName)
        {
            _client = client;
            _secretName = secretName;
        }

        public override async void Load()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var response = await _client.GetSecretValueAsync(new GetSecretValueRequest { SecretId = _secretName });

            var secretString = response.SecretString;
            if (string.IsNullOrEmpty(secretString))
            {
                throw new InvalidOperationException($"Secret {_secretName} is empty or not found.");
            }

            ParseSecret(secretString);
        }

        private void ParseSecret(string secretString)
        {
            try
            {
                var jToken = JToken.Parse(secretString);
                var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                ExtractValues(jToken, prefix: string.Empty, data);
                Data = data;
            }
            catch (JsonReaderException ex)
            {
                throw new FormatException(
                    $"The secret value for {_secretName} is not in a recognized format. Expected JSON.", ex);
            }
        }

        private static void ExtractValues(JToken token, string prefix, IDictionary<string, string> data)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (var child in token.Children<JProperty>())
                    {
                        ExtractValues(child.Value, JoinKey(prefix, child.Name), data);
                    }

                    break;
                case JTokenType.Array:
                    var index = 0;
                    foreach (var child in token.Children())
                    {
                        ExtractValues(child, JoinKey(prefix, index.ToString()), data);
                        index++;
                    }

                    break;
                default:
                    data[prefix] = token.ToString();
                    break;
            }
        }

        private static string JoinKey(string prefix, string key)
        {
            return string.IsNullOrEmpty(prefix) ? key : $"{prefix}:{key.Replace("__", ":")}";
        }

        public async Task ForceReloadAsync()
        {
            await LoadAsync();
            OnReload();
        }
    }
}
