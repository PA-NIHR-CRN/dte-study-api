using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class User : IAudit, ISoftDelete
{
    public int Id { get; set; }
    public string AuthenticationId { get; set; }
    public string ContactFullName { get; set; }
    public string ContactEmail { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
    public bool IsDeleted { get; set; }
    public EngagementPreferences? EngagementPreferences { get; set; }
    public ICollection<StudyResearcher> StudyResearchers { get; set; } = new List<StudyResearcher>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
