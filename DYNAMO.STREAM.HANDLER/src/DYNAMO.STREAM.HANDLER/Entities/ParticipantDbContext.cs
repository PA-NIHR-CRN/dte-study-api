using DYNAMO.STREAM.HANDLER.Entities.Configuration;
using DYNAMO.STREAM.HANDLER.Entities.Interceptors;
using DYNAMO.STREAM.HANDLER.Entities.RefData;
using Microsoft.EntityFrameworkCore;

namespace DYNAMO.STREAM.HANDLER.Entities;

public class ParticipantDbContext : DbContext
{
    public ParticipantDbContext(DbContextOptions<ParticipantDbContext> options) : base(options)
    {
        Participants = null!;
        DailyLifeImpacts = null!;
        CommunicationLanguages = null!;
        Genders = null!;
        HealthConditions = null!;
        IdentifierTypes = null!;
        ParticipantIdentifiers = null!;
    }

    public DbSet<Participant> Participants { get; set; }
    public DbSet<ParticipantIdentifier> ParticipantIdentifiers { get; set; }
    public DbSet<DailyLifeImpact> DailyLifeImpacts { get; set; }
    public DbSet<CommunicationLanguage> CommunicationLanguages { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<HealthCondition> HealthConditions { get; set; }
    public DbSet<IdentifierType> IdentifierTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor(),
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
        modelBuilder.ApplyConfiguration(new CommunicationLanguageConfiguration());
        modelBuilder.ApplyConfiguration(new DailyLifeImpactConfiguration());
        modelBuilder.ApplyConfiguration(new GenderConfiguration());
        modelBuilder.ApplyConfiguration(new HealthConditionConfiguration());
        modelBuilder.ApplyConfiguration(new IdentifierTypeConfiguration());
    }

    public async Task<Participant?> GetParticipantByLinkedIdentifiersAsync(IList<(int type, string value)> identifiers,
        CancellationToken cancellationToken)
    {
        return await ParticipantIdentifiers
            .Where(x => identifiers.Any(y => y.type == x.IdentifierTypeId && y.value == x.Value))
            .Select(x => x.Participant)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Participant?> GetParticipantByPkAsync(string pk, CancellationToken cancellationToken)
    {
        return await Participants
            .Where(x => x.ParticipantIdentifiers.Any(y => y.Value == pk))
            .SingleOrDefaultAsync(cancellationToken);
    }
}