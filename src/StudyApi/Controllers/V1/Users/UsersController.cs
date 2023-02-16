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

namespace StudyApi.Controllers.V1.Users
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
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
            return Ok(await _mediator.Send(new UserLoginCommand(request.Email, request.Password)));
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
            
            if(response.Content.UserExists)
                return Ok(response);

            if (!response.IsSuccess)
            {
                return Ok(response);
            }

            var createDetailsResponse = await _mediator.Send(new CreateParticipantDetailsCommand(response.Content.UserId, request.Email, request.Firstname, request.Lastname, request.ConsentRegistration));

            return Ok(new { IsSuccess = response.IsSuccess && createDetailsResponse.IsSuccess });
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
            var response = await _mediator.Send(new ResendVerificationEmailCommand(request.Email));

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
            var response = await _mediator.Send(new ConfirmSignUpCommand(request.Code, request.Email));

            return Ok(response);
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
            var response = await _mediator.Send(new ConfirmForgotPasswordCommand(request.Code, request.Email, request.Password));

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
            return Ok(await _mediator.Send(new ChangePasswordCommand(request.AccessToken, request.OldPassword, request.NewPassword)));
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
        public async Task<IActionResult> DeleteParticipantAccount([FromBody] DeleteParticipantAccountRequest request)
        {
            return Ok(await _mediator.Send(new DeleteParticipantAccountCommand(request.AccessToken, User.GetUserIdCognito())));
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
            return Ok(await _mediator.Send(new ChangeEmailCommand(User.GetUserIdCognito(), request.AccessToken, request.NewEmail)));
        }
        
        /// <summary>
        /// [Admin] Get users who are in the whitelist
        /// </summary>
        /// <response code="200">When IsSuccess true</response>
        /// <response code="500">Server side error</response>
        [Authorize("Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<IEnumerable<AccessWhiteListResponse>>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpGet("accesswhitelist")]
        public async Task<IActionResult> GetAccessWhiteList()
        {
            return Ok(await _mediator.Send(new GetAccessWhiteListQuery()));
        }
        
        /// <summary>
        /// [Admin] Add users to the white list
        /// </summary>
        /// <response code="200">When IsSuccess true</response>
        /// <response code="500">Server side error</response>
        [Authorize("Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<object>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = null)]
        [HttpPost("accesswhitelist")]
        public async Task<IActionResult> SaveAccessWhiteList([FromBody] SaveAccessWhiteListRequest request)
        {
            return Ok(await _mediator.Send(new SaveAccessWhiteListCommand(request.Emails)));
        }
    }
}