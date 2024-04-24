using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace NIHR.Infrastructure.EntityFrameworkCore;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserIdProvider _currentUserIdProvider;

    public AuditInterceptor(ICurrentUserIdProvider currentUserIdProvider)
    {
        _currentUserIdProvider = currentUserIdProvider;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        return new ValueTask<InterceptionResult<int>>(HandleAuditColumns(eventData, result));
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        return HandleAuditColumns(eventData, result);
    }

    protected InterceptionResult<int> HandleAuditColumns(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null)
        {
            return result;
        }

        if(_currentUserIdProvider.UserId is null)
        {
            // TODO: We want to track the correct user info for changes,
            // log a warning or error here.
            return result;
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is { State: EntityState.Added, Entity: IAudit inserted })
            {
                if (inserted.CreatedById == default)
                {
                    inserted.CreatedById = _currentUserIdProvider.UserId.Value;
                }

                if (inserted.UpdatedAt == default)
                {
                    inserted.UpdatedById = _currentUserIdProvider.UserId.Value;
                }
            }

            if (entry is { State: EntityState.Modified, Entity: IAudit updated })
            {
                updated.UpdatedById = _currentUserIdProvider.UserId.Value;
            }

            if (entry is { State: EntityState.Deleted, Entity: IAudit deleted })
            {
                deleted.UpdatedById = _currentUserIdProvider.UserId.Value;
            }
        }
        return result;
    }
}
