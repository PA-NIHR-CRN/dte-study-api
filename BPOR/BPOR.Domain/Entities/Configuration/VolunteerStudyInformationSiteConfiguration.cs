using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class VolunteerStudyInformationSiteConfiguration : IEntityTypeConfiguration<VolunteerStudyInformationSite>
{
    public void Configure(EntityTypeBuilder<VolunteerStudyInformationSite> builder)
    {
        builder.Property(s => s.AddressLine1).HasMaxLength(100).IsRequired();
        builder.Property(s => s.AddressLine2).HasMaxLength(100).IsRequired(false);
        builder.Property(s => s.AddressLine3).HasMaxLength(100).IsRequired(false);
        builder.Property(s => s.AddressLine4).HasMaxLength(100).IsRequired(false);
        builder.Property(s => s.AddressLine5).HasMaxLength(100).IsRequired(false);
        builder.Property(s => s.Postcode).HasMaxLength(10).IsRequired();
    }
}