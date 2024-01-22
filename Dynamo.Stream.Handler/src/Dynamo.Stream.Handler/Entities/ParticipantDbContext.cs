using Dynamo.Stream.Handler.Entities.Configuration;
using Dynamo.Stream.Handler.Entities.Interceptors;
using Dynamo.Stream.Handler.Entities.RefData;
using Dynamo.Stream.Handler.Entities.System;
using Dynamo.Stream.Handler.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dynamo.Stream.Handler.Entities;

public class ParticipantDbContext : DbContext
{
    public ParticipantDbContext(DbContextOptions<ParticipantDbContext> options) : base(options)
    {
    }

    public DbSet<Participant> Participants { get; set; } = null!;
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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParticipantDbContext).Assembly);
    }

    public IQueryable<Participant> GetParticipantByLinkedIdentifiers(List<Identifier> identifiers)
    {
        var values = identifiers.Select(id => id.Value).ToList();

        return Participants.Where(p => p.ParticipantIdentifiers.Any(pi => values.Contains(pi.Value)));
    }

    public void ThrowIfInMaintenanceMode() { 
        if (SysConfigurations.Any(x => x.Name == nameof(SysConfigurationConfiguration.ConfigurationKeys.IsInMaintenanceMode) && x.Value == $"{true}"))
        {
            throw new MaintenanceModeException();
        }
    }
}

public static class ParticipantQueryableExtensions
{
    public static IQueryable<Participant> ForUpdate(this IQueryable<Participant> source)
    {
        return source
            .Include(x => x.Address)
            .Include(x => x.HealthConditions)
            .Include(x => x.ParticipantIdentifiers)
            .Include(x => x.SourceReferences);
    }
}
