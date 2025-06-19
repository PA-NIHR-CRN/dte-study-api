using System.Data.Common;
using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Bson;
using NSubstitute;
using NSubstitute.Core;

namespace BPOR.Tests.Common
{
    public class LocalParticipantDatabase : IDisposable
    {
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        private readonly IConfiguration _configuration;
        private string ConnectionString => _configuration.GetValue<string>("dteDatabase:connectionString");

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

        public ParticipantDbContext CreateDbContext(params IInterceptor[] interceptors)
        {
            return new ParticipantDbContext(new DbContextOptionsBuilder<ParticipantDbContext>()
                 .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString), builder =>
                 {
                     builder.UseNetTopologySuite();
                     builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                     builder.CommandTimeout(5);
                 })
                 .AddInterceptors(interceptors)
                 .Options);
        }


        public class SaveChangedCountInterceptor : ISaveChangesInterceptor
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

            public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
            {
                Interlocked.Increment(ref _saveChangesAsyncCount);
                return result;
            }
        }
    }
}