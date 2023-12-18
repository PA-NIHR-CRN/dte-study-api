using Amazon.SecretsManager.Model;
using Dte.Common.Lambda.Extensions;
using Dynamo.Stream.Handler.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dynamo.Stream.Handler.Extensions;

public static class AwsSecretsConfigurationBuilderExtensions
{
    private const string AwsSecretManagerSecretName = "AWS_SECRET_MANAGER_SECRET_NAME";

    public static IConfigurationBuilder AddAwsSecrets(this IConfigurationBuilder configurationBuilder,
        IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetService<ILoggerFactory>()!.CreateLogger("AwsSecretsConfiguration");
        try
        {
            // Log that the method has started
            logger.LogInformation("Starting AddAwsSecrets");

            // return if in development
            if (EnvironmentHelper.IsDevelopment())
            {
                logger.LogInformation("In development environment, skipping AddAwsSecrets");
                return configurationBuilder;
            }

            var awsSecretsName = Environment.GetEnvironmentVariable(AwsSecretManagerSecretName);
            logger.LogInformation("AWS_SECRET_MANAGER_SECRET_NAME: {AwsSecretsName}", awsSecretsName);

            if (string.IsNullOrWhiteSpace(awsSecretsName))
            {
                logger.LogWarning("AWS secrets name is not set");
                return configurationBuilder;
            }

            var allowedSecretNames = new[] { awsSecretsName };

            // Log the secret names being added
            logger.LogInformation($"Adding secrets for: {string.Join(", ", allowedSecretNames)}");

            var secrets = configurationBuilder.AddSecretsManager(configurator: opts =>
            {
                opts.SecretFilter = entry => HasValue(allowedSecretNames, entry);
                opts.KeyGenerator = (entry, key) => GenerateKey(allowedSecretNames, key);
            });

            // Log that the method has finished and the secrets
            logger.LogInformation("Finished AddAwsSecrets, secrets: {Secrets}", string.Join(", ", secrets));

            return secrets;
        }
        catch (Exception ex)
        {
            // Log the exception
            logger.LogError(ex, "An error occurred in AddAwsSecrets");
            throw; // Re-throw the exception to ensure the caller knows an error occurred
        }
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
