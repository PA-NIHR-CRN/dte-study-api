namespace BPOR.Rms.Models.Study;

public class StudyStatusViewModel
{
    public string? StudyStatus { get; set; }
    public long StudyId { get; set; }
    public bool CanChangeStatus { get; set; } = true;
}