using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;

namespace BPOR.Domain.Entities.Configuration;

public class RejectedReasonConfiguration : IEntityTypeConfiguration<RejectedReason>
{
    public void Configure(EntityTypeBuilder<RejectedReason> builder)
    {
        builder.ConfigureReferenceData<RejectedReason, RejectedReasonType>();
        
        builder.HasData(
            new RejectedReason
            {
                Id = RejectedReasonType.NotNihrAffiliated,
                Code = "Not NIHR-affiliated",
                Description = "Not NIHR-affiliated"
            },
            new RejectedReason
            {
                Id = RejectedReasonType.PpieOpportunity,
                Code = "PPIE opportunity",
                Description = "PPIE opportunity"
            },
            new RejectedReason
            {
                Id = RejectedReasonType.StudyAlreadyListedHere,
                Code = "Study already listed here",
                Description = "Study already listed here"
            },
            new RejectedReason
            {
                Id = RejectedReasonType.NotPossibleToRecruitTargetPopulation,
                Code = "Not possible to recruit target population",
                Description = "Not possible to recruit target population"
            },
            new RejectedReason
            {
                Id = RejectedReasonType.RecruitmentWindowTooShort,
                Code = "Recruitment window too short",
                Description = "Recruitment window too short"
            },
            new RejectedReason
            {
                Id = RejectedReasonType.Misc,
                Code = "Misc",
                Description = "Misc"
            });
    }
}