using BPOR.Domain.Entities;
using BPOR.Infrastructure.Enum;
using BPOR.Infrastructure.Models.Mfa;

namespace BPOR.Infrastructure.Interfaces;

public interface IParticipantService
{
    Task CreateParticipantAsync(DynamoParticipant entity, CancellationToken cancellationToken);
    Task<DynamoParticipant> GetParticipantAsync(string participantId, CancellationToken cancellationToken);
    Task<DynamoParticipant> UpdateParticipantAsync(DynamoParticipant participant, CancellationToken cancellationToken);
    Task DeleteParticipantAsync(string requestParticipantId, CancellationToken cancellationToken);
    Task StoreMfaCodeAsync(string username, string code, CancellationToken cancellationToken);
    Task<MfaValidationResultEnum> ValidateMfaCodeAsync(string username, string code, CancellationToken cancellationToken);
    Task NhsLoginAsync(DynamoParticipant dynamoParticipant, CancellationToken cancellationToken);
    Task<DynamoParticipant> GetParticipantDetailsByEmailAsync(string email, CancellationToken cancellationToken);
}
