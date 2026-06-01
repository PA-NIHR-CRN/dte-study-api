using BPOR.Domain.Entities.RefData;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class StudyResearcherEmail
{
    public int Id { get; set; }
    public int StudyResearcherId { get; set; }
    public int StudyResearcherEmailOptionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedById { get; set; }
    
    public required StudyResearcher StudyResearcher { get; set; }
    public required StudyResearcherEmailOptions StudyResearcherEmailOption { get; set; }

}