using BPOR.Domain.Enums;

namespace BPOR.Rms.Models.Study;

public class StudyStatusViewModel
{
    public StudyStatusType? StudyStatusId { get; set; }
    public long StudyId { get; set; }
    public string? StudyStatusCode { get; set; }
    public bool CanChangeStatus { get; set; } = true;
}