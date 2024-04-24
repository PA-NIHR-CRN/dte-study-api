using NIHR.Infrastructure.Entities;

namespace BPOR.Domain.Entities;

public class StudyResearcher : IAudit, ISoftDelete
{
    public int Id { get; set; }
    public int StudyId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
    public bool IsDeleted { get; set; }
    
    public Study Study { get; set; }
    public User User { get; set; }
}
