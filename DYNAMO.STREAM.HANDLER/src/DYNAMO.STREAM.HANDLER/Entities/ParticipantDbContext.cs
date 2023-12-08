using DYNAMO.STREAM.HANDLER.Entities.Configuration;
using DYNAMO.STREAM.HANDLER.Entities.Interceptors;
using DYNAMO.STREAM.HANDLER.Entities.RefData;
using Microsoft.EntityFrameworkCore;

namespace DYNAMO.STREAM.HANDLER.Entities;

public class ParticipantDbContext : DbContext
{
    private static string StripPrimaryKey(string pk) => pk.Replace("PARTICIPANT#", "");

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

    public async Task<Participant?> GetParticipantByLinkedIdentifiersAsync(List<Identifier> identifiers, CancellationToken cancellationToken)
    {
        var types = identifiers.Select(id => id.Type).ToList();
        var values = identifiers.Select(id => id.Value).ToList();

        return await ParticipantIdentifiers
            .Where(pi => types.Contains(pi.IdentifierTypeId) && values.Contains(pi.Value))
            .Select(pi => pi.Participant)
            .SingleOrDefaultAsync(cancellationToken);
    }


    public async Task<Participant?> GetParticipantByPkAsync(string pk, CancellationToken cancellationToken)
    {
        pk = StripPrimaryKey(pk);
        return await Participants
            .Where(x => x.ParticipantIdentifiers.Any(y => y.Value == pk))
            .Include(x => x.Address)
            .Include(x => x.HealthConditions)
            .Include(x => x.ParticipantIdentifiers)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
