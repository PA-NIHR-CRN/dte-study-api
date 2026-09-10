using BPOR.Domain.Enums;

public class RmsParticipantIdentifierAudit
{
    public int Id { get; set; }
    public IdentifierTypes IdentifierTypeId { get; set; }
    public Guid Value { get; set; }
    public List<DynamoDbAudit> DynamoDbRecords { get; set; } = new();
}