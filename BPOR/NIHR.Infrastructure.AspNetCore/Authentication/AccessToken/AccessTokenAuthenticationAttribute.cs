using Microsoft.AspNetCore.Authorization;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public class AccessTokenAuthenticationAttribute : AuthorizeAttribute
{
    public AccessTokenAuthenticationAttribute()
    {
        AuthenticationSchemes = AccessTokenAuthenticationOptions.DefaultScheme;
    }
}