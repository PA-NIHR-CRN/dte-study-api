using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Controllers
{
    public class GatherAccountInformationModel : IValidatableObject
    {
        [Required(ErrorMessage = "Verification code is required", AllowEmptyStrings = false)]
        public string Code { get; set; } = string.Empty;

        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(12)]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public bool RequiresValidation() => !string.IsNullOrWhiteSpace(FirstName) || !string.IsNullOrWhiteSpace(LastName) || !string.IsNullOrWhiteSpace(Password);
    }
}