using System.Globalization;
using System.Reflection;
using BPOR.Domain.Entities.RefData;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class HealthConditionConfiguration : IEntityTypeConfiguration<HealthCondition>
{
    private const string HealthConditionResourceName = "BPOR.Domain.Entities.Configuration.HealthConditions.csv";

    public void Configure(EntityTypeBuilder<HealthCondition> builder)
    {
        var healthConditions = LoadHealthConditionArrayFromResource();
        if (healthConditions.Select(healthCondition => healthCondition.Condition).Distinct().Count() != healthConditions.Count)
        {
            throw new ArgumentException($"Health conditions csv contains non-unique conditions.");
        }

        // get a list of all Id of conditions that have superseded another
        // and ensure they all point to a condtion that is not superseded.
        var healthConditionsSupersecedBy = healthConditions.Select(healthConditions => healthConditions.SupersededBy);
        foreach (int supersededBy in healthConditionsSupersecedBy)
        {
            var tempSupersededBy = healthConditions[(supersededBy - 1)];
            if (tempSupersededBy.Id != tempSupersededBy.SupersededBy)
            {
                throw new ArgumentException($"Health condition at {supersededBy -1} is superseded by another condition");
            }
        };

        builder.HasData(healthConditions
            .Select((healthCondition) => new HealthCondition
            {
                Id = healthCondition.Id,
                Code = healthCondition.Condition,
                Description = healthCondition.Condition,
                IsDeleted = false,
                SupersededById = healthCondition.Id == healthCondition.SupersededBy ? null : healthCondition.SupersededBy
            }));
    }

    private static List<HealthconditionCsvResults> LoadHealthConditionArrayFromResource()
    {
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream(HealthConditionResourceName);
        if (stream == null)
        {
            throw new FileNotFoundException($"Resource with name '{HealthConditionResourceName}' not found.");
        }


        using var reader = new StreamReader(stream);
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
           var HealthCondiitonrecords = csv.GetRecords<HealthconditionCsvResults>().ToList();

            return HealthCondiitonrecords;
        }

    }

    private class HealthconditionCsvResults
    {

        public int Id { get; set; }
        public String Condition { get; set; }
        public int SupersededBy { get; set; }

    }
}
