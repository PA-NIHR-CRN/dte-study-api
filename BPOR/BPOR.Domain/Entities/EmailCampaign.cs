using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class EmailCampaign : IAudit, ISoftDelete
{
    public int Id { get; set; }
    public int FilterCriteriaId { get; set; }
    public Guid EmailTemplateId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? PreviewRecipients { get; set; }
    public int? TargetGroupSize { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<EmailCampaignParticipant> Participants { get; set; } = new List<EmailCampaignParticipant>();
}
