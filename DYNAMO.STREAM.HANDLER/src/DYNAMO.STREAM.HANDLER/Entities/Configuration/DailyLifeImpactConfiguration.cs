using DYNAMO.STREAM.HANDLER.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DYNAMO.STREAM.HANDLER.Entities.Configuration;

public class DailyLifeImpactConfiguration : IEntityTypeConfiguration<DailyLifeImpact>
{
    public void Configure(EntityTypeBuilder<DailyLifeImpact> builder)
    {
        // Seeding the DailyLifeImpact table
        builder.HasData(
            new DailyLifeImpact
            {
                Id = 1,
                Code = "Yes, a lot",
                Description = "Yes, a lot",
                IsDeleted = false
            },
            new DailyLifeImpact
            {
                Id = 2,
                Code = "Yes, a little",
                Description = "Yes, a little",
                IsDeleted = false
            },
            new DailyLifeImpact
            {
                Id = 3,
                Code = "Not at all",
                Description = "Not at all",
                IsDeleted = false
            },
            new DailyLifeImpact
            {
                Id = 4,
                Code = "Prefer not to say",
                Description = "Prefer not to say",
                IsDeleted = false
            }
        );
    }
}
