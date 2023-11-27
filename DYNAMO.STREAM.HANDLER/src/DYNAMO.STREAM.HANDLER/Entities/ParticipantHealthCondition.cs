namespace DYNAMO.STREAM.HANDLER.Entities;

public class ParticipantHealthCondition
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int? HealthConditionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
