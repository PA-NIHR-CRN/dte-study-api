namespace NIHR.Rts.Client;

public interface ITokenService
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken);
    Task<string> RefreshAccessTokenAsync(CancellationToken cancellationToken);
}