using NIHR.Rts.Client;

namespace BPOR.Rms.VolunteerInformation.Models;

public class ManualSiteEntryModel
{
    public int StudyId { get; set; }
    public RtsAddress Address { get; set; } = new RtsAddress();
}

