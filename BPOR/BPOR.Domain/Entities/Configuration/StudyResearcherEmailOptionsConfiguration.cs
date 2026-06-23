using BPOR.Domain.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class StudyResearcherEmailOptionsConfiguration : IEntityTypeConfiguration<StudyResearcherEmailOptions>
{
    public void Configure(EntityTypeBuilder<StudyResearcherEmailOptions> builder)
    {
        builder.HasData(
            new StudyResearcherEmailOptions
            {
                Id = 1,
                Code = "Introductory Email",
                Description = "Introduction email",
            },
            new StudyResearcherEmailOptions
            {
                Id = 2,
                Code = "Offer Pre-Screener",
                Description = "Next steps email with pre-screener",
            },
            new StudyResearcherEmailOptions
            {
                Id = 3,
                Code = "Without Pre-Screener",
                Description = "Next steps email with NO pre-screener",
            });
    }
}