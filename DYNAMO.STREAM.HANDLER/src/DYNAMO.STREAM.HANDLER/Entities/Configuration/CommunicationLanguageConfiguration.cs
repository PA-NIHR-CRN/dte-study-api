using DYNAMO.STREAM.HANDLER.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DYNAMO.STREAM.HANDLER.Entities.Configuration;

public class CommunicationLanguageConfiguration: IEntityTypeConfiguration<CommunicationLanguage>
{
    public void Configure(EntityTypeBuilder<CommunicationLanguage> builder)
    {
        // Seeding the CommunicationLanguage table
        builder.HasData(
            new CommunicationLanguage
            {
                Id = 1,
                Code = "1",
                Description = "en-GB",
                IsDeleted = false
            },
            new CommunicationLanguage
            {
                Id = 2,
                Code = "2",
                Description = "cy-GB",
                IsDeleted = false
            }
        );
    }
}
