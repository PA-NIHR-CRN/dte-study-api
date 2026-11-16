using BPOR.Domain.Entities;
using BPOR.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain;

public static class DiExtensions
{
    private const string HealthCheckNameRmsDb = "RmsDb";

    public static void AddRmsDatabase(this IServiceCollection serviceCollection, 
        Action<DbContextOptionsBuilder, IServiceProvider>? configureDbContext = null)
    {
        serviceCollection.AddOptions<RmsDbSettings>().BindConfiguration(RmsDbSettings.SectionName).ValidateDataAnnotations().ValidateOnStart();
        
        serviceCollection.AddDbContext<ParticipantDbContext>((serviceProvider, options) =>
        {
            var dbSettings = serviceProvider.GetRequiredService<IOptions<RmsDbSettings>>();
            var participantConnectionString = dbSettings.Value.BuildConnectionString();
      
            var dbContextOptionsBuilder = options.UseMySql(participantConnectionString, ServerVersion.AutoDetect(participantConnectionString),
                builder =>
                {
                    builder.UseNetTopologySuite();
                    builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            configureDbContext?.Invoke(dbContextOptionsBuilder, serviceProvider);
        });

        serviceCollection.AddHealthChecks().AddMySql(serviceProvider => 
        {
            var dbSettings = serviceProvider.GetRequiredService<IOptions<RmsDbSettings>>();
            var participantConnectionString = dbSettings.Value.BuildConnectionString();
            return participantConnectionString;
        }, name: HealthCheckNameRmsDb);
    }
}