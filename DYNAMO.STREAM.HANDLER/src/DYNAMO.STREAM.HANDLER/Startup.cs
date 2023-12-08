using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Annotations;
using DYNAMO.STREAM.HANDLER.Entities;
using DYNAMO.STREAM.HANDLER.Extensions;
using DYNAMO.STREAM.HANDLER.Handlers;
using DYNAMO.STREAM.HANDLER.Mappers;
using DYNAMO.STREAM.HANDLER.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DYNAMO.STREAM.HANDLER;

[LambdaStartup]
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
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddScoped<IDynamoDBContext>(x => new DynamoDBContext(new AmazonDynamoDBClient()));

        // add application services
        services.AddSingleton<IRefDataService, RefDataService>();
        services.AddTransient<IStreamHandler, StreamHandler>();
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
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        });
    }
}
