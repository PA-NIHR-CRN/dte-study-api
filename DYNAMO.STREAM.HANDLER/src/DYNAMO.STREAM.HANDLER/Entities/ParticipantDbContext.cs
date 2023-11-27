using Microsoft.EntityFrameworkCore;

namespace DYNAMO.STREAM.HANDLER.Entities;

public class ParticipantDbContext: DbContext
{
    public ParticipantDbContext(DbContextOptions<ParticipantDbContext> options) : base(options)
    {
    }

    public DbSet<Participant> Participants { get; set; }
    public DbSet<DailyLifeImpact> DailyLifeImpacts { get; set; }
    public DbSet<CommunicationLanguage> CommunicationLanguages { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<HealthCondition> HealthConditions { get; set; }
    public DbSet<IdentifierType> IdentifierTypes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // get all classes that inherit from RefData
        var refDataTypes = typeof(RefData).Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(RefData)));
        
        foreach (var type in refDataTypes)
        {
            modelBuilder.Entity(type).ToTable("SysRef" + type.Name);
        }
    }
}
