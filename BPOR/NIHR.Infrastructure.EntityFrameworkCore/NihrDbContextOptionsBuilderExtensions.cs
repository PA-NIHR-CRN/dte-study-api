using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NIHR.Infrastructure.EntityFrameworkCore.Internal;

namespace NIHR.Infrastructure.EntityFrameworkCore
{
    public static class NihrDbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseNihrConventions(this DbContextOptionsBuilder options)
        {
            options.AddInterceptors(
                new DisableAutoDetectChangesInterceptor(), // TODO: Make this a configurable option
                new SoftDeleteInterceptor(),
                new TimestampInterceptor(),
                new AuditInterceptor()
                );

            var extension = new NihrContextOptionsExtension();

            ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(extension);

            return options;
        }
    }
}
