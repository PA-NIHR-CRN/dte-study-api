using BPOR.Rms.Infrastructure.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using BPOR.Rms.Infrastructure.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace BPOR.Domain.Entities;

public class ParticipantDbContextFactory() : IDesignTimeDbContextFactory<ParticipantDbContext>
{
    public ParticipantDbContext CreateDbContext(string[] args)
    {
// TODO: make this more consistent. Base factory in BPOR.Rms.Infrastructure.EntityFrameworkCore.
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.user.json", optional: true)
            .Build();

        var dbSettings = configuration.GetSection(DbSettings.SectionName).Get<DbSettings>();
        var connectionString = dbSettings?.BuildConnectionString() ??
                               Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

        if (connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString), "Database connection string not configured.");
        }

        var options = new DbContextOptionsBuilder<ParticipantDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
            {
                x.UseNetTopologySuite();
                x.CommandTimeout(300);
            })
            .Options;
        

        return new ParticipantDbContext(options);
    }
}
