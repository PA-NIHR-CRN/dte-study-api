using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BPOR.Rms.Models.Filter
{
    public class UKPostcodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var postcode = value as string;
            if (string.IsNullOrWhiteSpace(postcode))
            {
                return ValidationResult.Success;
            }

            // UK postcode regex pattern
            var regexPattern = @"^(GIR 0AA|[A-PR-UWYZ]([0-9]{1,2}|([A-HK-Y][0-9]|[A-HK-Y][0-9]([0-9]|[ABEHMNPRV-Y]))|[0-9][A-HJKS-UW])( [0-9][ABD-HJLNP-UW-Z]{2})?)$";

            if (!Regex.IsMatch(postcode, regexPattern, RegexOptions.IgnoreCase))
            {
                return new ValidationResult("Invalid UK postcode format.");
            }

            // Validation passed
            return ValidationResult.Success;
        }
    }
}
