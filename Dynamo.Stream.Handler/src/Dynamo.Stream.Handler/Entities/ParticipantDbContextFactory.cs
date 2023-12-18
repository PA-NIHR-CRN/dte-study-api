using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Dynamo.Stream.Handler.Entities;

public class ParticipantDbContextFactory : IDesignTimeDbContextFactory<ParticipantDbContext>
{
    public ParticipantDbContext CreateDbContext(string[] args)
    {
        var configuration = Startup.BuildConfiguration(new ServiceCollection().BuildServiceProvider());

        var connectionString = Startup.GetConnectionString(configuration);

        var options = new DbContextOptionsBuilder<ParticipantDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;

        return new ParticipantDbContext(options);
    }
}
