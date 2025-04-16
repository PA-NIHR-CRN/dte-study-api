using NIHR.Infrastructure.EntityFrameworkCore;
using BPOR.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPOR.Domain.Entities;

[Table("Campaigns")]
public class Campaigns : IAudit, ISoftDelete
{
    public int Id { get; set; }
    public int FilterCriteriaId { get; set; }
    public Guid TemplateId { get; set; }
    public ContactMethods TypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? PreviewRecipients { get; set; }
    public int? TargetGroupSize { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<CampaignParticipants> Participants { get; set; } = new List<CampaignParticipants>();

    public FilterCriteria FilterCriteria { get; set; } = null!;
}