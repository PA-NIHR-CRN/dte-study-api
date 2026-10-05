using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class StudyStatusHistoryConfiguration : IEntityTypeConfiguration<StudyStatusHistory>
{
    public void Configure(EntityTypeBuilder<StudyStatusHistory> builder)
    {
        builder.HasKey(s => s.Id);
    }
}