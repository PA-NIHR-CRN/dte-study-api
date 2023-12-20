using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Dynamo.Stream.Handler.Entities;
using Dynamo.Stream.Handler.Extensions;
using Dynamo.Stream.Handler.Handlers;
using Dynamo.Stream.Handler.Mappers;
using Dynamo.Stream.Handler.Services;
using Dynamo.Stream.Handler.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Dynamo.Stream.Handler;


public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // configuration
        var configuration = BuildConfiguration();
        services.AddSingleton(configuration);

        // db setup
        services.AddOptions<DbSettings>().Bind(configuration.GetSection(DbSettings.SectionName));
        var connectionString = GetConnectionString(configuration);
        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
        services.AddScoped<IDynamoDBContext>(x => new DynamoDBContext(new AmazonDynamoDBClient()));

        // add application services
        services.AddSingleton<IRefDataService, RefDataService>();
        services.AddTransient<IStreamHandler, StreamHandler>();
        services.AddTransient<IParticipantMapper, ParticipantMapper>();

        ConfigureLogging(services, configuration);
    }

    public static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true)
            .AddJsonFile("appsettings.user.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddAwsSecrets()
            .Build();
    }

    public static string GetConnectionString(IConfiguration configuration)
    {
        var dbSettings = configuration.GetSection(DbSettings.SectionName).Get<DbSettings>();
        return dbSettings.BuildConnectionString();
    }

    public static void ConfigureLogging(IServiceCollection services, IConfiguration configuration)
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
                    .AddConsole();
                //.AddDebug();
            }
        });
    }
}
