using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain;

public static class DiExtensions
{
    public static void AddRmsDatabase(this IServiceCollection serviceCollection, 
        Action<DbContextOptionsBuilder, IServiceProvider>? configureDbContext = null)
    {
        serviceCollection.AddOptions<DbSettings>().BindConfiguration(DbSettings.SectionName);
        serviceCollection.AddDbContext<ParticipantDbContext>((serviceProvider, options) =>
        {
            var dbSettings = serviceProvider.GetRequiredService<IOptions<DbSettings>>();
            var participantConnectionString = dbSettings.Value.BuildConnectionString();
            var dbContextOptionsBuilder = options.UseMySql(participantConnectionString, ServerVersion.AutoDetect(participantConnectionString),
                builder =>
                {
                    builder.UseNetTopologySuite();
                    builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            configureDbContext?.Invoke(dbContextOptionsBuilder, serviceProvider);
        });
    }
}