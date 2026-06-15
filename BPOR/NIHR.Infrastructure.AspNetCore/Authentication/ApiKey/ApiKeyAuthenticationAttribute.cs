using Microsoft.AspNetCore.Authorization;

namespace NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;

public class ApiKeyAuthenticationAttribute : AuthorizeAttribute
{
    public ApiKeyAuthenticationAttribute()
    {
        AuthenticationSchemes = ApiKeyAuthenticationOptions.DefaultScheme;
    }
}