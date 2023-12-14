using Amazon.Extensions.NETCore.Setup;
using Amazon.SecretsManager.Model;
using Dte.Common.Lambda.Extensions;
using Dynamo.Stream.Handler.Helpers;
using Microsoft.Extensions.Configuration;

namespace Dynamo.Stream.Handler.Extensions;

public static class AwsSecretsConfigurationBuilderExtensions
{
    private const string AwsSecretManagerSecretName = "AWS_SECRET_MANAGER_SECRET_NAME";

    public static IConfigurationBuilder AddAwsSecrets(this IConfigurationBuilder configurationBuilder)
    {
        // return if in development
        if (EnvironmentHelper.IsDevelopment())
        {
            return configurationBuilder;
        }

        var awsSecretsName = Environment.GetEnvironmentVariable(AwsSecretManagerSecretName);

        if (string.IsNullOrWhiteSpace(awsSecretsName))
        {
            return configurationBuilder;
        }

        var allowedSecretNames = new[] { awsSecretsName };

        return configurationBuilder.AddSecretsManager(configurator: opts =>
        {
            opts.SecretFilter = entry => HasValue(allowedSecretNames, entry);
            opts.KeyGenerator = (entry, key) => GenerateKey(allowedSecretNames, key);
        });
    }

    // Only load entries that start with any of the allowed prefixes
    private static bool HasValue(IEnumerable<string> allowedSecretNames, SecretListEntry entry)
    {
        return allowedSecretNames.Any(prefix =>
            string.Equals(prefix, entry.Name, StringComparison.CurrentCultureIgnoreCase));
    }

    // Strip the prefix and replace '__' with ':'
    private static string GenerateKey(IEnumerable<string> allowedSecretNames, string entryName)
    {
        return entryName[
            (allowedSecretNames.First(x => entryName.StartsWith(x, StringComparison.CurrentCultureIgnoreCase)).Length +
             1)..].Replace("__", ":");
    }
}
