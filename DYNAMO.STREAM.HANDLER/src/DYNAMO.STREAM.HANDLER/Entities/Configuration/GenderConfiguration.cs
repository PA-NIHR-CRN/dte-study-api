
using DYNAMO.STREAM.HANDLER.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DYNAMO.STREAM.HANDLER.Entities.Configuration;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        // Seeding the Gender table
        builder.HasData(
            new Gender
            {
                Id = 1,
                Code = "Male",
                Description = "Male",
                IsDeleted = false
            },
            new Gender
            {
                Id = 2,
                Code = "Female",
                Description = "Female",
                IsDeleted = false
            },
            new Gender
            {
                Id = 3,
                Code = "Prefer Not to Say",
                Description = "Prefer Not to Say",
                IsDeleted = false
            }
        );
    }
}
