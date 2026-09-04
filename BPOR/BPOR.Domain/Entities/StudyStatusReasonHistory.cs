using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class StudyStatusReasonHistory : IAudit
{
    public int Id { get; set; }
    public string? AdditionalReasonText { get; set; }
    
    public int StudyStatusHistoryId { get; set; }
    public WithdrawnReasonType? WithdrawnReasonId { get; set; }
    public RejectedReasonType? RejectedReasonId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
    
    public StudyStatusHistory StudyStatusHistory { get; set; } = null!;
    public WithdrawnReason? WithdrawnReason { get; set; }
    public RejectedReason? RejectedReason { get; set; }
}