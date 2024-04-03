using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NIHR.Infrastructure.Extensions;
using NIHR.Infrastructure.Settings;

namespace BPOR.Domain.Entities;

public class AuroraParticipantDbContextFactory() : IDesignTimeDbContextFactory<AuroraDbContext>
{
    public AuroraDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.user.json", optional: true)
            .Build();
        
        var dbSettings = configuration.GetSection(DbSettings.SectionName).Get<DbSettings>();

        var connectionString = dbSettings.BuildConnectionString();

        var options = new DbContextOptionsBuilder<AuroraDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;

        return new AuroraDbContext(options);
    }
}
