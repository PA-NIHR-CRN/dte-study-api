using BPOR.Domain.Entities.RefData;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class StudyResearcherEmail : IAudit
{
    public int Id { get; set; }
    public string StudyResearcherEmailAddress { get; set; }
    public int StudyResearcherEmailOptionId { get; set; }
    public int DeliveryStatusId { get; set; }
    public int StudyId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
    
    public StudyResearcherEmailOptions StudyResearcherEmailOption { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public Study Study { get; set; }

}