using System;
using System.Diagnostics;
using System.IO;
using Amazon;
using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Configuration;
using NIHR.Infrastructure.Settings;

namespace NIHR.Infrastructure.Extensions
{
    public static class Configuration
    {
        
        //TODO chat with chris about this global exception handling
       
        public static IServiceCollection ConfigureNihrLogging(this IServiceCollection services,
            IConfiguration configuration)
        {
            var loggerOptions = new LambdaLoggerOptions
            {
                IncludeCategory = true,
                IncludeLogLevel = true,
                IncludeNewline = true,
                IncludeEventId = true,
                IncludeException = true,
                IncludeScopes = true,
            };

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));

                if (IsRunningInLambda())
                {
                    loggingBuilder.AddLambdaLogger(loggerOptions);
                }

                if (Debugger.IsAttached || Environment.UserInteractive)
                {
                    loggingBuilder.AddConsole().AddDebug();
                }
            });

            return services;
        }

        private static bool IsRunningInLambda()
        {
            var executionEnv = Environment.GetEnvironmentVariable("AWS_EXECUTION_ENV");
            return !string.IsNullOrEmpty(executionEnv) && executionEnv.StartsWith("AWS_Lambda_");
        }

        public static ConfigurationManager AddNihrConfiguration(this ConfigurationManager configuration,
            IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsDevelopment())
            {
                configuration.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.user.json", optional: true, reloadOnChange: true);
            }

            var secretsManagerSettings = configuration.GetSection("AwsSecretsManager").Get<AwsSecretsManagerSettings>();
            if (secretsManagerSettings.Enabled)
            {
                configuration.AddAwsSecretsManager(secretsManagerSettings.SecretName,
                    () => new AmazonSecretsManagerClient(
                        RegionEndpoint.GetBySystemName(secretsManagerSettings.Region)));
            }

            return configuration;
        }

        private static void AddAwsSecretsManager(this IConfigurationBuilder configurationBuilder, string secretName,
            Func<IAmazonSecretsManager> secretsManagerClientFactory)
        {
            var configurationSource = new AwsSecretsManagerConfigurationSource(secretName, secretsManagerClientFactory);
            configurationBuilder.Add(configurationSource);
        }

        public static IOptions<T> GetSectionAndValidate<T>(this IServiceCollection services,
            IConfiguration configuration) where T : class, new()
        {
            var sectionNameProperty = typeof(T).GetProperty("SectionName");
            if (sectionNameProperty == null)
            {
                throw new InvalidOperationException(
                    $"Type {typeof(T).Name} does not contain a property named 'SectionName'.");
            }

            var instance = Activator.CreateInstance<T>();
            var sectionName = sectionNameProperty.GetValue(instance) as string;
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                throw new InvalidOperationException(
                    $"The 'SectionName' property in {typeof(T).Name} cannot be null or whitespace.");
            }

            var settings = configuration.GetSection(sectionName).Get<T>();

            services.AddOptions<T>()
                .BindConfiguration(sectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return Options.Create(settings);
        }
    }
}
