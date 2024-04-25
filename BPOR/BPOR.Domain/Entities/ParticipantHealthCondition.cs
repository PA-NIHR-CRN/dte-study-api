using BPOR.Domain.Entities.RefData;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class ParticipantHealthCondition: ISoftDelete, ITimestamped
{
    public ParticipantHealthCondition()
    {
        Participant = null!;
        HealthCondition = null!;
    }

    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int HealthConditionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public Participant Participant { get; set; }
    public HealthCondition HealthCondition { get; set; }
}
