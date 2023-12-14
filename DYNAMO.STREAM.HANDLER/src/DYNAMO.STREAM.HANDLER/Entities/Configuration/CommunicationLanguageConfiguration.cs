using Dynamo.Stream.Handler.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dynamo.Stream.Handler.Entities.Configuration;

public class CommunicationLanguageConfiguration : IEntityTypeConfiguration<CommunicationLanguage>
{
    public void Configure(EntityTypeBuilder<CommunicationLanguage> builder)
    {
        // Seeding the CommunicationLanguage table
        builder.HasData(
            new CommunicationLanguage
            {
                Id = 1,
                Code = "en-GB",
                Description = "English",
                IsDeleted = false
            },
            new CommunicationLanguage
            {
                Id = 2,
                Code = "cy-GB",
                Description = "Welsh",
                IsDeleted = false
            }
        );
    }
}
