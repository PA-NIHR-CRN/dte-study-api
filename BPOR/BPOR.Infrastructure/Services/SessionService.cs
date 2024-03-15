using BPOR.Domain.Entities;
using BPOR.Domain.Interfaces;
using BPOR.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace BPOR.Infrastructure.Services;

public class SessionService(IParticipantRepository participantRepository, ILogger<SessionService> logger)
    : ISessionService
{
    private async Task<DynamoParticipant> GetParticipant(string participantId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(participantId))
        {
            logger.LogWarning("Invalid participant ID");
            return null;
        }

        var participant = await participantRepository.GetParticipantAsync(participantId, cancellationToken);
        if (participant == null)
        {
            logger.LogWarning("Participant {ParticipantId} not found in the repository", participantId);
        }

        return participant;
    }

    public async Task DeleteSessionAsync(string participantId, CancellationToken cancellationToken)
    {
        var participant = await GetParticipant(participantId, cancellationToken);
        if (participant == null)
        {
            return;
        }

        participant.SessionId = null;
        await participantRepository.UpdateParticipantAsync(participant, cancellationToken);

        logger.LogInformation("Session ID deleted for participant: {ParticipantId}", participantId);
    }

    public async Task SetSessionAsync(string participantId, string? sessionId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            logger.LogWarning("Invalid session ID");
            return;
        }

        var participant = await GetParticipant(participantId, cancellationToken);
        if (participant == null)
        {
            return;
        }

        participant.SessionId = sessionId;
        await participantRepository.UpdateParticipantAsync(participant, cancellationToken);

        logger.LogInformation("Session ID {SessionId} set for participant: {ParticipantId}", sessionId,
            participantId);
    }

    public async Task<bool> ValidateSessionAsync(string participantId, string sessionId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            logger.LogWarning("Invalid session ID");
            return false;
        }

        var participant = await GetParticipant(participantId, cancellationToken);

        logger.LogInformation("Current session ID: {CurrentSessionId}", sessionId);
        logger.LogInformation("Saved Participant session ID: {SavedSessionId}", participant?.SessionId);

        return participant?.SessionId == sessionId;
    }
}
