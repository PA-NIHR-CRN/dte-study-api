using System.ComponentModel.DataAnnotations;

namespace BPOR.Domain.Settings;

public class EmailSettings : IValidatableObject
{
    public const string SectionName = "EmailSettings";
    public string FromAddress { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(FromAddress))
        {
            yield return new ValidationResult("FromAddress is required", new[] { nameof(FromAddress) });
        }
    }
}
