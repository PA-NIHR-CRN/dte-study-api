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
using NIHR.Infrastructure.Configuration;

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

        // Rate limiting
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        // Infrastructure dependencies
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
        services.AddTransient<IMfaService, MfaService>();
        services.AddTransient<INhsService, NhsService>();
        services.AddTransient<IPasswordService, PasswordService>();
        services.AddTransient<ISignUpService, SignUpService>();
        services.AddTransient<IRespondToMfaChallengeService, RespondToMfaChallengeService>();

        // Additional services
        services.AddContentfulServices(configuration);
        services.ConfigureAwsServices(configuration);
        services.AddHttpContextAccessor();

        // Clients
        services.AddHttpClients(configuration);
        
        // CORS
        services.AddCors(hostEnvironment);

        var dataProtection = services.AddDataProtection();
        if (!hostEnvironment.IsDevelopment())
        {
            dataProtection.PersistKeysToAWSSystemsManager("/BPOR/DataProtection");
        }
        
        var build = System.Environment.GetEnvironmentVariable("DTE_BUILD_STRING") ?? "Unknown";

        //TODO confirm health check 
        services.AddHealthChecks();


        if (hostEnvironment.IsDevelopment())
        {
            services.ConfigureDevelopmentServices(configuration);
        }
        else if (hostEnvironment.IsStaging())
        {
            services.ConfigureStagingServices(configuration);
        }

        return services;
    }

    private static void ConfigureStagingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.GetSectionAndValidate<DevelopmentSettings>(configuration);
        services.Decorate<IEmailService, DevelopmentEmailService>();
        services.Decorate<IRespondToMfaChallengeService, DevelopmentRespondToMfaChallengeService>();
        services.Decorate<ISignUpService, DevelopmentSignUpService>();
    }

    private static void ConfigureDevelopmentServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.GetSectionAndValidate<DevelopmentSettings>(configuration);
        services.Decorate<IEmailService, DevelopmentEmailService>();
        services.Decorate<IRespondToMfaChallengeService, DevelopmentRespondToMfaChallengeService>();
        services.Decorate<ISignUpService, DevelopmentSignUpService>();
    }

    private static void ConfigureAwsServices(this IServiceCollection services, IConfiguration configuration)
    {

        var awsSettings = services.GetSectionAndValidate<AwsSettings>(configuration).Value;
        
        // Configure Amazon DynamoDB
        var dynamoDbConfig = new AmazonDynamoDBConfig();
        if (!string.IsNullOrWhiteSpace(awsSettings.ServiceUrl))
        {
            dynamoDbConfig.ServiceURL = awsSettings.ServiceUrl;
        }
        var dynamoDbClient = new AmazonDynamoDBClient(dynamoDbConfig);
        services.AddSingleton<IAmazonDynamoDB>(dynamoDbClient);
        services.AddSingleton<IDynamoDBContext, DynamoDBContext>(_ => new DynamoDBContext(dynamoDbClient));

        // Configure Amazon Cognito Identity Provider
        var cognitoConfig = new AmazonCognitoIdentityProviderConfig
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(awsSettings.CognitoRegion)
        };
        if (!string.IsNullOrWhiteSpace(awsSettings.ServiceUrl))
        {
            cognitoConfig.ServiceURL = awsSettings.ServiceUrl;
        }
        services.AddSingleton<IAmazonCognitoIdentityProvider>(new AmazonCognitoIdentityProviderClient(cognitoConfig));

        // Configure Amazon Simple Email Service
        var sesConfig = new AmazonSimpleEmailServiceConfig
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(awsSettings.CognitoRegion)
        };
        services.AddSingleton<IAmazonSimpleEmailService>(new AmazonSimpleEmailServiceClient(sesConfig));

        // DynamoDB Operation Configuration
        services.AddSingleton(new DynamoDBOperationConfig
        {
            OverrideTableName = awsSettings.ParticipantRegistrationDynamoDbTableName
        });

        // Configure AWS Options globally if needed
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
    }
    
    private static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var nhsLoginSettings = services.GetSectionAndValidate<NhsLoginSettings>(configuration);
        var clientsSettings = services.GetSectionAndValidate<ClientsSettings>(configuration);
        var logger = services.BuildServiceProvider().GetService<ILoggerFactory>()
            .CreateLogger("BPOR.Registration.Api.Startup.DependencyInjection");

        services.AddHttpClientWithRetry<ILocationApiClient, LocationApiClient>(clientsSettings.Value.LocationService, 2,
            logger);

        services.AddHttpClient<NhsLoginHttpClient>((serviceProvider, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(nhsLoginSettings.Value.BaseUrl);
        });
    }
    
    private static void AddCors(this IServiceCollection services, IHostEnvironment hostEnvironment)
    {
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
    }
}
