using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class VolunteerStudyInformationContactConfiguration : IEntityTypeConfiguration<VolunteerStudyInformationContact>
{
    public void Configure(EntityTypeBuilder<VolunteerStudyInformationContact> builder)
    {
    }
}