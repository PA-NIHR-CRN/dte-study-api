using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SimpleEmail;
using AspNetCoreRateLimit;
using BPOR.Domain.Interfaces;
using BPOR.Domain.Repositories;
using BPOR.Domain.Settings;
using BPOR.Infrastructure.Clients;
using BPOR.Infrastructure.Interfaces;
using BPOR.Infrastructure.Services;
using BPOR.Infrastructure.Services.Development;
using Dte.Common;
using Dte.Common.Authentication;
using Dte.Common.Contracts;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Services;
using NIHR.Infrastructure.Clients;
using NIHR.Infrastructure.Extensions;

namespace BPOR.Registration.Api.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        // Settings
        services.GetSectionAndValidate<ContentfulSettings>(configuration);
        services.GetSectionAndValidate<AppSettings>(configuration);
        services.GetSectionAndValidate<EmailSettings>(configuration);
        var nhsLoginSettings = services.GetSectionAndValidate<NhsLoginSettings>(configuration);
        var clientsSettings = services.GetSectionAndValidate<ClientsSettings>(configuration);
        var awsSettings = services.GetSectionAndValidate<AwsSettings>(configuration);

        // Rate limiting
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        // Infrastructure dependencies
        services.AddSingleton<IClock, Clock>();
        services.AddScoped<IParticipantRepository, ParticipantDynamoDbRepository>();
        services.AddScoped<IParticipantService, ParticipantService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IContentfulService, ContentfulService>();
        services.AddTransient<IRichTextToHtmlService, RichTextToHtmlService>();
        services.AddTransient<IPrivateKeyProvider, NhsLoginPrivateKeyProvider>();
        services.AddTransient<IClientAssertionJwtProvider, NhsLoginClientAssertionJwtProvider>();
        services.AddTransient<IAuthService, AuthService>();

        // Contentful set up
        services.AddContentfulServices(configuration);

        // AWS
        var amazonDynamoDbConfig = new AmazonDynamoDBConfig();
        var amazonCognitoConfig = new AmazonCognitoIdentityProviderConfig();
        if (!string.IsNullOrWhiteSpace(awsSettings.Value.ServiceUrl))
        {
            amazonDynamoDbConfig.ServiceURL = awsSettings.Value.ServiceUrl;
            amazonCognitoConfig.ServiceURL = awsSettings.Value.ServiceUrl;
        }

        services.AddScoped<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(amazonDynamoDbConfig));
        services.AddScoped<IDynamoDBContext>(_ =>
            new DynamoDBContext(new AmazonDynamoDBClient(amazonDynamoDbConfig)));
        amazonCognitoConfig.RegionEndpoint = RegionEndpoint.GetBySystemName(awsSettings.Value.CognitoRegion);
        services.AddScoped<IAmazonCognitoIdentityProvider>(_ =>
            new AmazonCognitoIdentityProviderClient(amazonCognitoConfig));
        services.AddSingleton<IAmazonSimpleEmailService>(sp =>
        {
            var config = new AmazonSimpleEmailServiceConfig
            {
                RegionEndpoint = RegionEndpoint.EUWest2
            };
            return new AmazonSimpleEmailServiceClient(config);
        });

        services.AddSingleton<DynamoDBOperationConfig>(_ => new DynamoDBOperationConfig
        {
            OverrideTableName = awsSettings.Value.ParticipantRegistrationDynamoDbTableName
        });

        //TODO test this
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());

        // Clients
        var logger = services.BuildServiceProvider().GetService<ILoggerFactory>()
            .CreateLogger("BPOR.Registration.Api.Startup.DependencyInjection");

        services.AddHttpClientWithRetry<ILocationApiClient, LocationApiClient>(clientsSettings.Value.LocationService, 2,
            logger);

        services.AddHttpClient<NhsLoginHttpClient>((serviceProvider, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(nhsLoginSettings.Value.BaseUrl);
        });

        if (hostEnvironment.IsDevelopment())
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowLocal",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }

        var dataProtection = services.AddDataProtection();
        if (!hostEnvironment.IsDevelopment())
        {
            dataProtection.PersistKeysToAWSSystemsManager("/BPOR/DataProtection");
        }

        services.AddHttpContextAccessor();


        var build = System.Environment.GetEnvironmentVariable("DTE_BUILD_STRING") ?? "Unknown";

        //TODO confirm health check 
        services.AddHealthChecks();


        if (hostEnvironment.IsDevelopment())
        {
            ConfigureDevelopmentServices(configuration, hostEnvironment, services);
        }
        else if (hostEnvironment.IsStaging())
        {
            ConfigureStagingServices(configuration, hostEnvironment, services);
        }

        return services;
    }

    private static void ConfigureStagingServices(IConfiguration configuration, IHostEnvironment hostEnvironment,
        IServiceCollection services)
    {
        services.GetSectionAndValidate<DevelopmentSettings>(configuration);
        services.Decorate<IEmailService, DevelopmentEmailService>();
        services.Decorate<IRespondToMfaChallengeService, DevelopmentRespondToMfaChallengeService>();
        services.Decorate<ISignUpService, DevelopmentSignUpService>();
    }

    private static void ConfigureDevelopmentServices(IConfiguration configuration, IHostEnvironment hostEnvironment,
        IServiceCollection services)
    {
        services.GetSectionAndValidate<DevelopmentSettings>(configuration);
        services.Decorate<IEmailService, DevelopmentEmailService>();
        services.Decorate<IRespondToMfaChallengeService, DevelopmentRespondToMfaChallengeService>();
        services.Decorate<ISignUpService, DevelopmentSignUpService>();
    }
}
