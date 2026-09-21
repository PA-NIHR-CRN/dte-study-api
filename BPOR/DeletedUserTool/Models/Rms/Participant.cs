using BPOR.Domain.Enums;

namespace DeletedUserTool.Models.Rms;

public class Participant
{
    public int Id { get; set; }
    public string? Email { get; set; }
}

public record ParticipantIdentifier (int Id, Guid Value, IdentifierTypes IdentifierTypeId);