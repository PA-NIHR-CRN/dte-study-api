using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NIHR.NotificationService.Tests.Fixtures
{
    public class ParticipantDatabaseFixture
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString => _configuration.GetValue<string>("dteDatabase:connectionString");

        public ParticipantDatabaseFixture(IConfiguration configuration)
        {
            _configuration = configuration;
            using var context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public ParticipantDbContext CreateContext()
             => new ParticipantDbContext(new DbContextOptionsBuilder<ParticipantDbContext>()
                 .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString), builder =>
                 {
                     builder.UseNetTopologySuite();
                     builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                 })
                 .Options);
    }
}