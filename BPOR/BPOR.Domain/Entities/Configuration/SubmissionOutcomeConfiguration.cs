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
    public class SubmissionOutcomeConfiguration : IEntityTypeConfiguration<SubmissionOutcome>
    {
        public void Configure(EntityTypeBuilder<SubmissionOutcome> builder)
        {
            builder.HasData(
                new SubmissionOutcome
                {
                    Id = 1,
                    Code = "Eligible for inclusion",
                    Description = "Eligible for inclusion",
                    IsDeleted = false
                },
                new SubmissionOutcome
                {
                    Id = 2,
                    Code = "Awaiting outcome",
                    Description = "Awaiting outcome",
                    IsDeleted = false
                },
                new SubmissionOutcome
                {
                    Id = 3,
                    Code = "Ineligible for inclusion",
                    Description = "Ineligible for inclusion",
                    IsDeleted = false
                }
            );
        }
    }
}
