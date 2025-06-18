using System.Security.Cryptography.Xml;
using System.Threading;
using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace BPOR.Tests.Common
{
    public class LocalParticipantDatabase : IDisposable, IDbContextFactory<ParticipantDbContext>
    {
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        private readonly IConfiguration _configuration;
        private string ConnectionString => _configuration.GetValue<string>("dteDatabase:connectionString");

        private readonly TestInterceptor _interceptor = new TestInterceptor();

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

        public int SaveChangesAsyncCount => _interceptor.SaveChancesAsyncCount;

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

        private class TestInterceptor : ISaveChangesInterceptor
        {
            int _saveChangesAsyncCount = 0;

            public int SaveChancesAsyncCount => _saveChangesAsyncCount;

            public ValueTask<InterceptionResult<int>> SavingChangesAsync(
               DbContextEventData eventData,
               InterceptionResult<int> result,
               CancellationToken cancellationToken = default)
            {
                Interlocked.Increment(ref _saveChangesAsyncCount);
                return ValueTask.FromResult(result);
            }
        }
    }
}