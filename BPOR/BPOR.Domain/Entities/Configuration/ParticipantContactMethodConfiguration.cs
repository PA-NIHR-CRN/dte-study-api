using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class ParticipantContactMethodConfiguration : IEntityTypeConfiguration<ParticipantContactMethod>
{
    public void Configure(EntityTypeBuilder<ParticipantContactMethod> builder)
    {
        builder.HasIndex(p => p.ParticipantId);
        builder.HasIndex(p => new { p.ParticipantId, p.IsDeleted, p.ContactMethodId })
            .HasDatabaseName("IX_ParticipantContactMethod_FilterContactMethod");
        builder.HasIndex(p => new { p.ContactMethodId, p.IsDeleted, p.ParticipantId })
            .HasDatabaseName("IX_ParticipantContactMethod_Method_Deleted_Participant");
    }
}