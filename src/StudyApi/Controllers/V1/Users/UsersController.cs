using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Participants.V1.Commands.Participants;
using Application.Responses.V1.Users;
using Application.Users.V1.Commands;
using Application.Users.V1.Queries;
using Dte.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudyApi.Requests.Users;
using Swashbuckle.AspNetCore.Annotations;
using Application.Extensions;
using ConfirmForgotPasswordRequest = StudyApi.Requests.Users.ConfirmForgotPasswordRequest;
using ConfirmSignUpRequest = StudyApi.Requests.Users.ConfirmSignUpRequest;
using ForgotPasswordRequest = StudyApi.Requests.Users.ForgotPasswordRequest;
using SignUpRequest = StudyApi.Requests.Users.SignUpRequest;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Application.Contracts;
using Application.Models.MFA;
using Dte.Common.Exceptions.Common;
using Microsoft.AspNetCore.DataProtection;
using IAuthenticationService = Application.Contracts.IAuthenticationService;

namespace StudyApi.Controllers.V1.Users
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private const string NhsAccessTokenCookieName = ".BPOR.NHS.Access";
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;
        private readonly IDataProtector _dataProtector;
        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public UsersController(IMediator mediator, ILogger<UsersController> logger,
            IDataProtectionProvider dataProtector, ISessionService sessionService, IUserService userService,
            IAuthenticationService authenticationService)
        {
            _mediator = mediator;
            _logger = logger;
            _dataProtector = dataProtector.CreateProtector("nhs.login.cookies");
            _sessionService = sessionService;
            _userService = userService;
            _authenticationService = authenticationService;
        }

        private async Task CreateSessionAndLogin(string jwtToken, string sessionId)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var claims = new List<Claim>
            {
                new Claim("sessionId", sessionId)
            };
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            var participantId = token.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            await _sessionService.SetSessionAsync(participantId, sessionId);
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
            await CreateSessionAndLogin(response.Content, sessionId);
            return Ok(response);
        }

        /// <summary>
        /// [AllowAnonymous] RespondToMfaChallenge
        /// </summary>
        /// <response code="200">When IsSuccess true</response>
        /// <response code="500">Server side error</response>
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPost("respondtomfachallenge")]
        public async Task<IActionResult> RespondToMfaChallengeAsync([FromBody] RespondToMfaRequest request)
        {
            var response =
                await _authenticationService.RespondToMfaChallengeAsync(request.MfaCode, request.MfaDetails);

            if (!response.IsSuccess)
            {
                return Ok(Response<UserLoginResponse>.CreateErrorMessageResponse(response.Errors));
            }

            var sessionId = Guid.NewGuid().ToString();
            await CreateSessionAndLogin(response.Content, sessionId);
            return Ok(response);
        }

        /// <summary>
        /// [AllowAnonymous] RespondToMfaChallenge
        /// </summary>
        /// <response code="200">When IsSuccess true</response>
        /// <response code="500">Server side error</response>
        [AllowAnonymous]
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
            await CreateSessionAndLogin(response.Content, sessionId);
            return Ok(response);
        }

        /// <summary>
        /// [AllowAnonymous] Login
        /// </summary>
        /// <response code="200">When IsSuccess true</response>
        /// <response code="500">Server side error</response>
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserLoginResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPost("verifytokenmfa")]
        public async Task<IActionResult> VerifySoftwareTokenAsync([FromBody] VerifyMfaRequest request)
        {
            var response = await _userService.VerifySoftwareTokenAsync(request.AuthenticatorAppCode, request.SessionId,
                request.MfaDetails);

            return Ok(response);
        }


        /// <summary>
        /// [AllowAnonymous] NhsLogin
        /// </summary>
        /// <response code="200">When IsSuccess true</response>
        /// <response code="500">Server side error</response>
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<string>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPost("nhslogin")]
        public async Task<IActionResult> NhsLogin([FromBody] NhsLoginRequest request)
        {
            var response =
                await _mediator.Send(new NhsLoginCommand(request.Code, request.RedirectUrl, request.SelectedLocale));

            if (!response.IsSuccess)
            {
                return Ok(Response<string>.CreateErrorMessageResponse(response.Errors));
            }

            var sessionId = Guid.NewGuid().ToString();
            await CreateSessionAndLogin(response.Content.IdToken, sessionId);
            var cookieValue = _dataProtector.Protect(response.Content.AccessToken);
            HttpContext.Response.Cookies.Append(NhsAccessTokenCookieName, cookieValue,
                new CookieOptions { HttpOnly = true, IsEssential = true, Secure = true });
            return Ok(Response<string>.CreateSuccessfulContentResponse(response.Content.IdToken));
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
            var response =
                await _mediator.Send(new SignUpCommand(request.Email, request.Password, request.SelectedLocale));

            if (response.IsSuccess)
            {
                await _mediator.Send(new CreateParticipantDetailsCommand(
                    response.Content.UserId, request.Email, request.Firstname, request.Lastname,
                    request.ConsentRegistration, null, request.DateOfBirth, "", request.SelectedLocale));
            }

            return Ok(response);
        }

        public class NhsSignUpRequestLocal
        {
            public bool ConsentRegistration { get; set; }
            public string SelectedLocale { get; set; }
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
        public async Task<IActionResult> NhsSignUpUserAsync([FromBody] NhsSignUpRequestLocal request)
        {
            if (HttpContext.Request.Cookies.TryGetValue(NhsAccessTokenCookieName, out var cookieValue))
            {
                var accessToken = _dataProtector.Unprotect(cookieValue);
                var response = await _mediator.Send(new NhsSignUpCommand(request.ConsentRegistration,
                    request.SelectedLocale, accessToken));

                if (response.IsSuccess)
                {
                    HttpContext.Response.Cookies.Delete(NhsAccessTokenCookieName);
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
            var response = await _mediator.Send(new ResendVerificationEmailCommand(request.UserId));

            return Ok(response);
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
            var response = await _mediator.Send(new ConfirmSignUpCommand(request.Code, request.UserId));

            return Ok(new { IsSuccess = response.IsSuccess });
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
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request)
        {
            var response = await _mediator.Send(new ForgotPasswordCommand(request.Email));

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
        public async Task<IActionResult> ConfirmForgotPasswordAsync([FromBody] ConfirmForgotPasswordRequest request)
        {
            var response =
                await _mediator.Send(new ConfirmForgotPasswordCommand(request.Code, request.UserId, request.Password));

            return Ok(response);
        }

        /// <summary>
        /// [AllowAnonymous] Get the current Cognito password policy
        /// </summary>
        /// <response code="200">When IsSuccess true</response>
        /// <response code="500">Server side error</response>
        [AllowAnonymous]
        [HttpGet("passwordpolicy")]
        public async Task<IActionResult> GetPasswordPolicy()
        {
            return Ok(await _mediator.Send(new GetPasswordPolicyQuery()));
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
        public async Task<IActionResult> LogOut()
        {
            await _sessionService.DeleteSessionAsync(User.GetParticipantId());
            HttpContext.Response.Cookies.Delete(NhsAccessTokenCookieName);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
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
            var email = User.GetUserEmail();
            return Ok(await _mediator.Send(new ChangePasswordCommand(email, request.OldPassword,
                request.NewPassword)));
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
            return Ok(await _mediator.Send(
                new DeleteParticipantAccountCommand(User.GetUserEmail(), User.GetParticipantId())));
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
            return Ok(await _mediator.Send(new ChangeEmailCommand(User.GetParticipantId(), currentEmail,
                request.NewEmail)));
        }
    }
}
