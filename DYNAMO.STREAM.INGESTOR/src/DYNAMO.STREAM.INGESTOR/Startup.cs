using System.Diagnostics;
using Amazon.DynamoDBv2;
using DYNAMO.STREAM.INGESTOR.Repository;
using DYNAMO.STREAM.INGESTOR.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Amazon.Lambda;
using Amazon.Lambda.Core;
using DYNAMO.STREAM.HANDLER;
using DYNAMO.STREAM.HANDLER.Entities;
using DYNAMO.STREAM.HANDLER.Extensions;
using Microsoft.EntityFrameworkCore;
using Dynamo.Stream.Ingestor.Services;
using DYNAMO.STREAM.HANDLER.Handlers;

namespace DYNAMO.STREAM.INGESTOR;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = BuildConfiguration();
        services.AddSingleton(configuration);

        // add aws services
        services.AddScoped<IAmazonDynamoDB, AmazonDynamoDBClient>();
        services.AddScoped<IAmazonLambda, AmazonLambdaClient>();
        services.AddScoped<ILambdaSerializer, Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>();

        // add application services
        services.AddScoped<IDynamoParticipantRepository, DynamoParticipantRepository>();
        services.AddTransient<IDynamoDbEventService, DynamoDbEventService>();
        
        services.AddOptions<StreamHandlerLambdaSettings>().BindConfiguration("StreamHandlerLambdaSettings");
        services.AddTransient<IStreamHandler, LambdaStreamHandler>();
        
        // add aurora services
        // db setup
        services.AddOptions<DbSettings>().Bind(configuration.GetSection(DbSettings.SectionName));
        var connectionString = GetConnectionString(configuration);
        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


        ConfigureLogging(services, configuration);
    }

    private IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true)
            .AddEnvironmentVariables()
            .AddAwsSecrets()
            .Build();
    }
    
    private static string GetConnectionString(IConfiguration configuration)
    {
        var dbSettings = configuration.GetSection(DbSettings.SectionName).Get<DbSettings>();
        return dbSettings.BuildConnectionString();
    }

    private static void ConfigureLogging(IServiceCollection services, IConfiguration configuration)
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
            loggingBuilder
                .AddConfiguration(configuration.GetSection("Logging"))
                .AddLambdaLogger(loggerOptions);

            if (Debugger.IsAttached || Environment.UserInteractive)
            {
                loggingBuilder
                    .AddConsole()
                    .AddDebug();
            }
        });
    }
}
