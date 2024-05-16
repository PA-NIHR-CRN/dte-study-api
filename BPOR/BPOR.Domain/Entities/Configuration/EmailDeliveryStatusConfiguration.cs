using BPOR.Domain.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class EmailDeliveryStatusConfiguration : IEntityTypeConfiguration<EmailDeliveryStatus>
{
    public void Configure(EntityTypeBuilder<EmailDeliveryStatus> builder)
    {
        builder.HasData(
            new EmailDeliveryStatus
            {
                Id = 1,
                Code = "Pending",
                Description = "Pending",
                IsDeleted = false
            },
            new EmailDeliveryStatus
            {
                Id = 2,
                Code = "Sent",
                Description = "Sent",
                IsDeleted = false
            },
            new EmailDeliveryStatus
            {
                Id = 3,
                Code = "Delivered",
                Description = "Delivered",
                IsDeleted = false
            },
            new EmailDeliveryStatus
            {
                Id = 4,
                Code = "RegisteredInterest",
                Description = "RegisteredInterest",
                IsDeleted = false
            },
            new EmailDeliveryStatus
            {
                Id = 5,
                Code = "Failed",
                Description = "Failed",
                IsDeleted = false
            }
        );
    }
}
