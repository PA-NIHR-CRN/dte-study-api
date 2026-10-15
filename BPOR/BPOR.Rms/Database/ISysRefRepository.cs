using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Rms.Database;

public interface ISysRefRepository<T>
    where T : class, IReferenceData
{
    Task<T?> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync(Func<T, bool>? predicate = null);

    Task<string?> GetShortenedDescriptionAsync(int id);

    async Task<string?> GetDescriptionAsync(int id) => (await GetByIdAsync(id))?.Description;

    async Task<string?> GetDescriptionAsync(int? id) => id.HasValue ? await GetDescriptionAsync(id.Value) : null;

    async Task<string?> GetCodeAsync(int id) => (await GetByIdAsync(id))?.Code;

    async Task<string?> GetCodeAsync(int? id) => id.HasValue ? await GetCodeAsync(id.Value) : null;
}