using NIHR.Rts.Client;

namespace BPOR.Rms.VolunteerInformation.Models;

public class SiteSearchResultsModel : SiteSearchModel
{

    public RtsAddress[] SearchResult { get; set; }
    
    public string? SelectedRtsId { get; set; }
}