using DYNAMO.STREAM.HANDLER.Entities;

namespace DYNAMO.STREAM.HANDLER.Contracts;

public interface IAuroraRepository
{
    Task<Participant?> GetParticipantAsync(Participant participant, CancellationToken cancellationToken);
    Task<Participant?> GetParticipantByIdAsync(string participantId, CancellationToken cancellationToken);
    Task<bool> IsLinkedAccountAsync(Participant participant, CancellationToken cancellationToken);
}
