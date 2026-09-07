using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class StudyStatusReasonHistoryConfiguration : IEntityTypeConfiguration<StudyStatusReasonHistory>
{
    public void Configure(EntityTypeBuilder<StudyStatusReasonHistory> builder)
    {
        builder.HasKey(s => s.Id);
    }
}