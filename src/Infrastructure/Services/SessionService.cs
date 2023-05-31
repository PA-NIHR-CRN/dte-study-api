using System.Threading.Tasks;
using Application.Contracts;
using Domain.Entities.Participants;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class SessionService : ISessionService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly ILogger<SessionService> _logger;

        public SessionService(IParticipantRepository participantRepository, ILogger<SessionService> logger)
        {
            _participantRepository = participantRepository;
            _logger = logger;
        }

        private async Task<ParticipantDetails> GetParticipant(string participantId)
        {
            if (string.IsNullOrWhiteSpace(participantId))
            {
                _logger.LogWarning("Invalid participant ID");
                return null;
            }

            var participant = await _participantRepository.GetParticipantDetailsAsync(participantId);
            if (participant == null)
            {
                _logger.LogWarning("Participant {ParticipantId} not found in the repository", participantId);
            }

            return participant;
        }

        public async Task<bool> ValidateSessionAsync(string participantId, string sessionId)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                _logger.LogWarning("Invalid session ID");
                return false;
            }

            var participant = await GetParticipant(participantId);

            _logger.LogInformation("Current session ID: {CurrentSessionId}", sessionId);
            _logger.LogInformation("Saved Participant session ID: {SavedSessionId}", participant?.SessionId);

            return participant?.SessionId == sessionId;
        }

        public async Task SetSessionAsync(string participantId, string sessionId)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                _logger.LogWarning("Invalid session ID");
                return;
            }

            var participant = await GetParticipant(participantId);
            if (participant == null) return;

            participant.SessionId = sessionId;
            await _participantRepository.UpdateParticipantDetailsAsync(participant);

            _logger.LogInformation("Session ID {SessionId} set for participant: {ParticipantId}", sessionId,
                participantId);
        }

        public async Task DeleteSessionAsync(string participantId)
        {
            var participant = await GetParticipant(participantId);
            if (participant == null) return;

            participant.SessionId = null;
            await _participantRepository.UpdateParticipantDetailsAsync(participant);

            _logger.LogInformation("Session ID deleted for participant: {ParticipantId}", participantId);
        }
    }
}