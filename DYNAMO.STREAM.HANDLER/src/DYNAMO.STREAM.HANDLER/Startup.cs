using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DYNAMO.STREAM.HANDLER.Entities;
using DYNAMO.STREAM.HANDLER.Extensions;
using DYNAMO.STREAM.HANDLER.Handlers;
using DYNAMO.STREAM.HANDLER.Mappers;
using DYNAMO.STREAM.HANDLER.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DYNAMO.STREAM.HANDLER;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // configuration
        var configuration = BuildConfiguration();
        services.AddSingleton(configuration);

        // db setup
        services.AddOptions<DbSettings>().Bind(configuration.GetSection(DbSettings.SectionName));
        var connectionString = GetConnectionString(configuration);
        services.AddDbContext<ParticipantDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    builder => { builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null); });
            }
        );

        services.AddScoped<IDynamoDBContext>(x => new DynamoDBContext(new AmazonDynamoDBClient()));

        // add application services
        services.AddSingleton<IRefDataService, RefDataService>();
        services.AddScoped<IStreamHandler, StreamHandler>();
        services.AddTransient<IParticipantMapper, ParticipantMapper>();

        ConfigureLogging(services, configuration);
    }

    private static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true).AddEnvironmentVariables().AddAwsSecrets().Build();
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
