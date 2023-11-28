using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities.Interceptors;
using DYNAMO.STREAM.HANDLER.Entities.RefData;
using Microsoft.EntityFrameworkCore;

namespace DYNAMO.STREAM.HANDLER.Entities;

public class ParticipantDbContext: DbContext
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
                                       new AuditedInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var refDataTypes = typeof(IReferenceData).Assembly.GetTypes()
            .Where(t => t.IsAssignableFrom(typeof(IReferenceData)));
        
        foreach (var type in refDataTypes)
        {
            modelBuilder.Entity(type).ToTable("SysRef" + type.Name);
        }
    }
}
