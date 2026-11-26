using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class CampaignParticipantConfiguration : IEntityTypeConfiguration<CampaignParticipant>
{
    public void Configure(EntityTypeBuilder<CampaignParticipant> builder)
    {
        builder.HasIndex(p => p.ParticipantId);
        builder.HasIndex(p => new { p.ParticipantId, p.IsDeleted, p.CampaignId })
            .HasDatabaseName("IX_CampaignParticipant_FilterContacted");
    }
}