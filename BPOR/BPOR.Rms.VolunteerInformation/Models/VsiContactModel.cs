using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.VolunteerInformation.Models;

public class VsiContactModel
{
    public int Id { get; set; }
    
    [Display(Name = "Name")]
    public string Name { get; set; }
    [Display(Name = "Role")]
    public string Role { get; set; }
    [Display(Name = "Organisation")]
    public string Organisation { get; set; }
    [Display(Name = "Email")]
    public string? Email { get; set; }
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }
}