namespace BPOR.Rms.Models.Study;

public class StudyStatusViewModel
{
    public long StudyId { get; set; }
    public string? StudyStatusCode { get; set; }
    public bool CanChangeStatus { get; set; } = true;
}