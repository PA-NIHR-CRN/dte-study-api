using System;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Models.MFA;
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
[Route("api/mfa")]
[AllowAnonymous]
public class MfaController: Controller
{
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authenticationService;

    public MfaController(IUserService userService, IAuthenticationService authenticationService)
    {
        _userService = userService;
        _authenticationService = authenticationService;
    }
    
    /// <summary>
    /// [AllowAnonymous] RespondToMfaChallenge
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("respondtomfachallenge")]
    public async Task<IActionResult> RespondToMfaChallengeAsync([FromBody] RespondToMfaRequest request)
    {
        var response =
            await _userService.RespondToMfaChallengeAsync(request.MfaCode, request.MfaDetails);

        if (!response.IsSuccess)
        {
            return Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors));
        }

        var sessionId = Guid.NewGuid().ToString();
        await _authenticationService.CreateSessionAndLoginAsync(response.Content, sessionId);
        return Ok(response);
    }

    /// <summary>
    /// [AllowAnonymous] RespondToMfaChallenge
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("respondtototpmfachallenge")]
    public async Task<IActionResult> RespondToTotpMfaChallengeAsync([FromBody] RespondToMfaRequest request)
    {
        var response =
            await _userService.RespondToTotpMfaChallengeAsync(request.MfaCode, request.MfaDetails);

        if (!response.IsSuccess)
        {
            return Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors));
        }

        var sessionId = Guid.NewGuid().ToString();
        await _authenticationService.CreateSessionAndLoginAsync(response.Content, sessionId);
        return Ok(response);
    }
    
        /// <summary>
    /// [AllowAnonymous] Login
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("setupsmsmfa")]
    public async Task<IActionResult> SetUpMfaAsync([FromBody] SetUpMfaRequest request)
    {
        await _userService.UpdateCognitoPhoneNumberAsync(request.MfaDetails, request.PhoneNumber);
        var response = await _userService.SetUpMfaAsync(request.MfaDetails);

        return !response.IsSuccess
            ? Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors))
            : Ok(response);
    }
        
    /// <summary>
    /// [AllowAnonymous] Login
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("reissuesession")]
    public async Task<IActionResult> ReissueMfaSession([FromBody] SetUpMfaRequest request)
    {
        var response = await _userService.ReissueMfaSessionAsync(request.MfaDetails);
            
        return !response.IsSuccess
            ? Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors))
            : Ok(response);
    }
        
    /// <summary>
    /// [AllowAnonymous] Login
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("sendmfaotpemail")]
    public async Task<IActionResult> SendMfaOtpEmail([FromBody] SetUpMfaRequest request)
    {
        var email = await _userService.SendEmailOtpAsync(request.MfaDetails);
        return Ok(email);
    }
    
        /// <summary>
    /// [AllowAnonymous] Login
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("getmaskedmobile")]
    public async Task<IActionResult> GetMaskedMobile([FromBody] SetUpMfaRequest request)
    {
        var maskedMobile = await _userService.GetMaskedMobile(request.MfaDetails);
            
        return Ok(maskedMobile);
            
    }
        
    /// <summary>
    /// [AllowAnonymous] ValidateEmailOtp
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("validatemfaotpemail")]
    public async Task<IActionResult> ValidateEmailOtp([FromBody] RespondToMfaRequest request)
    {
        var response = await _userService.ValidateEmailOtpAsync(request.MfaDetails, request.MfaCode);

        return !response.IsSuccess
            ? Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors))
            : Ok(response);
    }

    /// <summary>
    /// [AllowAnonymous] ResendMfaChallenge
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("resendmfachallenge")]
    public async Task<IActionResult> ResendMfaChallenge([FromBody] SetUpMfaRequest request)
    {
        var response = await _userService.ResendMfaChallenge(request.MfaDetails);

        return !response.IsSuccess
            ? Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors))
            : Ok(response);
    }

    /// <summary>
    /// [AllowAnonymous] SetUpTokenMfaAsync
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("setuptokenmfa")]
    public async Task<IActionResult> SetUpTokenMfaAsync([FromBody] SetUpMfaRequest request)
    {
        var response = await _userService.GenerateTotpToken(request.MfaDetails);

        return Ok(Response<TotpTokenResult>.CreateSuccessfulContentResponse(response));
    }
    
    
    /// <summary>
    /// [AllowAnonymous] VerifySoftwareTokenAsync
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("verifytokenmfa")]
    public async Task<IActionResult> VerifySoftwareTokenAsync([FromBody] VerifyMfaRequest request)
    {
        var response = await _userService.VerifySoftwareTokenAsync(request.AuthenticatorAppCode, request.SessionId, request.MfaDetails);

        return Ok(response);
    }
}
