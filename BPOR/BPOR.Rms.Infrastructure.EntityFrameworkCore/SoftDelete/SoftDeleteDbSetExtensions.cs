using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.Infrastructure.EntityFrameworkCore
{
    public static class SoftDeleteDbSetExtensions
    {
        public static EntityEntry<TSource> HardDelete<TSource>(this DbSet<TSource> source, TSource item) where TSource : class, ISoftDelete
        {
            source.Entry(item).Metadata.SetRuntimeAnnotation(SoftDeleteInterceptor.HardDeleteAnnotation, true);
            return source.Remove(item);
        }
    }
}
