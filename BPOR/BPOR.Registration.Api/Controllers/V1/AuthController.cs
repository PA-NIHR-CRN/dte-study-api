using BPOR.Domain.Entities;
using BPOR.Infrastructure.Interfaces;
using BPOR.Infrastructure.Responses.V1.Users;
using BPOR.Registration.Api.Constants;
using BPOR.Registration.Api.Extensions;
using BPOR.Registration.Api.Requests.Auth;
using Dte.Common.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SignUpRequest = BPOR.Registration.Api.Requests.Auth.SignUpRequest;

namespace BPOR.Registration.Api.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/auth")]
public class AuthController(
    ISessionService sessionService,
    IAuthService authService,
    ISignUpService signUpService,
    IParticipantService participantService)
    : Controller
{
    /// <summary>
    /// [AllowAnonymous] Login
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(request.Email, request.Password, cancellationToken);
        if (!response.IsSuccess)
        {
            return Ok(response);
        }

        var sessionId = Guid.NewGuid().ToString();
        await authService.CreateSessionAndLoginAsync(response.Content, sessionId, cancellationToken);
        return Ok(response);
    }
    
    /// <summary>
    /// [AllowAnonymous] SignUp user
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<SignUpResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUpUserAsync([FromBody] SignUpRequest request, CancellationToken cancellationToken)
    {
        var response = await signUpService.SignUpAsync(request.Email, request.Password, request.SelectedLocale, cancellationToken);

        if (response.IsSuccess)
        {
            var participant = new DynamoParticipant
            {
                ParticipantId = response.Content.UserId,
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                ConsentRegistration = request.ConsentRegistration,
                DateOfBirth = request.DateOfBirth,
                SelectedLocale = request.SelectedLocale
            };
            
            await participantService.CreateParticipantAsync(participant, cancellationToken);
        }

        return Ok(response);
    }
    
    /// <summary>
    /// Dummy endpoint to refresh session cookie
    /// </summary>
    /// <response code="204">When IsSuccess true</response>
    [HttpGet("refreshsession")]
    public async Task<IActionResult> RefreshSession()
    {
        return NoContent();
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <response code="204">When IsSuccess true</response>
    [HttpPost("logout")]
    public async Task<IActionResult> LogOut(CancellationToken cancellationToken)
    {
        await sessionService.DeleteSessionAsync(User.GetParticipantId(), cancellationToken);
        HttpContext.Response.Cookies.Delete(Cookies.NhsAccessToken);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return NoContent();
    }
}
