using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dynamo.Stream.Handler.Entities;

public class ParticipantDbContextFactory : IDesignTimeDbContextFactory<ParticipantDbContext>
{
    public ParticipantDbContext CreateDbContext(string[] args)
    {
        var configuration = Startup.BuildConfiguration();

        var connectionString = Startup.GetConnectionString(configuration);

        var options = new DbContextOptionsBuilder<ParticipantDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;

        return new ParticipantDbContext(options);
    }
}
