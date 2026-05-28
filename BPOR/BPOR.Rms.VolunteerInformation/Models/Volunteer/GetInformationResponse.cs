namespace BPOR.Rms.VolunteerInformation.Models.Volunteer;

public class GetInformationResponse
{
    public long CampaignParticipantId { get; set; }
    public Participant Participant { get; set; }
    public Study Study { get; set; }
}

public class Participant
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
}

public class Study
{
    public string PrescreenerId { get; set; }
}