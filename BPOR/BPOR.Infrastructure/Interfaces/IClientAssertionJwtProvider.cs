namespace BPOR.Infrastructure.Interfaces;

public interface IClientAssertionJwtProvider
{
    Task<string> CreateClientAssertionJwtAsync(CancellationToken cancellationToken);
    Task<string> CreateSsoJwtAsync(string jti, CancellationToken cancellationToken);
}
