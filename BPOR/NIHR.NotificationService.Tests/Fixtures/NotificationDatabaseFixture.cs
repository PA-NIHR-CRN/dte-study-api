using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NIHR.NotificationService.Context;

namespace NIHR.NotificationService.Tests.Fixtures
{
    public class NotificationDatabaseFixture
    {
        private string ConnectionString => _configuration.GetValue<string>("notificationDatabase:connectionString");
        private readonly IConfiguration _configuration;

        public NotificationDatabaseFixture(IConfiguration configuration)
        {
            _configuration = configuration;
            using var context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public NotificationDbContext CreateContext()
             => new NotificationDbContext(new DbContextOptionsBuilder<NotificationDbContext>()
                 .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                 .Options);
    }
}