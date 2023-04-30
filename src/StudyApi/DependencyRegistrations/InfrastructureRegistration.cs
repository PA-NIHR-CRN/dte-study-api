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
using Dte.Participant.Api.Client;
using Dte.Reference.Data.Api.Client;
using Dte.Study.Management.Api.Client;
using Infrastructure.Clients;
using Infrastructure.Factories;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StudyApi.DependencyRegistrations
{
    public static class InfrastructureRegistration
    {
        private static readonly string[] ProdEnvironmentNames = { "production", "prod", "live" };

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string environmentName)
        {
            // Infrastructure dependencies
            services.AddScoped<IStudyRegistrationRepository, StudyRegistrationDynamoDbRepository>();
            services.AddScoped<IParticipantRegistrationRepository, ParticipantRegistrationDynamoDbRepository>();
            services.AddScoped<IAccessWhitelistRepository, AccessWhitelistRepository>();
            services.AddSingleton<IClock, Clock>();
            services.AddScoped<IUserService, UserService>();

            
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<CpmsHttpMessageHandler>();
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
            // CPMS

            var cpmsSettings = configuration.GetSection(CpmsSettings.SectionName).Get<CpmsSettings>();
            services.AddHttpClient<ICpmsHttpClient, CpmsHttpClient>(client =>
                {
                    var clientBaseAddress = new Uri(cpmsSettings.CpmsApiBaseUrl);
                    client.BaseAddress = clientBaseAddress;
                })
                .AddHttpMessageHandler<CpmsHttpMessageHandler>();

            var clientsSettings = configuration.GetSection(ClientsSettings.SectionName).Get<ClientsSettings>();
            var logger = services.BuildServiceProvider().GetService<ILoggerFactory>().CreateLogger("StudyApi.DependencyRegistrations.InfrastructureRegistration");

            services.AddHttpClientWithRetry<IStudyManagementApiClient, StudyManagementApiClient>(clientsSettings.StudyManagementService, 2, logger);
            services.AddHttpClientWithRetry<ILocationApiClient, LocationApiClient>(clientsSettings.LocationService, 2, logger);
            services.AddHttpClientWithRetry<IReferenceDataApiClient, ReferenceDataApiClient>(clientsSettings.ReferenceDataService, 2, logger);
            services.AddHttpClientWithRetry<IParticipantApiClient, ParticipantApiClient>(clientsSettings.ParticipantService, 2, logger);

            // get DevSwitches from appsettings.json
            var devSwitches = configuration.GetSection(DevSwitches.SectionName).Get<DevSwitches>();

            // If not Prod, then enable stubs
            if (devSwitches.EnableStubs)
            {
                // Enable local stubs
                services.AddScoped<IEmailService, MockEmailService>();
            }

            return services;
        }
    }
}