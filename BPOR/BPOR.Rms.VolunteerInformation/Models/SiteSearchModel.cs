using System.ComponentModel.DataAnnotations;
using NIHR.Rts.Client;

namespace BPOR.Rms.VolunteerInformation.Models;

public class SiteSearchModel
{
    [Display(Description = "Enter a postcode to select one or multiple research locations")]
    public string SearchTerm { get; set; }
    public RtsAddress[] SearchResult { get; set; }
    
    public int? SelectedRtsId { get; set; }
}