namespace BPOR.Infrastructure.Interfaces;

public interface ISessionService
{
    Task DeleteSessionAsync(string participantId, CancellationToken cancellationToken);
    Task SetSessionAsync(string participantId, string? sessionId, CancellationToken cancellationToken);
    Task<bool> ValidateSessionAsync(string participantId, string sessionId, CancellationToken cancellationToken);
}
