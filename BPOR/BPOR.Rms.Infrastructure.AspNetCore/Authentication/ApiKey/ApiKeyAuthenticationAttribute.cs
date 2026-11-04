using Microsoft.AspNetCore.Authorization;

namespace BPOR.Rms.Infrastructure.AspNetCore.Authentication.ApiKey;

public class ApiKeyAuthenticationAttribute : AuthorizeAttribute
{
    public ApiKeyAuthenticationAttribute()
    {
        AuthenticationSchemes = ApiKeyAuthenticationOptions.DefaultScheme;
    }
}