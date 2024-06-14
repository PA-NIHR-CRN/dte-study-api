using BPOR.Domain.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration
{
    public class EthnicGroupConfiguration : IEntityTypeConfiguration<EthnicGroup>
    {
        public void Configure(EntityTypeBuilder<EthnicGroup> builder)
        {
            builder.HasData(
                new EthnicGroup
                {
                    Id = 1,
                    Code = "Asian",
                    Description = "Asian",
                    IsDeleted = false
                },
                new EthnicGroup
                {
                    Id = 2,
                    Code = "Black",
                    Description = "Black",
                    IsDeleted = false
                },
                new EthnicGroup
                {
                    Id = 3,
                    Code = "Mixed",
                    Description = "Mixed",
                    IsDeleted = false
                },
                new EthnicGroup
                {
                    Id = 4,
                    Code = "White",
                    Description = "White",
                    IsDeleted = false
                },
                new EthnicGroup
                {
                    Id = 5,
                    Code = "Other",
                    Description = "Other",
                    IsDeleted = false
                }
            );
        }
    }
}
