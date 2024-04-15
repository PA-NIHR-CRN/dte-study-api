using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Filter
{
    public class IntegerOrDecimalAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // Null values are considered valid
                return ValidationResult.Success;
            }

            decimal number;
            if (!Decimal.TryParse(value.ToString(), out number))
            {
                // Not a valid decimal
                return new ValidationResult("Not a valid number.");
            }

            // Check if the number has no decimal places or exactly one decimal place
            if (Math.Round(number, 1) != number && Math.Round(number, 0) != number)
            {
                return new ValidationResult("Enter a whole number or a number with one decimal place, like 8 or 1.3");
            }

            // Validation passed
            return ValidationResult.Success;
        }
    }
}
