using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Entities.RefData;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class EmailCampaignParticipant : IAudit, ISoftDelete
{
    public int Id { get; set; }
    public int EmailCampaignId { get; set; }
    public int ParticipantId { get; set; }
    public int? DeliveryStatusId { get; set; }
    [MaxLength(255)] public string ContactEmail { get; set; } = string.Empty;
    public DateTime? SentAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? RegisteredInterestAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
    public bool IsDeleted { get; set; }
    public EmailDeliveryStatus DeliveryStatus { get; set; }
    public EmailCampaign EmailCampaign { get; set; }
    public Participant Participant { get; set; }
}
