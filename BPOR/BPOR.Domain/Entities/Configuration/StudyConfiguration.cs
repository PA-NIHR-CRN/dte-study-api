using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;

namespace BPOR.Domain.Entities.Configuration;

public class StudyConfiguration : IEntityTypeConfiguration<Study>
{
    public const int InclusionCriteriaMaxLength = 500;
    
    public void Configure(EntityTypeBuilder<Study> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.FullName).Name();
        builder.Property(s => s.StudyName).Name();

        builder.Property(s => s.EmailAddress).Email();
        builder.Property(s => s.ChiefInvestigatorEmail).Email();

        builder.Property(s => s.InformationUrl).Url();
        builder.Property(s => s.PreScreenerUrl).Url();
        
        builder.Property(s => s.InclusionCriteria).HasMaxLength(InclusionCriteriaMaxLength);
    }
}