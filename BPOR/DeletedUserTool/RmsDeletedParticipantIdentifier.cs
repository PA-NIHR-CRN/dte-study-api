using BPOR.Domain.Enums;

namespace DeletedUserTool;

public record RmsDeletedParticipantIdentifier(
    int IdentifierId,
    IdentifierTypes IdentifierTypeId,
    Guid IdentifierValue);