using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class IdentifierTypeConfiguration : IEntityTypeConfiguration<IdentifierType>
{
    public void Configure(EntityTypeBuilder<IdentifierType> builder)
    {
        // Seeding the IdentifierType table
        builder.HasData(
            new IdentifierType
            {
                Id = (int)IdentifierTypes.ParticipantId,
                Code = "ParticipantId",
                Description = "ParticipantId",
                IsDeleted = false
            },
            new IdentifierType
            {
                Id = (int)IdentifierTypes.NhsId,
                Code = "NhsId",
                Description = "NhsId",
                IsDeleted = false
            },
            new IdentifierType
            {
                Id = (int)IdentifierTypes.Deleted,
                Code = "Deleted",
                Description = "Deleted",
                IsDeleted = false
            },
            new IdentifierType
            {
                Id = (int)IdentifierTypes.Offline,
                Code = "Offline",
                Description = "Offline",
                IsDeleted = false
            }
        );
    }
}
