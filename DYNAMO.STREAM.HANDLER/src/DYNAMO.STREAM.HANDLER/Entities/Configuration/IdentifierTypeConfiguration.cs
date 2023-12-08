using DYNAMO.STREAM.HANDLER.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DYNAMO.STREAM.HANDLER.Entities.Configuration;

public class IdentifierTypeConfiguration : IEntityTypeConfiguration<IdentifierType>
{
    public void Configure(EntityTypeBuilder<IdentifierType> builder)
    {
        // Seeding the IdentifierType table
        builder.HasData(
            new IdentifierType
            {
                Id = 1,
                Code = "ParticipantId",
                Description = "ParticipantId",
                IsDeleted = false
            },
            new IdentifierType
            {
                Id = 2,
                Code = "NhsId",
                Description = "NhsId",
                IsDeleted = false
            }
        );
    }
}
