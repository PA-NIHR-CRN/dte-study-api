using Microsoft.AspNetCore.Authentication;

namespace NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;

public sealed class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "ApiKey";
    public const string HeaderName = "Authorization";
    
    public List<FixedApiKey> FixedApiKeys { get; set; }
}