using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BPOR.Tests.Common
{
    public class SaveChangeCountInterceptor : ISaveChangesInterceptor
    {
        int _saveChangesAsyncCount, _saveChangesCount = 0;

        public int SaveChangesAsyncCount => _saveChangesAsyncCount;
        public int SaveChangesCount => _saveChangesCount;

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
            Interlocked.Increment(ref _saveChangesCount);
            return result;
        }
    }
}