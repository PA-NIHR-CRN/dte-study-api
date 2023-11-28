using DYNAMO.STREAM.HANDLER.Entities;

namespace DYNAMO.STREAM.HANDLER.Contracts;

public interface IAuroraRepository
{
    Task<Participant?> GetParticipantAsync(Participant participant, CancellationToken cancellationToken);
    Task<Participant?> GetParticipantByPkAsync(string pk, CancellationToken cancellationToken);
    bool IsLinkedAccount(Participant participant);
}
