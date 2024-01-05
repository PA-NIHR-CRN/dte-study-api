using System;
using System.Threading.Tasks;
using Application.Participants.V1.Commands.Participants;
using Application.Responses.V1.Users;
using Application.Users.V1.Commands;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Requests.Users;
using Swashbuckle.AspNetCore.Annotations;
using IAuthenticationService = Application.Contracts.IAuthenticationService;

namespace StudyApi.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IMediator mediator, IAuthenticationService authenticationService)
    {
        _mediator = mediator;
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// [AllowAnonymous] Login
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
    {
        var response = await _mediator.Send(new UserLoginCommand(request.Email, request.Password));
        if (!response.IsSuccess)
        {
            return Ok(response);
        }

        var sessionId = Guid.NewGuid().ToString();
        await _authenticationService.CreateSessionAndLoginAsync(response.Content, sessionId);
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
    public async Task<IActionResult> SignUpUserAsync([FromBody] SignUpRequest request)
    {
        var response = await _mediator.Send(new SignUpCommand(request.Email, request.Password));

        await _mediator.Send(new CreateParticipantDetailsCommand(
            response.Content.UserId, request.Email, request.Firstname, request.Lastname,
            request.ConsentRegistration, null, request.DateOfBirth, ""));

        return Ok(new { IsSuccess = response.IsSuccess });
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <response code="204">When IsSuccess true</response>
    [Authorize("AnyAuthenticatedUser")]
    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        await _authenticationService.DeleteSessionAndSignOutAsync();
        return NoContent();
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
}
