namespace BPOR.Rms.Models.Study;

public class StudyStatusReasonViewModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsSelected { get; set; }
}