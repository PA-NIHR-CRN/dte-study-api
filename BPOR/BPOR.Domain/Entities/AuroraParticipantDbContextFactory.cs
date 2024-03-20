using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NIHR.Infrastructure.Extensions;
using NIHR.Infrastructure.Settings;

namespace BPOR.Domain.Entities;

public class AuroraParticipantDbContextFactory(IHostEnvironment environment)
    : IDesignTimeDbContextFactory<AuroraParticipantDbContext>
{
    public AuroraParticipantDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationManager().AddNihrConfiguration(environment);

        var connectionString =
            configuration.GetSection(DbSettings.SectionName).Get<DbSettings>().BuildConnectionString();

        var options = new DbContextOptionsBuilder<AuroraParticipantDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;

        return new AuroraParticipantDbContext(options);
    }
}
