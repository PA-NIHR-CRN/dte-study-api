using BPOR.Domain.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Immutable;

namespace BPOR.Domain.Entities.Configuration;

public enum GenderId
{
    Male = 1,
    Female = 2,
    PreferNotToSay = 3
}

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public static ImmutableList<Gender> GetGenders()
    {
        var genderBuilder = new List<Gender> {
            new() {
                Id = (int)GenderId.Male,
                Code = "Male",
                Description = "Male",
                IsDeleted = false
            },
            new() {
                Id = (int)GenderId.Female,
                Code = "Female",
                Description = "Female",
                IsDeleted = false
            },
            new() {
                Id = (int)GenderId.PreferNotToSay,
                Code = "Prefer Not to Say",
                Description = "Prefer Not to Say",
                IsDeleted = false
            }
        };

        return genderBuilder.ToImmutableList();
    }

    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        // Seeding the Gender table
        builder.HasData(GetGenders()
        );
    }
}
