using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasIndex(p => new { p.IsDeleted, p.Id, p.Stage2CompleteUtc, p.DateOfBirth })
            .HasDatabaseName("IX_Participants_FilterChunk");
    }
}