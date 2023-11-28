using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DYNAMO.STREAM.HANDLER.Entities.Interceptors
{
    internal class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult(HandleSoftDelete(eventData, result));
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
                if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete })
                {
                    continue;
                }

                if (entry is { State: EntityState.Deleted, Entity: Participant participant })
                {
                    participant.Email = null;
                    participant.FirstName = null;
                    participant.LastName = null;
                    participant.MobileNumber = null;
                    participant.LandlineNumber = null;
                    participant.RegistrationConsent = false;
                    participant.RemovalOfConsentRegistrationAtUtc = DateTime.UtcNow;
                    participant.Disability = null;
                    participant.Address.Clear();
                    participant.HealthConditions.Clear();
                }

                entry.State = EntityState.Modified;
                delete.IsDeleted = true;
            }
            return result;
        }
    }
}