using BPOR.Domain.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Seeding the Role table
        builder.HasData(
            new Role
            {
                Id = 1,
                Code = "Admin",
                Description = "Admin",
                IsDeleted = false
            },
            new Role
            {
                Id = 2,
                Code = "Researcher",
                Description = "Researcher",
                IsDeleted = false
            }
        );
    }
}
