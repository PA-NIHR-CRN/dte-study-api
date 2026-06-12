using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.VolunteerInformation.Models;

public class SiteSearchModel
{
        [Display(Description = "Enter a postcode to select one or multiple research locations")]
        public string SearchTerm { get; set; }
}