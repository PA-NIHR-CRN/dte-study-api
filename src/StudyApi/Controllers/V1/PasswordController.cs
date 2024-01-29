using System.Threading.Tasks;
using Application.Contracts;
using Application.Extensions;
using Application.Responses.V1.Users;
using Dte.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Requests.Users;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApi.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/password")]
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
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ForgotPasswordResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [AllowAnonymous]
    [HttpPost("forgotpassword")]
    public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request)
    {
        return Ok(await _passwordService.ForgotPasswordAsync(request.Email));
    }

    /// <summary>
    /// [AllowAnonymous] Confirm Forgot Password endpoint
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<ConfirmForgotPasswordResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [AllowAnonymous]
    [HttpPost("confirmforgotpassword")]
    public async Task<IActionResult> ConfirmForgotPasswordAsync([FromBody] ConfirmForgotPasswordRequest request)
    {
        return Ok(await _passwordService.ConfirmForgotPasswordAsync(request.Code, request.UserId, request.Password));
    }

    /// <summary>
    /// [AllowAnonymous] Get the current Cognito password policy
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    /// [AllowAnonymous]
    [HttpGet("passwordpolicy")]
    public async Task<IActionResult> GetPasswordPolicy()
    {
        return Ok(await _passwordService.GetPasswordPolicyTypeAsync());
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
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        return Ok(await _passwordService.ChangePasswordAsync(User.GetUserEmail(), request.NewPassword));
    }
}
