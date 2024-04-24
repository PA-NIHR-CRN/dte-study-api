using BPOR.Domain.Entities.Configuration;
using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Entities.System;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.EntityFrameworkCore;
using NIHR.Infrastructure.Exceptions;

namespace BPOR.Domain.Entities;

public class ParticipantDbContext : NihrDbContext
{
    public ParticipantDbContext(DbContextOptions options) : base(options)
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
    public DbSet<Study> Studies { get; set; } = null!;
    public DbSet<ManualEnrollment> ManualEnrollments { get; set; } = null!;
    public DbSet<FilterCriteria> FilterCriterias { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParticipantDbContext).Assembly);
    }

    public IQueryable<Participant> GetParticipantByLinkedIdentifiers(List<Identifier> identifiers)
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
    public static IQueryable<Participant> ForUpdate(this IQueryable<Participant> source)
    {
        return source
            .Include(x => x.Address)
            .Include(x => x.HealthConditions)
            .Include(x => x.ParticipantIdentifiers)
            .Include(x => x.SourceReferences);
    }
}
