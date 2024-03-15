using Amazon.CognitoIdentityProvider.Model;
using BPOR.Infrastructure.Interfaces;
using BPOR.Registration.Api.Extensions;
using BPOR.Registration.Api.Requests.Users;
using Dte.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ConfirmSignUpRequest = BPOR.Registration.Api.Requests.Users.ConfirmSignUpRequest;

namespace BPOR.Registration.Api.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/users")]
public class UserController(IUserService userService)
    : Controller
{
    /// <summary>
    /// [AllowAnonymous] Resend Confirmation Email
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ResendConfirmationCodeResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("resendverificationemail")]
    public async Task<IActionResult> ResendVerificationEmailAsync([FromBody] ResendVerificationEmailRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await userService.ResendVerificationEmailAsync(request.UserId, cancellationToken));
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
    public async Task<IActionResult> ConfirmSignUpAsync([FromBody] ConfirmSignUpRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await userService.ConfirmSignUpAsync(request.Code, request.UserId, cancellationToken));
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
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request,
        CancellationToken cancellationToken)
    {
        await userService.ChangeEmailAsync(User.GetUserEmail(), request.NewEmail, cancellationToken);

        return Ok();
    }
}
