using BPOR.Domain.Entities.Configuration;
using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Entities.System;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.Entities.Interceptors;
using NIHR.Infrastructure.Exceptions;

namespace BPOR.Domain.Entities;

public class AuroraParticipantDbContext : DbContext
{
    public AuroraParticipantDbContext(DbContextOptions<AuroraParticipantDbContext> options) : base(options)
    {
    }

    public DbSet<AuroraParticipant> Participants { get; set; } = null!;
    public DbSet<ParticipantIdentifier> ParticipantIdentifiers { get; set; } = null!;
    public DbSet<DailyLifeImpact> DailyLifeImpacts { get; set; } = null!;
    public DbSet<CommunicationLanguage> CommunicationLanguages { get; set; } = null!;
    public DbSet<Gender> Genders { get; set; } = null!;
    public DbSet<HealthCondition> HealthConditions { get; set; } = null!;
    public DbSet<IdentifierType> IdentifierTypes { get; set; } = null!;
    public DbSet<SysConfiguration> SysConfigurations { get; set; } = null!;

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
        var refDataTypes = typeof(IReferenceData).Assembly.GetTypes()
            .Where(t => t != typeof(ReferenceData) && typeof(IReferenceData).IsAssignableFrom(t) && !t.IsInterface);

        foreach (var type in refDataTypes)
        {
            modelBuilder.Entity(type).ToTable("SysRef" + type.Name);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuroraParticipantDbContext).Assembly);
    }

    public IQueryable<AuroraParticipant> GetParticipantByLinkedIdentifiers(List<Identifier> identifiers)
    {
        var values = identifiers.Select(id => id.Value).ToList();

        return Participants.Where(p => p.ParticipantIdentifiers.Any(pi => values.Contains(pi.Value)));
    }

    public void ThrowIfInMaintenanceMode()
    {
        if (SysConfigurations.Any(x =>
                x.Name == nameof(SysConfigurationConfiguration.ConfigurationKeys.IsInMaintenanceMode) &&
                x.Value == $"{true}"))
        {
            throw new MaintenanceModeException();
        }
    }
}

public static class ParticipantQueryableExtensions
{
    public static IQueryable<AuroraParticipant> ForUpdate(this IQueryable<AuroraParticipant> source)
    {
        return source
            .Include(x => x.Address)
            .Include(x => x.HealthConditions)
            .Include(x => x.ParticipantIdentifiers)
            .Include(x => x.SourceReferences);
    }
}
