using Microsoft.AspNetCore.Authentication;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public sealed class AccessTokenAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "AccessToken";
    public const string QueryParameterName = "AccessToken";
    public const string TokenPurpose = "AccessToken";
    
    public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromDays(5);
}