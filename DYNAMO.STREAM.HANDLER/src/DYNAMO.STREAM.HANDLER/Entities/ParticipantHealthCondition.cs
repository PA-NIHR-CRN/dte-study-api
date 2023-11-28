using DYNAMO.STREAM.HANDLER.Entities.RefData;

namespace DYNAMO.STREAM.HANDLER.Entities;

public class ParticipantHealthCondition : ISoftDelete, IAudited
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
