using BPOR.Domain.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Immutable;

namespace BPOR.Domain.Entities.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public const string RoleNameAdmin = "Admin";
    
    public static int RoleId(string code)
    {
        return GetRoles().Where(x => x.Code == code).Select(x => x.Id).Single();
    }
    
    public static ImmutableList<Role> GetRoles()
    {
        var roleBuilder = new List<Role> {
            new Role
            {
                Id = 1,
                Code = RoleNameAdmin,
                Description = "Admin",
                IsDeleted = false
            },
            new Role
            {
                Id = 2,
                Code = "Researcher",
                Description = "Researcher",
                IsDeleted = false
            },
            new Role
            {
                Id = 3,
                Code = "Tester",
                Description = "Tester",
                IsDeleted = false
            }
        };

        return roleBuilder.ToImmutableList();
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Seeding the Role table
        builder.HasData(
            GetRoles()
        );
    }
}
