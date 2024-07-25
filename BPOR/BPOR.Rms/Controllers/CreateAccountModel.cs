using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Controllers
{
    public class CreateAccountModel
    {
        [Required(ErrorMessage = "Enter your email address")]
        [EmailAddress(ErrorMessage = "Enter an email address in the correct format, like name@example.com")]
        [Display(Order = 1)]
        public string Email { get; set; }
    }
}