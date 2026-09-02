namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public interface IAccessTokenService
{
    string EncryptAccessToken(AccessToken token, TimeSpan? lifetime = null);
    string DecryptAccessToken(string token);
    AccessToken DeserializeClaim(string claim);
}