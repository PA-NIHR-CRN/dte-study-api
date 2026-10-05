using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;

namespace BPOR.Domain.Entities.Configuration;

public class WithdrawnReasonConfiguration : IEntityTypeConfiguration<WithdrawnReason>
{
    public void Configure(EntityTypeBuilder<WithdrawnReason> builder)
    {
        builder.ConfigureReferenceData<WithdrawnReason, WithdrawnReasonType>();

        builder.HasData(
            new WithdrawnReason
            {
                Id = WithdrawnReasonType.NoResponseFromStudyTeam,
                Code = "No response from study team",
                Description = "No response from study team"
            },
            new WithdrawnReason
            {
                Id = WithdrawnReasonType.StudyDoesNotNeedAdditionalSupport,
                Code = "Study does not need additional support",
                Description = "Study does not need additional support"
            },
            new WithdrawnReason
            {
                Id = WithdrawnReasonType.ProblemsWithStudy,
                Code = "Problems with Study",
                Description = "Problems with Study"
            },
            new WithdrawnReason
            {
                Id = WithdrawnReasonType.StudyTeamHasLimitedCapacity,
                Code = "Study team has limited capacity",
                Description = "Study team has limited capacity"
            },
            new WithdrawnReason
            {
                Id = WithdrawnReasonType.ContactDroppedByBPorTeam,
                Code = "Contact dropped by BPoR team",
                Description = "Contact dropped by BPoR team"
            },
            new WithdrawnReason
            {
                Id = WithdrawnReasonType.Other,
                Code = "Other",
                Description = "Other"
            });
    }
}