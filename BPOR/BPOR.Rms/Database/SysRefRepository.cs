using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Rms.Database;

public class SysRefRepository<T>(ParticipantDbContext dbContext, IMemoryCache cache) : ISysRefRepository<T>
    where T : class, IReferenceData
{
    public async Task<T?> GetByIdAsync(int id)
    {
        return (await GetSysRefRows()).TryGetValue(id, out T? item) ? item : null;
    }

    public virtual async Task<string?> GetShortenedDescriptionAsync(int id)
    {
        return (await GetSysRefRows()).TryGetValue(id, out T? item) ? item.Description : null;
    }

    private async Task<IReadOnlyDictionary<int, T>> GetSysRefRows()
    {
        return (await cache.GetOrCreateAsync(new CacheKey(typeof(T)), LoadFromDb))!;
    }

    private async Task<Dictionary<int, T>> LoadFromDb(ICacheEntry arg)
    {
        return await dbContext.Set<T>().ToDictionaryAsync(i => i.Id, i => i);
    }
    
    public async Task<IEnumerable<T>> GetAllAsync(Func<T, bool>? predicate = null)
    {
        var values = (await GetSysRefRows()).Values;

        return predicate is null
            ? values
            : values.Where(predicate);
    }

    private record CacheKey(Type entityType);
}