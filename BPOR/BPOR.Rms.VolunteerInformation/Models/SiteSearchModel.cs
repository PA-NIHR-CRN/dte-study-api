using NIHR.Rts.Client;

namespace BPOR.Rms.VolunteerInformation.Models;

public class SiteSearchModel
{
    public int StudyId { get; set; }
    public string SearchTerm { get; set; }
    public RtsAddress[] SearchResult { get; set; }
}