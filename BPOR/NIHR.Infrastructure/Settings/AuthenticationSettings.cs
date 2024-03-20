using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NIHR.Infrastructure.Settings
{
    public class AuthenticationSettings : IValidatableObject
    {
        public static string SectionName => "AuthenticationSettings";
        [Required] public string Authority { get; set; } = string.Empty;
        [Required] public string ClientId { get; set; } = string.Empty;
        [Required] public string ClientSecret { get; set; } = string.Empty;


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Authority))
            {
                yield return new ValidationResult("Authority is required", new[] { nameof(Authority) });
            }

            if (string.IsNullOrWhiteSpace(ClientId))
            {
                yield return new ValidationResult("ClientId is required", new[] { nameof(ClientId) });
            }

            if (string.IsNullOrWhiteSpace(ClientSecret))
            {
                yield return new ValidationResult("ClientSecret is required", new[] { nameof(ClientSecret) });
            }
        }
    }
}
