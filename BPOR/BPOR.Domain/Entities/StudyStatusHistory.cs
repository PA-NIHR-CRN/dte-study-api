using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class StudyStatusHistory : IAudit
{
    public int Id { get; set; }
    
    public int StudyId { get; set; }
    public StudyStatusType StudyStatusId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
    
    public StudyStatus StudyStatus { get; set; } = null!;
    public Study Study { get; set; } = null!;
}