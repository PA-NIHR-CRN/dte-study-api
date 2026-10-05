using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;

namespace BPOR.Domain.Entities.Configuration;

public class StudyStatusConfiguration : IEntityTypeConfiguration<StudyStatus>
{
    public void Configure(EntityTypeBuilder<StudyStatus> builder)
    {
        builder.ConfigureReferenceData<StudyStatus, StudyStatusType>();

        builder.HasData(
            new StudyStatus
            {
                Id = StudyStatusType.Draft,
                Code = "Draft Application",
                Description = "Draft Application"
            },
            new StudyStatus
            {
                Id = StudyStatusType.NewApplication,
                Code = "New Application",
                Description = "New Application"
            },
            new StudyStatus
            {
                Id = StudyStatusType.InProgress,
                Code = "In Progress",
                Description = "In Progress"
            },
            new StudyStatus
            {
                Id = StudyStatusType.Active,
                Code = "Active",
                Description = "Active"
            },
            new StudyStatus
            {
                Id = StudyStatusType.ConcludedSuccessfully,
                Code = "Concluded Successfully",
                Description = "Concluded Successfully"
            },
            new StudyStatus
            {
                Id = StudyStatusType.Rejected,
                Code = "Rejected",
                Description = "Rejected"
            },
            new StudyStatus
            {
                Id = StudyStatusType.Withdrawn,
                Code = "Withdrawn",
                Description = "Withdrawn"
            });
    }
}