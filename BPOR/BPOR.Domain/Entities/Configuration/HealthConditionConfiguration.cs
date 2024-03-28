using System.Reflection;
using BPOR.Domain.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class HealthConditionConfiguration : IEntityTypeConfiguration<HealthCondition>
{
    private const string HealthConditionResourceSuffix = "Configuration/HealthConditions.txt";

    public void Configure(EntityTypeBuilder<HealthCondition> builder)
    {
        var healthConditions = LoadHealthConditionArrayFromResource();
        builder.HasData(healthConditions.Select((hc, i) => new HealthCondition
            { Id = i + 1, Code = hc, Description = hc, IsDeleted = false }));
    }

    private static string[] LoadHealthConditionArrayFromResource()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly
            .GetManifestResourceNames()
            .SingleOrDefault(str =>
                str.EndsWith(HealthConditionResourceSuffix, StringComparison.InvariantCultureIgnoreCase));

        if (string.IsNullOrWhiteSpace(resourceName))
        {
            throw new FileNotFoundException($"Email template resource '{HealthConditionResourceSuffix}' not found.");
        }

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new FileNotFoundException($"Email template resource '{HealthConditionResourceSuffix}' not found.");
        }

        using var reader = new StreamReader(stream);

        return reader.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    }
}
