using BPOR.Domain.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BPOR.Domain.Enums;
using NIHR.Infrastructure.EntityFrameworkCore.Extensions;

namespace BPOR.Domain.Entities.Configuration;

public class SubmittedConfiguration : IEntityTypeConfiguration<Submitted>
{
    public void Configure(EntityTypeBuilder<Submitted> builder)
    {
        builder.ConfigureReferenceData<Submitted, SubmittedType>();

        builder.HasData(
            new Submitted
            {
                Id = SubmittedType.Yes,
                Code = "Yes",
                Description = "Yes",
                IsDeleted = false
            },
            new Submitted
            {
                Id = SubmittedType.No,
                Code = "No",
                Description = "No",
                IsDeleted = false
            },
            new Submitted
            {
                Id = SubmittedType.NoButWillApply,
                Code = "Not yet, but will be submitted",
                Description = "Not yet, but will be submitted",
                IsDeleted = false
            }
        );
    }
}