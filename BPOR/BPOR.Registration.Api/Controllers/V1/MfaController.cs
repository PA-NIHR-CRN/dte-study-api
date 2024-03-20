using BPOR.Infrastructure.Interfaces;
using BPOR.Infrastructure.Models.Mfa;
using BPOR.Infrastructure.Responses.V1.Users;
using BPOR.Registration.Api.Requests.Mfa;
using Dte.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BPOR.Registration.Api.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/mfa")]
public class MfaController(
    IRespondToMfaChallengeService respondToMfaChallengeService,
    IAuthService authService,
    IMfaService mfaService)
    : Controller
{
    /// <summary>
    /// [AllowAnonymous] RespondToMfaChallenge
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("respondtomfachallenge")]
    public async Task<IActionResult> RespondToMfaChallengeAsync([FromBody] RespondToMfaRequest request,
        CancellationToken cancellationToken)
    {
        var response =
            await respondToMfaChallengeService.RespondToMfaChallengeAsync(request.MfaCode, request.MfaDetails);

        if (!response.IsSuccess)
        {
            return Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors));
        }

        var sessionId = Guid.NewGuid().ToString();
        await authService.CreateSessionAndLoginAsync(response.Content, sessionId, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// [AllowAnonymous] SetUpMfaAsync
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("setupsmsmfa")]
    public async Task<IActionResult> SetUpMfaAsync([FromBody] SetUpMfaRequest request,
        CancellationToken cancellationToken)
    {
        await mfaService.UpdateCognitoPhoneNumberAsync(request.MfaDetails, request.PhoneNumber, cancellationToken);
        var response = await mfaService.SetUpMfaAsync(request.MfaDetails, cancellationToken);

        return !response.IsSuccess
            ? Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors))
            : Ok(response);
    }

    /// <summary>
    /// [AllowAnonymous] Login
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("reissuesession")]
    public async Task<IActionResult> ReissueMfaSession([FromBody] SetUpMfaRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mfaService.ReissueMfaSessionAsync(request.MfaDetails, cancellationToken);

        return !response.IsSuccess
            ? Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors))
            : Ok(response);
    }

    /// <summary>
    /// [AllowAnonymous] Login
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("sendmfaotpemail")]
    public async Task<IActionResult> SendMfaOtpEmail([FromBody] SetUpMfaRequest request,
        CancellationToken cancellationToken)
    {
        var email = await mfaService.SendEmailOtpAsync(request.MfaDetails, cancellationToken);
        return Ok(email);
    }

    /// <summary>
    /// [AllowAnonymous] Login
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("getmaskedmobile")]
    public async Task<IActionResult> GetMaskedMobile([FromBody] SetUpMfaRequest request)
    {
        var maskedMobile = await mfaService.GetMaskedMobile(request.MfaDetails);

        return Ok(maskedMobile);
    }

    /// <summary>
    /// [AllowAnonymous] ValidateEmailOtp
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("validatemfaotpemail")]
    public async Task<IActionResult> ValidateEmailOtp([FromBody] RespondToMfaRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mfaService.ValidateEmailOtpAsync(request.MfaDetails, request.MfaCode, cancellationToken);

        return !response.IsSuccess
            ? Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors))
            : Ok(response);
    }

    /// <summary>
    /// [AllowAnonymous] ResendMfaChallenge
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("resendmfachallenge")]
    public async Task<IActionResult> ResendMfaChallenge([FromBody] SetUpMfaRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mfaService.ResendMfaChallengeAsync(request.MfaDetails, cancellationToken);

        return !response.IsSuccess
            ? Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors))
            : Ok(response);
    }

    /// <summary>
    /// [AllowAnonymous] SetUpTokenMfaAsync
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("setuptokenmfa")]
    public async Task<IActionResult> SetUpTokenMfaAsync([FromBody] SetUpMfaRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mfaService.GenerateTotpTokenAsync(request.MfaDetails, cancellationToken);

        return Ok(Response<TotpTokenResult>.CreateSuccessfulContentResponse(response));
    }

    /// <summary>
    /// [AllowAnonymous] VerifySoftwareTokenAsync
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("verifytokenmfa")]
    public async Task<IActionResult> VerifySoftwareTokenAsync([FromBody] VerifyMfaRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mfaService.VerifySoftwareTokenAsync(request.AuthenticatorAppCode, request.SessionId,
            request.MfaDetails, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// [AllowAnonymous] RespondToTotpMfaChallengeAsync
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("respondtototpmfachallenge")]
    public async Task<IActionResult> RespondToTotpMfaChallengeAsync([FromBody] RespondToMfaRequest request,
        CancellationToken cancellationToken)
    {
        var response =
            await mfaService.RespondToTotpMfaChallengeAsync(request.MfaCode, request.MfaDetails, cancellationToken);

        if (!response.IsSuccess)
        {
            return Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors));
        }

        var sessionId = Guid.NewGuid().ToString();
        await authService.CreateSessionAndLoginAsync(response.Content, sessionId, cancellationToken);
        return Ok(response);
    }
}
