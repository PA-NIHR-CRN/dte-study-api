using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class StudyParticipantEnrollment : IAudit
{
    public int Id { get; set; }
    public int StudyId { get; set; }
    public int ParticipantId { get; set; }
    public string Reference { get; set; }
    public DateTime EnrolledAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
}
