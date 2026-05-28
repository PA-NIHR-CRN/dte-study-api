using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class CampaignParticipantConfiguration : IEntityTypeConfiguration<CampaignParticipant>
{
    public void Configure(EntityTypeBuilder<CampaignParticipant> builder)
    {
        builder.Property(i => i.Token).HasMaxLength(64);
    }
}