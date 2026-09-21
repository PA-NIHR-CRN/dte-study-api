public class RmsParticipantAudit
{
    public int Id { get; set; }
    public List<RmsParticipantIdentifierAudit> ParticipantIdentifierAudits { get; set; } = new();
}