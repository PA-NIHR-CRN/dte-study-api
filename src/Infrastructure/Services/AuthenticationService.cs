using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Application.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using IAuthenticationService = Application.Contracts.IAuthenticationService;

namespace Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISessionService _sessionService;

    public AuthenticationService(IHttpContextAccessor httpContextAccessor, ISessionService sessionService)
    {
        _httpContextAccessor = httpContextAccessor;
        _sessionService = sessionService;
    }

    public async Task CreateSessionAndLoginAsync(string jwtToken, string sessionId)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);

        var claims = new List<Claim>
        {
            new("sessionId", sessionId)
        };
        claims.AddRange(token.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
        };

        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        var participantId = token.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

        await _sessionService.SetSessionAsync(participantId, sessionId);
    }

    public async Task DeleteSessionAndSignOutAsync()
    {
        await _sessionService.DeleteSessionAsync( _httpContextAccessor.HttpContext.User.GetParticipantId());
        _httpContextAccessor.HttpContext.Response.Cookies.Delete(CookieNames.NhsAccessToken);
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
