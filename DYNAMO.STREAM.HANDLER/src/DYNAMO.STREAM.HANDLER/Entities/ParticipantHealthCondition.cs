namespace DYNAMO.STREAM.HANDLER.Entities;

public class ParticipantHealthCondition
{
    public int Id { get; set; }
    public string ParticipantId { get; set; }
    public string HealthConditionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
