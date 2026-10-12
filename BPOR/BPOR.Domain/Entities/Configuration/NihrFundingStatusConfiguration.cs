using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;

namespace BPOR.Domain.Entities.Configuration;

public class NihrFundingStatusConfiguration : IEntityTypeConfiguration<NihrFundingStatus>
{
    public void Configure(EntityTypeBuilder<NihrFundingStatus> builder)
    {
        builder.ConfigureReferenceData<NihrFundingStatus, NihrFundingStatusType>();

        builder.HasData(
            new NihrFundingStatus
            {
                Id = NihrFundingStatusType.Yes,
                Code = "Yes",
                Description = "Yes"
            },
            new NihrFundingStatus
            {
                Id = NihrFundingStatusType.No,
                Code = "No",
                Description = "No"
            },
            new NihrFundingStatus
            {
                Id = NihrFundingStatusType.NoButApplied,
                Code = "No, but I have applied for NIHR funding",
                Description = "No, but I have applied for NIHR funding"
            });
    }
}