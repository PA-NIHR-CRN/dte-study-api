namespace BPOR.Rms.Models.Volunteer;

public abstract class UpdateRecruitedBase
{
    public string? StudyName { get; set; }
    public int StudyId { get; set; }
    public bool HasCampaigns { get; set; }
}