using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BPOR.Tests.Common
{
    public class SaveChangeCountInterceptor : ISaveChangesInterceptor
    {
        int _saveChangesAsyncCount = 0;

        public int SaveChancesAsyncCount => _saveChangesAsyncCount;

        public ValueTask<InterceptionResult<int>> SavingChangesAsync(
           DbContextEventData eventData,
           InterceptionResult<int> result,
           CancellationToken cancellationToken = default)
        {
            Interlocked.Increment(ref _saveChangesAsyncCount);
            return ValueTask.FromResult(result);
        }
    }
}