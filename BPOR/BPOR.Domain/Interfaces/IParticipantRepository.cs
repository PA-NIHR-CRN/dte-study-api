using BPOR.Domain.Entities;

namespace BPOR.Domain.Interfaces;

public interface IParticipantRepository
{
    Task<DynamoParticipant> GetParticipantAsync(string participantId, CancellationToken cancellationToken);
    Task CreateParticipantAsync(DynamoParticipant entity, CancellationToken cancellationToken);
    Task<DynamoParticipant> UpdateParticipantAsync(DynamoParticipant entity, CancellationToken cancellationToken);
    Task DeleteParticipantAsync(DynamoParticipant participant, CancellationToken cancellationToken);

    Task<DynamoParticipant> QueryIndexForParticipantAsync(string query, string colName,
        CancellationToken cancellationToken);
}
