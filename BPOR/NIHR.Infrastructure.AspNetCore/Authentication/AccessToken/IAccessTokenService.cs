using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public interface IAccessTokenService
{
    string EncryptAccessToken(AccessToken token, TimeSpan? lifetime = null);
    string DecryptAccessToken(string token);
    AccessToken DeserializeClaim(string claim);
}

public interface IUrlAccessTokenService
{
    string AddAccessToken(string uri, AccessToken token, TimeSpan? lifetime = null);
    string AddCurrentAccessToken(string uri);
}

public class UrlAccessTokenService (IAccessTokenService accessTokenService, IHttpContextAccessor httpContextAccessor, IOptions<AccessTokenAuthenticationOptions> options) : IUrlAccessTokenService
{
    public string AddAccessToken(string uri, AccessToken token, TimeSpan? lifetime = null)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { options.Value.QueryParameterName, accessTokenService.EncryptAccessToken(token, lifetime) }
        };

        var result = QueryHelpers.AddQueryString(uri, queryParams);
        return result;
    }
    
    public string AddCurrentAccessToken(string uri)
    {
        if (httpContextAccessor.HttpContext.Request.Query.TryGetValue(options.Value.QueryParameterName,
                out var accessToken))
        {
            var queryParams = new Dictionary<string, string?>
            {
                { options.Value.QueryParameterName, accessToken.ToString() }
            };

            uri = QueryHelpers.AddQueryString(uri, queryParams);
        }

        return uri;
    }
}