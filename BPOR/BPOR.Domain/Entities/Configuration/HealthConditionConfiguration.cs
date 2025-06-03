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
        var errorsList = new List<Exception>();
        var distinctConditions = new HashSet<String>(StringComparer.OrdinalIgnoreCase);
        var healthConditionsDict = healthConditions.ToDictionary(healthConditions => healthConditions.Id, healthConditions => healthConditions);

        // Validation
        foreach (HealthConditionRow healthConditionRow in healthConditions)
        {

            if (!distinctConditions.Add(healthConditionRow.Condition))
            {
                errorsList.Add(new ArgumentException($"Health conditions {healthConditionRow.Condition}, at {healthConditionRow.Id} is a duplicate value"));
            }
            if (String.IsNullOrWhiteSpace(healthConditionRow.Condition))
            {
                errorsList.Add(new ArgumentException($"Health condition at {healthConditionRow.Id} is empty"));
            }

            if (healthConditionRow.SupersededById != null)
            {
                var tempSupersededBy = healthConditionsDict[(int)healthConditionRow.SupersededById];

                if (tempSupersededBy == null)
                {
                    errorsList.Add(new ArgumentException($"Health condition {healthConditionRow.Condition} at {healthConditionRow.Id} has a superseded value that does not appear in the csv"));
                }
                else
                {
                    // for conditions that are not superseded the supersededid = id
                    if (tempSupersededBy != null && tempSupersededBy.Id != tempSupersededBy.SupersededById)
                    {
                        errorsList.Add(new ArgumentException($"Health Condition {healthConditionRow.Condition} is superseded by Health Condition {tempSupersededBy.Condition} which is itself superseded"));
                    }
                }
            }
        };
        if (errorsList.Any())
        {
            throw new AggregateException($"one or more errors occured when validating the health conditions csv file.",errorsList);
        }

        builder.HasData(healthConditions
            .Select((healthCondition) => new HealthCondition
            {
                Id = healthCondition.Id,
                Code = healthCondition.Condition,
                Description = healthCondition.Condition,
                IsDeleted = false,
                // sheet is provided with all superseded values filled in, so when a superseded value = current id, condition is not superseded
                SupersededById = healthCondition.Id == healthCondition.SupersededById ? null : healthCondition.SupersededById
            }));
    }

    private static List<HealthConditionRow> LoadHealthConditionArrayFromResource()
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
           var healthConditionRecords = csv.GetRecords<HealthConditionRow>().ToList();

            return healthConditionRecords;
        }

    }

    private class HealthConditionRow
    {

        public int Id { get; set; }
        public string Condition { get; set; }
        public int SupersededById { get; set; }
        public string SupersededbyCondition { get; set; }

    }
}
