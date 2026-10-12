using Microsoft.AspNetCore.Authentication;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public sealed class AccessTokenAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string AuthenticationScheme  = "NihrAccessToken";
    public const string ClaimType = "http://schemes.nihr.ac.uk/access-token";
    
    public string QueryParameterName { get; set; } = "AccessToken";
    public string TokenPurpose { get; set; } = "AccessToken";
    public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromDays(5);
}