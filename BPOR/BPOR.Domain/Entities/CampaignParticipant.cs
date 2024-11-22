using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class CampaignParticipant : IAudit, ISoftDelete
{
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public ContactMethods CampaignTypeId { get; set; }
    public int ParticipantId { get; set; }
    public int? DeliveryStatusId { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? RegisteredInterestAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
    public bool IsDeleted { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public Campaign Campaign { get; set; }
    public Participant Participant { get; set; }
}