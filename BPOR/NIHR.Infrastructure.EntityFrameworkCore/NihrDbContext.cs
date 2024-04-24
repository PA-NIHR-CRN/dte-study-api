using Microsoft.EntityFrameworkCore;

namespace NIHR.Infrastructure.EntityFrameworkCore;

public abstract class NihrDbContext : DbContext
{
    protected NihrDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.AddInterceptors(
                new DisableAutoDetectChangesInterceptor(),
                new SoftDeleteInterceptor(),
                new TimestampInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var subclassAssembly = this.GetType().Assembly;

        modelBuilder.EnableSoftDelete(subclassAssembly);

        modelBuilder.EnableReferenceDataEntites(subclassAssembly);
    }
}
