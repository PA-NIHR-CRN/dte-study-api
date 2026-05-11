using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class VolunteerStudyInformationGroupConfiguration : IEntityTypeConfiguration<VolunteerStudyInformationGroup>
{
    public void Configure(EntityTypeBuilder<VolunteerStudyInformationGroup> builder)
    {
        builder.HasMany(g => g.Criteria)
            .WithOne(c => c.Group)
            .HasForeignKey(c => c.VolunteerStudyInformationGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}