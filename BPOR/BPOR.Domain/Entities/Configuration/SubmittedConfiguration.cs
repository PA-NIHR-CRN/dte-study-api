using BPOR.Domain.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPOR.Domain.Entities.Configuration
{
    public class SubmittedConfiguration : IEntityTypeConfiguration<Submitted>
    {
        public void Configure(EntityTypeBuilder<Submitted> builder)
        {
            builder.HasData(
                new Submitted
                {
                    Id = 1,
                    Code = "Yes",
                    Description = "Yes",
                    IsDeleted = false
                },
                new Submitted
                {
                    Id = 2,
                    Code = "No",
                    Description = "No",
                    IsDeleted = false
                },
                new Submitted
                {
                    Id = 3,
                    Code = "Not yet, but will be submitted",
                    Description = "Not yet, but will be submitted",
                    IsDeleted = false
                }
            );
        }
    }
}
