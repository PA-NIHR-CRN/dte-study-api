using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Interfaces;
using NIHR.Infrastructure.Settings;

namespace BPOR.Rms.Controllers;

public class AccountController(IOptions<IdentityProviderApiSettings> config, IIdentityProviderService identityProviderService) : Controller
{
    public async Task<IActionResult> Logout()
    {
        var idToken = await identityProviderService.GetOrAcquireTokenAsync();
        
        // Redirect to WSO2 IS for logout
        var callbackUrl = Url.Action("Index", "Study", null, protocol: Request.Scheme);
        var logoutUrl =
            $"{config.Value.BaseUrl}/oidc/logout?post_logout_redirect_uri={Uri.EscapeDataString(callbackUrl)}&id_token_hint={idToken}";
        
        // Sign out of the application
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Redirect(logoutUrl);
    }
}
