using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace NIHR.Infrastructure.EntityFrameworkCore
{
    public static class NihrExtensions
    {
        public static DbContextOptionsBuilder<TContext> UseNihrExtensions<TContext>(this DbContextOptionsBuilder<TContext> optionsBuilder) where TContext : DbContext
        {
            UseNihrExtensionsImpl(optionsBuilder);

            return optionsBuilder;
        }

        public static DbContextOptionsBuilder UseNihrExtensions(this DbContextOptionsBuilder optionsBuilder)
        {
            return UseNihrExtensionsImpl(optionsBuilder);
        }

        public static bool TryGetService<TService>(this IInfrastructure<IServiceProvider> accessor, out TService? service) where TService : class?
        {
            // See Microsoft.EntityFrameworkCore.Infrastructure.Internal.InfrastructureExtensions.GetService()

            var serviceType = typeof(TService);

            IServiceProvider instance = accessor.Instance;
            object? obj = instance.GetService(serviceType) ?? instance.GetService<IDbContextOptions>()?.Extensions.OfType<CoreOptionsExtension>().FirstOrDefault()?.ApplicationServiceProvider?.GetService(serviceType);

            if (obj == null)
            {
                service = default;
                return false;
            }
            else
            {
                service = (TService)obj;
                return true;
            }
        }

        private static DbContextOptionsBuilder UseNihrExtensionsImpl(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(
                new DisableAutoDetectChangesInterceptor(),
                new SoftDeleteInterceptor(),
                new TimestampInterceptor(),
                new AuditInterceptor()
                );

            optionsBuilder.ReplaceService<IModelCustomizer, NihrModelCustomizer>();
            return optionsBuilder;
        }
    }
}
