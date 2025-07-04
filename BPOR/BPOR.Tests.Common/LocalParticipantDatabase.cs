using System.Security.Cryptography.Xml;
using System.Threading;
using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BPOR.Tests.Common
{
    public class LocalParticipantDatabase : IDisposable, IDbContextFactory<ParticipantDbContext>
    {
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        private readonly IConfiguration _configuration;
        private string ConnectionString => _configuration.GetValue<string>("dteDatabase:connectionString");

        private readonly SaveChangeCountInterceptor _interceptor = new SaveChangeCountInterceptor();

        public LocalParticipantDatabase(IConfiguration configuration)
        {
            _semaphore.Wait();
            _configuration = configuration;
            using var context = CreateDbContext();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public void Dispose()
        {
            try
            {
                using var context = CreateDbContext();
                context.Database.EnsureDeleted();
            }
            finally
            {
                _semaphore.Release();
            }

        }

        public int SaveChangesAsyncCount => _interceptor.SaveChangesAsyncCount;

        public ParticipantDbContext CreateDbContext()
        {
            return new ParticipantDbContext(new DbContextOptionsBuilder<ParticipantDbContext>()
                 .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString), builder =>
                 {
                     builder.UseNetTopologySuite();
                     builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                 })
                 .AddInterceptors(_interceptor)
                 .Options);
        }


    }
}