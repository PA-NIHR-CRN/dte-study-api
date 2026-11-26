using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasIndex(p => new { p.IsDeleted, p.DateOfBirth, p.Stage2CompleteUtc })
            .HasDatabaseName("IX_Participants_FilterAge");
        builder.HasIndex(p => new { p.IsDeleted, p.Stage2CompleteUtc })
            .HasDatabaseName("IX_Participants_FilterRegistration");
    }
}