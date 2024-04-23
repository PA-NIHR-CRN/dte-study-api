using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace NIHR.Infrastructure.Entities.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            return new ValueTask<InterceptionResult<int>>(HandleSoftDelete(eventData, result));
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            return HandleSoftDelete(eventData, result);
        }

        protected static InterceptionResult<int> HandleSoftDelete(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null)
            {
                return result;
            }

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is { State: EntityState.Deleted, Entity: ISoftDelete delete })
                {
                    if (entry is { Entity: IPersonalInformation pii })
                    {
                        pii.Anonymise();
                    }

                    entry.State = EntityState.Modified;
                    delete.IsDeleted = true;
                }
            }
            return result;
        }
    }
}
