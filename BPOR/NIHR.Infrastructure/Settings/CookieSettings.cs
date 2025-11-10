using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NIHR.Infrastructure.Settings
{
    public class CookieSettings : IValidatableObject
    {
        public const string SectionName = nameof(CookieSettings);

        public string ServiceName { get; set; }

        public string PolicyLink { get; set; }

        public string Domain { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(ServiceName))
            {
                yield return new ValidationResult("ServiceName is required", new[] { nameof(ServiceName) });
            }

            if (string.IsNullOrWhiteSpace(PolicyLink))
            {
                yield return new ValidationResult("PolicyName is required", new[] { nameof(PolicyLink) });
            }
            
            if (string.IsNullOrWhiteSpace(Domain))
            {
                yield return new ValidationResult("Domain is required", new[] { nameof(Domain) });
            }
        }
    }
}
