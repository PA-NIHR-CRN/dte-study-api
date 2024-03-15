using Amazon.CognitoIdentityProvider.Model;
using BPOR.Infrastructure.Interfaces;
using BPOR.Registration.Api.Extensions;
using Dte.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ChangePasswordRequest = BPOR.Registration.Api.Requests.Password.ChangePasswordRequest;
using ConfirmForgotPasswordRequest = BPOR.Registration.Api.Requests.Password.ConfirmForgotPasswordRequest;
using ForgotPasswordRequest = BPOR.Registration.Api.Requests.Password.ForgotPasswordRequest;

namespace BPOR.Registration.Api.Controllers.V1;

public class PasswordController : Controller
{
    private readonly IPasswordService _passwordService;

    public PasswordController(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }

    /// <summary>
    /// [AllowAnonymous] Forgot Password Email
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ForgotPasswordResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("forgotpassword")]
    public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _passwordService.ForgotPasswordAsync(request.Email, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// [AllowAnonymous] Confirm Forgot Password endpoint
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ConfirmForgotPasswordResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("confirmforgotpassword")]
    public async Task<IActionResult> ConfirmForgotPasswordAsync([FromBody] ConfirmForgotPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var response =
            await _passwordService.ConfirmForgotPasswordAsync(request.Code, request.UserId, request.Password,
                cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// [AllowAnonymous] Get the current Cognito password policy
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [HttpGet("passwordpolicy")]
    public async Task<IActionResult> GetPasswordPolicy(CancellationToken cancellationToken)
    {
        return Ok(await _passwordService.GetPasswordPolicyTypeAsync(cancellationToken));
    }

    /// <summary>
    /// [AllowAnonymous] Change user password
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [Authorize("AnyAuthenticatedUser")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("changepassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request,
        CancellationToken cancellationToken)
    {
        var email = User.GetUserEmail();
        return Ok(await _passwordService.ChangePasswordAsync(email, request.NewPassword, cancellationToken));
    }
}
