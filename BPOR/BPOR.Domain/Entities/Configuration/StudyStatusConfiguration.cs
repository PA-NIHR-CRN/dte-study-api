using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class StudyStatusConfiguration : IEntityTypeConfiguration<StudyStatus>
{
    public void Configure(EntityTypeBuilder<StudyStatus> builder)
    {
        builder.HasData(
            new StudyStatus
            {
                Id = (int)StudyStatusType.NewApplication,
                Code = "New Application",
                Description = "New Application"
            },
            new StudyStatus
            {
                Id = (int)StudyStatusType.InProgress,
                Code = "In Progress",
                Description = "In Progress"
            },
            new StudyStatus
            {
                Id = (int)StudyStatusType.Active,
                Code = "Active",
                Description = "Active"
            },
            new StudyStatus
            {
                Id = (int)StudyStatusType.ConcludedSuccessfully,
                Code = "Concluded Successfully",
                Description = "Concluded Successfully"
            },
            new StudyStatus
            {
                Id = (int)StudyStatusType.Rejected,
                Code = "Rejected",
                Description = "Rejected"
            },
            new StudyStatus
            {
                Id = (int)StudyStatusType.Withdrawn,
                Code = "Withdrawn",
                Description = "Withdrawn"
            });
    }
}