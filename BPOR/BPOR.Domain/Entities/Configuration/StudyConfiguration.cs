using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class StudyConfiguration : IEntityTypeConfiguration<Study>
{
    public void Configure(EntityTypeBuilder<Study> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.ChiefInvestigatorEmail).HasMaxLength(255);
        builder.Property(s => s.FullName).HasMaxLength(255);
        builder.Property(s => s.EmailAddress).HasMaxLength(255);
        builder.Property(s => s.StudyName).HasMaxLength(255);

        builder.Property(s => s.InformationUrl).HasMaxLength(2048);
        builder.Property(s => s.PreScreenerUrl).HasMaxLength(2048);
    }
}