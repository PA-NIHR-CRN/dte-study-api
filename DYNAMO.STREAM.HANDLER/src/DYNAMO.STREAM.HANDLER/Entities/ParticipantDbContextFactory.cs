using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DYNAMO.STREAM.HANDLER.Entities;

public class ParticipantDbContextFactory : IDesignTimeDbContextFactory<ParticipantDbContext>
{
    public ParticipantDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("ParticipantDb");

        var options = new DbContextOptionsBuilder<ParticipantDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;

        return new ParticipantDbContext(options);
    }
}
