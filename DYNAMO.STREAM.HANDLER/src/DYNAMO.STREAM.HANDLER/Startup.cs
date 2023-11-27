using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Annotations;
using Dte.Common;
using Dte.Common.Contracts;
using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using DYNAMO.STREAM.HANDLER.Handlers;
using DYNAMO.STREAM.HANDLER.Ingestors;
using DYNAMO.STREAM.HANDLER.Mappers;
using DYNAMO.STREAM.HANDLER.Repository;
using DYNAMO.STREAM.HANDLER.Services;
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
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true).AddEnvironmentVariables().Build();

        // ref data service singleton
        services.AddSingleton<IRefDataService, RefDataService>();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<IClock, Clock>();
        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("ParticipantDb"), ServerVersion.Parse("8.0.21")));
        services.AddTransient<IStreamHandler, StreamHandler>();
        services.AddTransient<IDataIngestor, CsvIngestor>();
        services.AddTransient<ICsvService, CsvService>();
        services.AddTransient<IParticipantMapper, ParticipantMapper>();
        services.AddTransient<IAuroraRepository, AuroraRepository>();
        services.AddSingleton<IAsyncPolicy>(x =>
            Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
        services.AddScoped<IDynamoDBContext>(x => new DynamoDBContext(new AmazonDynamoDBClient()));
    }
}
