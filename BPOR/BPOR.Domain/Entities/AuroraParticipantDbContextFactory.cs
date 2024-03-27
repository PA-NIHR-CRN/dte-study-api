using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NIHR.Infrastructure.Extensions;
using NIHR.Infrastructure.Settings;

namespace BPOR.Domain.Entities;

public class AuroraParticipantDbContextFactory(IHostEnvironment environment)
    : IDesignTimeDbContextFactory<AuroraDbContext>
{
    public AuroraDbContext CreateDbContext(string[] args)
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationManager().AddNihrConfiguration(services, environment);

        var connectionString = services.GetSectionAndValidate<DbSettings>(configuration).Value.BuildConnectionString();

        var options = new DbContextOptionsBuilder<AuroraDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;

        return new AuroraDbContext(options);
    }
}