using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYNAMO.STREAM.HANDLER.Entities.Interceptors
{
    internal class AuditedInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult(HandleAuditing(eventData, result));
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            return HandleAuditing(eventData, result);
        }

        protected static InterceptionResult<int> HandleAuditing(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null)
            {
                return result;
            }

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is { State: EntityState.Added, Entity: IAudited inserted })
                {
                    inserted.CreatedAt = inserted.UpdatedAt = DateTime.UtcNow;
                }

                if (entry is { State: EntityState.Modified, Entity: IAudited updated })
                {
                    updated.UpdatedAt = DateTime.UtcNow;
                }

                if (entry is { State: EntityState.Deleted, Entity: IAudited deleted })
                {
                    deleted.UpdatedAt = DateTime.UtcNow;
                }
            }
            return result;
        }
    }
}
