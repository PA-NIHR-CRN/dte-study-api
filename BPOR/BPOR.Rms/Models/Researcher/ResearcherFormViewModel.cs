using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Researcher;

public class ResearcherFormViewModel
{
    [Display(Name = "Email address", Order = 1)]
    [Required(ErrorMessage = "Enter your email address")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Enter an email address in the correct format, like name@example.com")]
    public string? EmailAddress { get; set; }

    [Display(Name = "First name", Order = 2)]
    [Required(ErrorMessage = "Enter your first name")]
    public string? FirstName { get; set; }

    [Display(Name = "Last name", Order = 3)]
    [Required(ErrorMessage = "Enter your last name")]
    public string? LastName { get; set; }

    [Display(Name = "Create your password", Order = 4)]
    [Required(ErrorMessage = "Enter a password")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Enter a password that is at least 12 characters long and does not include any symbols")]
    public string? Password { get; set; }

}
