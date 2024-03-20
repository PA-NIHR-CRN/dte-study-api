using BPOR.Domain.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class SysConfigurationConfiguration : IEntityTypeConfiguration<SysConfiguration>
{
    public enum ConfigurationKeys
    {
        IsInMaintenanceMode,
    }

    public void Configure(EntityTypeBuilder<SysConfiguration> builder)
    {
        builder.ToTable("SysConfiguration");

        builder.HasData(new SysConfiguration
            { Id = 1, Name = nameof(ConfigurationKeys.IsInMaintenanceMode), Value = false.ToString() });
    }
}
