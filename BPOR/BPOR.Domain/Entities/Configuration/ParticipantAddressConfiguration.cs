using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class ParticipantAddressConfiguration : IEntityTypeConfiguration<ParticipantAddress>
{
    public void Configure(EntityTypeBuilder<ParticipantAddress> builder)
    {
        builder.HasIndex(p => new { p.Postcode });
    }
}
