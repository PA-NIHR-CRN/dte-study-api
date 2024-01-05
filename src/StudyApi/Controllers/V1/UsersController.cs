using System.Threading.Tasks;
using Application.Contracts;
using Application.Extensions;
using Application.Responses.V1.Users;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Requests.Users;
using Swashbuckle.AspNetCore.Annotations;
using ConfirmSignUpRequest = StudyApi.Requests.Users.ConfirmSignUpRequest;

namespace StudyApi.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IParticipantService _participantService;

    public UsersController(IUserService userService, IParticipantService participantService)
    {
        _userService = userService;
        _participantService = participantService;
    }

    /// <summary>
    /// [AllowAnonymous] Resend Confirmation Email
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ResendConfirmationCodeResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("resendverificationemail")]
    public async Task<IActionResult> ResendVerificationEmailAsync([FromBody] ResendVerificationEmailRequest request)
    {
        return Ok(_userService.ResendVerificationEmailAsync(request.UserId));
    }

    /// <summary>
    /// [AllowAnonymous] Confirm the user's signup using the confirmation code
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("confirmsignup")]
    public async Task<IActionResult> ConfirmSignUpAsync([FromBody] ConfirmSignUpRequest request)
    {
        var response = await _userService.ConfirmSignUpAsync(request.Code, request.UserId);

        return Ok(new { IsSuccess = response.IsSuccess });
    }

    /// <summary>
    /// [AnyAuthenticatedUser] Delete own user account
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [Authorize("AnyAuthenticatedUser")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpDelete("deleteparticipantaccount")]
    public async Task<IActionResult> DeleteParticipantAccount()
    {
        return Ok(await _participantService.DeleteUserAsync(User.GetParticipantId()));
    }

    /// <summary>
    /// [AllowAnonymous] Change user email
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [Authorize("AnyAuthenticatedUser")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("changeemail")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
    {
        var currentEmail = User.GetUserEmail();

        var clientResponse = await _userService.ChangeEmailAsync(currentEmail, request.NewEmail);

        if (clientResponse != null && clientResponse.IsSuccess)
        {
            await _participantService.UpdateParticipantEmailAsync(User.GetParticipantId(), request.NewEmail);
        }

        return Ok(clientResponse);
    }
}
