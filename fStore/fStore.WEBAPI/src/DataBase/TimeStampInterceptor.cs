using fStore.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace fStore.WEBAPI;

public class TimeStampInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        // give collections of all entities experiencing the changes: Added or Updated, Deleted
        var changedData = eventData.Context?.ChangeTracker.Entries();
        var addeddEntries = changedData?.Where(entity => entity.State == EntityState.Added);
        var updatedEntries = changedData?.Where(entity => entity.State == EntityState.Modified);

        if (updatedEntries is not null)
        {
            foreach (var e in updatedEntries)
            {
                if (e.Entity is BaseEntity baseEntity)
                {
                    baseEntity.UpdatedAt = DateTime.Now;
                }
            }
        }

        if (addeddEntries is not null)
        {
            foreach (var e in addeddEntries)
            {
                if (e.Entity is BaseEntity baseEntity)
                {
                    baseEntity.UpdatedAt = DateTime.Now;
                    baseEntity.CreatedAt = DateTime.Now;
                }
            }
        }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
