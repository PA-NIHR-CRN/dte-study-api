using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NIHR.Infrastructure.Settings
{
    public class GovNotifySettings : IValidatableObject
    {
        public static string SectionName => "GovNotifySettings";
        public string ApiKey { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                yield return new ValidationResult("ApiKey is required", new[] { nameof(ApiKey) });
            }
        }
    }
}
