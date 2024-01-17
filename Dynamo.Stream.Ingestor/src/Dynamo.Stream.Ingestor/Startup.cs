using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;
using Amazon.Lambda;
using Amazon.Lambda.Core;
using Microsoft.EntityFrameworkCore;
using Dynamo.Stream.Ingestor.Services;
using Dynamo.Stream.Handler.Settings;
using Dynamo.Stream.Handler.Entities;
using Dynamo.Stream.Ingestor.Repository;
using Dynamo.Stream.Ingestor.Settings;
using Dynamo.Stream.Handler.Handlers;
using Dynamo.Stream.Handler.Services;
using Dynamo.Stream.Handler.Mappers;
using Amazon.DynamoDBv2.DataModel;

namespace Dynamo.Stream.Ingestor;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        var configuration = Handler.Startup.BuildConfiguration();
        services.AddSingleton(configuration);

        // add aws services
        services.AddScoped<IAmazonDynamoDB, AmazonDynamoDBClient>();
        services.AddScoped<IAmazonLambda, AmazonLambdaClient>();
        services.AddScoped<ILambdaSerializer, Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>();

        // add application services
        //services.AddScoped<IDynamoParticipantRepository, DynamoParticipantRepository>();
        services.AddScoped<IDynamoParticipantRepository, DynamoDbBackupRepository>();
        services.AddOptions<DynamoDbBackupSettings>().BindConfiguration("DynamoDbBackupSettings");
        services.AddTransient<IDynamoDbEventService, DynamoDbEventService>();

        //services.AddOptions<StreamHandlerLambdaSettings>().BindConfiguration("StreamHandlerLambdaSettings");
        //services.AddTransient<IStreamHandler, LambdaStreamHandler>();
        services.AddTransient<IStreamHandler, StreamHandler>();
        services.AddScoped<IDynamoDBContext>(x => new DynamoDBContext(new AmazonDynamoDBClient()));

        // add application services
        services.AddSingleton<IRefDataService, RefDataService>();
        services.AddTransient<IParticipantMapper, ParticipantMapper>();

        // add aurora services
        // db setup
        services.AddOptions<DbSettings>().BindConfiguration("DbSettings");
        var connectionString = Handler.Startup.GetConnectionString(configuration);
        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x=>x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
        //services.AddOptions<DynamoDbSettings>().BindConfiguration("DynamoDbSettings");

        Handler.Startup.ConfigureLogging(services, configuration);
    }
}
