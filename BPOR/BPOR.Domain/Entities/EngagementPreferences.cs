using System.ComponentModel.DataAnnotations;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class EngagementPreferences : IAudit
{
    [Key]
    public int UserId { get; set; }
    public string? WhereDidYouHear { get; set; }
    public bool CanContactForFeedback { get; set; }
    public bool CanContactAboutResearch { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
}
