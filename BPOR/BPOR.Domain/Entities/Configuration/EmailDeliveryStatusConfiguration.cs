using BPOR.Domain.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class EmailDeliveryStatusConfiguration : IEntityTypeConfiguration<DeliveryStatus>
{
    public void Configure(EntityTypeBuilder<DeliveryStatus> builder)
    {
        builder.HasData(
            new DeliveryStatus
            {
                Id = 1,
                Code = "Pending",
                Description = "Pending",
                IsDeleted = false
            },
            new DeliveryStatus
            {
                Id = 2,
                Code = "Sent",
                Description = "Sent",
                IsDeleted = false
            },
            new DeliveryStatus
            {
                Id = 3,
                Code = "Delivered",
                Description = "Delivered",
                IsDeleted = false
            },
            new DeliveryStatus
            {
                Id = 4,
                Code = "RegisteredInterest",
                Description = "RegisteredInterest",
                IsDeleted = false
            },
            new DeliveryStatus
            {
                Id = 5,
                Code = "Failed",
                Description = "Failed",
                IsDeleted = false
            }
        );
    }
}
