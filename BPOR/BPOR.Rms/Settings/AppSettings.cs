using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Settings;

public class AppSettings : IValidatableObject
{
    public static string SectionName => "AppSettings";
    [Required] public string BaseUrl { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(BaseUrl))
        {
            yield return new ValidationResult("BaseUrl is required", new[] { nameof(BaseUrl) });
        }
    }
}
