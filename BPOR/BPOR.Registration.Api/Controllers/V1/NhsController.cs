using Amazon.CognitoIdentityProvider.Model;
using BPOR.Infrastructure.Interfaces;
using BPOR.Registration.Api.Constants;
using BPOR.Registration.Api.Requests.Nhs;
using Dte.Common.Exceptions.Common;
using Dte.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BPOR.Registration.Api.Controllers.V1;

public class NhsController(IDataProtector dataProtector, INhsService nhsService, IAuthService authService)
    : Controller
{
    /// <summary>
    /// [AllowAnonymous] NhsLogin
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("nhslogin")]
    public async Task<IActionResult> NhsLogin([FromBody] NhsLoginRequest request, CancellationToken cancellationToken)
    {
        var response = await nhsService.NhsLoginAsync(request.Code, request.RedirectUrl, request.SelectedLocale,
            cancellationToken);

        if (!response.IsSuccess)
        {
            return Ok(Response<string>.CreateErrorMessageResponse(response.Errors));
        }

        var sessionId = Guid.NewGuid().ToString();
        await authService.CreateSessionAndLoginAsync(response.Content.IdToken, sessionId, cancellationToken);
        var cookieValue = dataProtector.Protect(response.Content.AccessToken);
        HttpContext.Response.Cookies.Append(Cookies.NhsAccessToken, cookieValue,
            new CookieOptions { HttpOnly = true, IsEssential = true, Secure = true });
        return Ok(Response<string>.CreateSuccessfulContentResponse(response.Content.IdToken));
    }

    /// <summary>
    /// [AllowAnonymous] SignUp NHS user
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<SignUpResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("nhssignup")]
    public async Task<IActionResult> NhsSignUpUserAsync([FromBody] NhsSignUpRequest request,
        CancellationToken cancellationToken)
    {
        if (HttpContext.Request.Cookies.TryGetValue(Cookies.NhsAccessToken, out var cookieValue))
        {
            var accessToken = dataProtector.Unprotect(cookieValue);
            var response = await nhsService.NhsSignUpAsync(request.ConsentRegistration, request.SelectedLocale,
                accessToken, cancellationToken);

            if (response.IsSuccess)
            {
                HttpContext.Response.Cookies.Delete(Cookies.NhsAccessToken);
            }

            return Ok(response);
        }
        else
        {
            var errors = new List<Error>
            {
                new() { HttpStatusCode = StatusCodes.Status400BadRequest }
            };

            return Ok(Response<SignUpResponse>.CreateErrorMessageResponse(errors));
        }
    }
}
