using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;

namespace BPOR.Rms.Models.Study;

public class StudyStatusViewModel
{
    public long StudyId { get; set; }
    public string? StudyStatusCode { get; set; }
    public StudyStatusType? StudyStatusId {get; set;}
    public bool CanChangeStatus { get; set; } = true;
    
    public string? WithdrawnOtherReason { get; set; }
    public string? RejectedMiscReason { get; set; }

    public List<StudyStatusReasonViewModel>? RejectedReasons { get; set; } = [];
    public List<StudyStatusReasonViewModel>? WithdrawnReasons { get; set; } = [];
    public List<StudyStatus>? AvailableStatuses { get; set; }
}