using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using BPOR.Domain.Entities;
using BPOR.Domain.Interfaces;
using BPOR.Domain.Repositories;
using BPOR.Domain.Settings;
using BPOR.Infrastructure.Clients;
using Dte.Common.Authentication;
using Dte.Common.Extensions;
using DynamoDBupdate.Backfills;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NIHR.Infrastructure;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace DynamoDBupdate.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        services.ConfigureNihrLogging(configuration);

        var dbSettings = services.GetSectionAndValidate<DbSettings>(configuration);
        var connectionString = dbSettings.Value.BuildConnectionString();

        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                builder => builder
                    .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    .UseNetTopologySuite()));

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

        //var dynamoDbConfig = new AmazonDynamoDBConfig();
        //if (!string.IsNullOrWhiteSpace(awsSettings.ServiceUrl))
        //{
        //    dynamoDbConfig.ServiceURL = awsSettings.ServiceUrl;
        //}

        //var dynamoDbClient = new AmazonDynamoDBClient(dynamoDbConfig);

        //services.AddSingleton<IAmazonDynamoDB>(dynamoDbClient);
        //services.AddSingleton<IDynamoDBContext>(_ => new DynamoDBContext(dynamoDbClient));
        //services.AddSingleton(new DynamoDBOperationConfig
        //{
        //    OverrideTableName = awsSettings.ParticipantRegistrationDynamoDbTableName
        //});

        var dynamoDbConfig = new AmazonDynamoDBConfig();
        if (!string.IsNullOrWhiteSpace(awsSettings.ServiceUrl))
        {
            dynamoDbConfig.ServiceURL = awsSettings.ServiceUrl;
        }
        else if (!string.IsNullOrWhiteSpace(awsSettings.CognitoRegion))
        {
            dynamoDbConfig.RegionEndpoint = RegionEndpoint.GetBySystemName(awsSettings.CognitoRegion);
        }

        AWSCredentials credentials;
        var chain = new CredentialProfileStoreChain();
        if (!chain.TryGetAWSCredentials(awsSettings.CognitoPoolId, out credentials))
        {
            throw new Exception($"Could not load AWS credentials from profile '{awsSettings.CognitoPoolId}'.");
        }

        var dynamoDbClient = new AmazonDynamoDBClient(credentials, dynamoDbConfig);

        services.AddSingleton<IAmazonDynamoDB>(dynamoDbClient);
        services.AddSingleton<IDynamoDBContext>(_ => new DynamoDBContext(dynamoDbClient));
        services.AddSingleton(new DynamoDBOperationConfig());

        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
    }
}
