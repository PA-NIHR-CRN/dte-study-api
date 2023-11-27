namespace DYNAMO.STREAM.HANDLER.Entities;

public class ParticipantIdentifier
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public string Value { get; set; }
    public int? IdentifierTypeId { get; set; }
    public bool IsDeleted { get; set; }
    public IdentifierType Type { get; set; }
}
