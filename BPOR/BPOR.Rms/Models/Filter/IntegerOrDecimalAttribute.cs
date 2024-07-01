using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Filter
{
    public class IntegerOrDecimalAttribute : ValidationAttribute
    {
        /// <summary>
        /// The property marked by this attribute is required if the property named in RequiredIfNotNull is not null.
        /// This facilitates linked properties. If both are null they are optional, if one is specified the other must be specified.
        /// </summary>
        public string? RequiredIfNotNull { get; set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (IsNullPermitted(validationContext) && value == null)
            {
                return ValidationResult.Success;
            }

            if (decimal.TryParse(value?.ToString(), out decimal number))
            {
                if (number >= 0 && (Math.Round(number, 0) == number || Math.Round(number, 1) == number))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }

        private bool IsNullPermitted(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(RequiredIfNotNull))
            {
                return true;
            }

            var linkedPropertyValue = validationContext?.ObjectInstance?.GetType()?.GetProperty(RequiredIfNotNull)?.GetValue(validationContext.ObjectInstance);

            return linkedPropertyValue is null;
        }
    }
}
