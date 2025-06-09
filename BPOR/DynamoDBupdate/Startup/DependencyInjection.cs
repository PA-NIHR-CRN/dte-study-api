using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using BPOR.Domain.Entities;
using BPOR.Domain.Interfaces;
using BPOR.Domain.Repositories;
using BPOR.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NIHR.Infrastructure.EntityFrameworkCore;
using DynamoDBupdate.Backfills;
using NIHR.Infrastructure;
using BPOR.Infrastructure.Clients;
using Dte.Common.Authentication;
using Dte.Common.Extensions;

namespace DynamoBDupdate.Startup;

public static class DependencyInjection
{

    public static IServiceCollection RegisterServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        services.ConfigureNihrLogging(configuration);

        var logger = services
            .BuildServiceProvider()
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger<LocationApiClient>();

        var dbSettings = services.GetSectionAndValidate<DbSettings>(configuration);
        var connectionString = dbSettings.Value.BuildConnectionString();

        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                builder => builder
                    .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    .UseNetTopologySuite()));

        var clientsSettings = services.GetSectionAndValidate<ClientsSettings>(configuration);
        if (clientsSettings?.Value?.LocationService?.BaseUrl is null)
        {
            throw new ArgumentException("LocationService configuration is required.", nameof(clientsSettings));
        }

        services.AddTransient<IPostcodeMapper, LocationApiClient>();
        services.AddHttpClientWithRetry<IPostcodeMapper, LocationApiClient>(
            clientsSettings.Value.LocationService,
            2,
            logger);

        services.ConfigureAwsServices(configuration);

        services.AddScoped<IParticipantRepository, ParticipantDynamoDbRepository>();
        services.AddScoped<Backfill>();
        services.AddScoped<Stage2Backfill>();
        services.AddScoped<CanonicalTownBackfill>();

        return services;
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

        // Configure AWS Options globally if needed
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
    }
}
