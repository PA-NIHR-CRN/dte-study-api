using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Handlers;
using BPOR.Registration.Stream.Handler.Mappers;
using BPOR.Registration.Stream.Handler.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NIHR.Infrastructure.EntityFrameworkCore;
using NIHR.Infrastructure.Extensions;


namespace BPOR.Registration.Stream.Handler;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services, IHostEnvironment hostEnvironment) 
    {
        // configuration
        var configuration = new ConfigurationManager().AddNihrConfiguration(services, hostEnvironment);
        services.AddSingleton(configuration);

        // db setup
        var dbSettings = services.GetSectionAndValidate<DbSettings>(configuration);
        var connectionString = dbSettings.Value.BuildConnectionString();
        services.AddDbContext<ParticipantDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
        services.AddScoped<IDynamoDBContext>(x => new DynamoDBContext(new AmazonDynamoDBClient()));

        // add application services
        services.AddSingleton<IRefDataService, RefDataService>();
        services.AddTransient<IStreamHandler, StreamHandler>();
        services.AddTransient<IParticipantMapper, ParticipantMapper>();
        
        services.ConfigureNihrLogging(configuration);
    }
}
