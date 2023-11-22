using Amazon.Lambda.Annotations;
using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using DYNAMO.STREAM.HANDLER.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace DYNAMO.STREAM.HANDLER;

[LambdaStartup]
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("ParticipantDb"), ServerVersion.Parse("8.0.21")));
        services.AddTransient<IStreamHandler, StreamHandler>();
        services.AddSingleton<IAsyncPolicy>(x =>
            Policy.Handle<Exception>().WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
    }
}
