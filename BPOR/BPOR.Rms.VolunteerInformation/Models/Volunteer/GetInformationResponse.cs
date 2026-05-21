namespace BPOR.Rms.Api.Models.Volunteer;

public class GetInformationResponse
{
    public long CampaignParticipantId { get; set; }
    public long StudyId { get; set; }
    public long ParticipantId { get; set; }
    public string ParticipantEmail { get; set; }
    public string ParticipantName { get; set; }
}