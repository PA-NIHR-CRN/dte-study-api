using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Annotations;
using DYNAMO.STREAM.HANDLER.Entities;
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
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true).AddEnvironmentVariables().Build();

        services.AddSingleton<IRefDataService, RefDataService>();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("ParticipantDb"), ServerVersion.Parse("8.0.21")));
        services.AddScoped<IDynamoDBContext>(x => new DynamoDBContext(new AmazonDynamoDBClient()));
        services.AddTransient<IStreamHandler, StreamHandler>();
        services.AddTransient<IParticipantMapper, ParticipantMapper>();
        
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        });
    }
}
