using BPOR.Domain.Entities.Configuration;
using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Entities.System;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.EntityFrameworkCore;
using NIHR.Infrastructure.Exceptions;

namespace BPOR.Domain.Entities;

public class ParticipantDbContext : DbContext
{
    public ParticipantDbContext(DbContextOptions<ParticipantDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNihrConventions(options =>
        {
            // For backwards compatibility with existing VS reporting schema,
            // to be updated.
            options.UseTableNameFromDbSet = true;
        });
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
    public DbSet<Campaign> Campaign { get; set; } = null!;
    public DbSet<ParticipantLocation> ParticipantLocation { get; set; } = null!;
    public DbSet<ParticipantContactMethod> ParticipantContactMethod { get; set; } = null!;
    public DbSet<ParticipantAddress> ParticipantAddress { get; set; } = null!;
    public DbSet<FilterEthnicGroup> FilterEthnicGroup { get; set; } = null!;
    public DbSet<FilterPostcode> FilterPostcode { get; set; } = null!;
    public DbSet<FilterAreaOfInterest> FilterAreaOfInterest { get; set; } = null!;
    public DbSet<FilterGender> FilterGender { get; set; } = null!;
    public DbSet<FilterContactMethod> FilterContactMethod { get; set; } = null!;
    public DbSet<FilterSexSameAsRegisteredAtBirth> FilterSexSameAsRegisteredAtBirth { get; set; } = null!;
    public DbSet<CampaignParticipant> CampaignParticipant { get; set; } = null!;
    public DbSet<DeliveryStatus> DeliveryStatus { get; set; } = null!;
    public DbSet<StudyParticipantEnrollment> StudyParticipantEnrollment { get; set; } = null!;
    public DbSet<Submitted> Submitted { get; set; } = null!;
    public DbSet<SubmissionOutcome> SubmissionOutcome { get; set; } = null!;

    public DbSet<User> User { get; set; } = null!;
    public DbSet<UserRole> UserRole { get; set; } = null!;


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
            .Include(x => x.ContactMethodId)
            .Include(x => x.ParticipantLocation)
            .Include(x => x.SourceReferences);
    }
}