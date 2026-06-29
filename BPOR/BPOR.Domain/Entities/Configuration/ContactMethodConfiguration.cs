using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class ContactMethodConfiguration : IEntityTypeConfiguration<ContactMethod>
{
    public void Configure(EntityTypeBuilder<ContactMethod> builder)
    {
        // Seeding the ContactMethod table
        builder.HasData(
            new ContactMethod
            {
                Id = (int)ContactMethodId.Email,
                Code = "Email",
                Description = "Email",
                IsDeleted = false
            },
            new ContactMethod
            {
                Id = (int)ContactMethodId.Letter,
                Code = "Letter",
                Description = "Letter",
                IsDeleted = false
            }
        );
    }
}
