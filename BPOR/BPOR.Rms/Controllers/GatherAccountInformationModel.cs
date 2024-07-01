using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Controllers
{
    public class GatherAccountInformationModel : IValidatableObject
    {
        [Required(ErrorMessage = "A verification code is required", AllowEmptyStrings = false)]
        public string Code { get; set; } = string.Empty;

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Enter your first name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Enter your last name")]
        public string? LastName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter a password")]
        [MinLength(12, ErrorMessage = "Enter a password that is at least 12 characters long")]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}