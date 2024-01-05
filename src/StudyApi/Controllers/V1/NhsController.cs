using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts;
using Application.Responses.V1.Users;
using Application.Users.V1.Commands;
using Dte.Common.Exceptions.Common;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Requests.Nhs;
using StudyApi.Requests.Users;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApi.Controllers.V1;

[AllowAnonymous]
[ApiController]
[ApiVersion("1")]
[Route("api/nhs")]
public class NhsController: Controller
{
    private readonly IMediator _mediator;
    private readonly IAuthenticationService _authenticationService;
    private readonly IDataProtector _dataProtector;

    public NhsController(IDataProtector dataProtector, IMediator mediator, IAuthenticationService authenticationService)
    {
        _mediator = mediator;
        _authenticationService = authenticationService;
        _dataProtector = dataProtector.CreateProtector("nhs.login.cookies");
    }
    
    /// <summary>
    /// [AllowAnonymous] NhsLogin
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("nhslogin")]
    public async Task<IActionResult> NhsLogin([FromBody] NhsLoginRequest request)
    {
        var response = await _mediator.Send(new NhsLoginCommand(request.Code, request.RedirectUrl));

        if (!response.IsSuccess)
        {
            return Ok(Response<string>.CreateErrorMessageResponse(response.Errors));
        }

        var sessionId = Guid.NewGuid().ToString();
        await _authenticationService.CreateSessionAndLoginAsync(response.Content.IdToken, sessionId);
        var cookieValue = _dataProtector.Protect(response.Content.AccessToken);
        HttpContext.Response.Cookies.Append(CookieNames.NhsAccessToken, cookieValue,
            new CookieOptions { HttpOnly = true, IsEssential = true, Secure = true });
        return Ok(Response<string>.CreateSuccessfulContentResponse(response.Content.IdToken));
    }
    
    /// <summary>
    /// [AllowAnonymous] SignUp NHS user
    /// </summary>
    /// <response code="200">When IsSuccess true</response>
    /// <response code="500">Server side error</response>
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<SignUpResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
    [HttpPost("nhssignup")]
    public async Task<IActionResult> NhsSignUpUserAsync([FromBody] NhsSignUpRequest request)
    {
        if (HttpContext.Request.Cookies.TryGetValue(CookieNames.NhsAccessToken, out var cookieValue))
        {
            var accessToken = _dataProtector.Unprotect(cookieValue);
            var response = await _mediator.Send(new NhsSignUpCommand(request.ConsentRegistration, accessToken));

            if (response.IsSuccess)
            {
                HttpContext.Response.Cookies.Delete(CookieNames.NhsAccessToken);
            }

            return Ok(response);
        }
        else
        {
            var errors = new List<Error>
            {
                new Error { Detail = "Access Token not found" }
            };
            return BadRequest(Response<SignUpResponse>.CreateErrorMessageResponse(errors));
        }
    }
}
