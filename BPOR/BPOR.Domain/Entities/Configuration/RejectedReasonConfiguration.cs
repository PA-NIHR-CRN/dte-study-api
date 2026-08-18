using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class RejectedReasonConfiguration : IEntityTypeConfiguration<RejectedReason>
{
    public void Configure(EntityTypeBuilder<RejectedReason> builder)
    {
        builder.HasData(
            new RejectedReason
            {
                Id = (int)RejectedReasonType.NotNihrAffiliated,
                Code = "Not NIHR-affiliated",
                Description = "Not NIHR-affiliated"
            },
            new RejectedReason
            {
                Id = (int)RejectedReasonType.PpieOpportunity,
                Code = "PPIE opportunity",
                Description = "PPIE opportunity"
            },
            new RejectedReason
            {
                Id = (int)RejectedReasonType.StudyAlreadyListedHere,
                Code = "Study already listed here",
                Description = "Study already listed here"
            },
            new RejectedReason
            {
                Id = (int)RejectedReasonType.NotPossibleToRecruitTargetPopulation,
                Code = "Not possible to recruit target population",
                Description = "Not possible to recruit target population"
            },
            new RejectedReason
            {
                Id = (int)RejectedReasonType.RecruitmentWindowTooShort,
                Code = "Recruitment window too short",
                Description = "Recruitment window too short"
            },
            new RejectedReason
            {
                Id = (int)RejectedReasonType.Misc,
                Code = "Misc",
                Description = "Misc"
            });
    }
}