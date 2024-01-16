using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo.Stream.Handler.Entities.Interceptors
{
    public class DisableAutoDetectChangesInterceptor : SaveChangesInterceptor
    {
        public DisableAutoDetectChangesInterceptor()
        {
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            eventData.Context.ChangeTracker.AutoDetectChangesEnabled = false;
            eventData.Context.ChangeTracker.DetectChanges();

            return result;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            eventData.Context.ChangeTracker.AutoDetectChangesEnabled = false;
            eventData.Context.ChangeTracker.DetectChanges();

            return ValueTask.FromResult(result);
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            eventData.Context.ChangeTracker.AutoDetectChangesEnabled = true;

            return result;
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            eventData.Context.ChangeTracker.AutoDetectChangesEnabled = true;

            return ValueTask.FromResult(result);
        }
    }
}
