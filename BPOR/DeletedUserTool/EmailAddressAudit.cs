public class EmailAddressAudit
{
    public string Email { get; set; }
    public string[] CognitoUserIdsMatchingEmail { get; set; }
    public List<DynamoDbAudit> DynamoDbRecordsMatchingEmail { get; set; } = new();
    public List<RmsParticipantAudit> RmsParticipants { get; set; } = new();
}