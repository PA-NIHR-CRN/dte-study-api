using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Dynamo.Stream.Handler.Entities.Interceptors
{
    internal class TimestampInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult(HandleTimestamps(eventData, result));
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            return HandleTimestamps(eventData, result);
        }

        protected static InterceptionResult<int> HandleTimestamps(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null)
            {
                return result;
            }

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is { State: EntityState.Added, Entity: ITimestamped inserted })
                {
                    inserted.CreatedAt = inserted.UpdatedAt = DateTime.UtcNow;
                }

                if (entry is { State: EntityState.Modified, Entity: ITimestamped updated })
                {
                    updated.UpdatedAt = DateTime.UtcNow;
                }

                if (entry is { State: EntityState.Deleted, Entity: ITimestamped deleted })
                {
                    deleted.UpdatedAt = DateTime.UtcNow;
                }
            }
            return result;
        }
    }
}
