using System;
using System.Linq;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Application.Contracts;
using Application.Settings;
using Dte.Common;
using Dte.Common.Authentication;
using Dte.Common.Contracts;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Location.Api.Client;
using Dte.Reference.Data.Api.Client;
using Dte.Study.Management.Api.Client;
using Infrastructure.Factories;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StudyApi.Mocks;

namespace StudyApi.DependencyRegistrations
{
    public static class InfrastructureRegistration
    {
        private static readonly string[] ProdEnvironmentNames = { "production", "prod", "live" };

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string environmentName)
        {
            // Infrastructure dependencies
            services.AddScoped<IParticipantRepository, ParticipantDynamoDbRepository>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddSingleton<IClock, Clock>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMessageSenderFactory, MessageSenderFactory>();
            services.AddSingleton<IHeaderService, HeaderService>();
            services.AddScoped<IFeatureFlagService, FeatureFlagService>();

            services.AddTransient<IPrivateKeyProvider, NhsLoginPrivateKeyProvider>();
            services.AddTransient<IClientAssertionJwtProvider, NhsLoginClientAssertionJwtProvider>();

            // AWS
            var awsSettings = configuration.GetSection(AwsSettings.SectionName).Get<AwsSettings>();
            var amazonDynamoDbConfig = new AmazonDynamoDBConfig();
            var amazonCognitoConfig = new AmazonCognitoIdentityProviderConfig();
            if (!string.IsNullOrWhiteSpace(awsSettings.ServiceUrl))
            {
                amazonDynamoDbConfig.ServiceURL = awsSettings.ServiceUrl;
                amazonCognitoConfig.ServiceURL = awsSettings.ServiceUrl;
            }

            services.AddScoped<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(amazonDynamoDbConfig));
            services.AddScoped<IDynamoDBContext>(_ => new DynamoDBContext(new AmazonDynamoDBClient(amazonDynamoDbConfig)));
            amazonCognitoConfig.RegionEndpoint = RegionEndpoint.GetBySystemName(awsSettings.CognitoRegion);
            services.AddScoped<IAmazonCognitoIdentityProvider>(_ => new AmazonCognitoIdentityProviderClient(amazonCognitoConfig));
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());

            // Clients

            var clientsSettings = configuration.GetSection(ClientsSettings.SectionName).Get<ClientsSettings>();
            var logger = services.BuildServiceProvider().GetService<ILoggerFactory>().CreateLogger("StudyApi.DependencyRegistrations.InfrastructureRegistration");

            services.AddHttpClientWithRetry<IStudyManagementApiClient, StudyManagementApiClient>(clientsSettings.StudyManagementService, 2, logger);
            services.AddHttpClientWithRetry<ILocationApiClient, LocationApiClient>(clientsSettings.LocationService, 2, logger);
            services.AddHttpClientWithRetry<IReferenceDataApiClient, ReferenceDataApiClient>(clientsSettings.ReferenceDataService, 2, logger);

            var devSettings = configuration.GetSection(DevSettings.SectionName).Get<DevSettings>();

            // If not Prod, then enable stubs
            if (devSettings.EnableStubs && !ProdEnvironmentNames.Any(x => string.Equals(x, environmentName, StringComparison.OrdinalIgnoreCase)))
            {
                // Enable local stubs
                services.AddScoped<IEmailService, MockEmailService>();
                services.AddSingleton<IAmazonCognitoIdentityProvider, MockCognitoProvider>();
            }

            return services;
        }
    }
}