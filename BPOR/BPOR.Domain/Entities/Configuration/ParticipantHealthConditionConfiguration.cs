using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class ParticipantHealthConditionConfiguration : IEntityTypeConfiguration<ParticipantHealthCondition>
{
    public void Configure(EntityTypeBuilder<ParticipantHealthCondition> builder)
    {
        builder.HasIndex(p => p.ParticipantId);
        builder.HasIndex(p => new { p.ParticipantId, p.IsDeleted, p.HealthConditionId })
            .HasDatabaseName("IX_ParticipantHealthCondition_FilterInterests");
    }
}