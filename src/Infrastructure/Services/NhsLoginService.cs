using System;
using System.Threading.Tasks;
using Application.Constants;
using Application.Content;
using Application.Contracts;
using Application.Participants.V1.Commands.Participants;
using Application.Responses.V1.Users;
using Application.Settings;
using Domain.Entities.Participants;
using Dte.Common.Exceptions;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Infrastructure.Clients;
using Infrastructure.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class NhsLoginService: INhsLoginService
{
    private readonly IMediator _mediator;
    private readonly IHeaderService _headerService;
    private readonly ILogger<NhsLoginService> _logger;
    private readonly EmailSettings _emailSettings;
    private readonly IEmailService _emailService;
    private readonly IParticipantService _participantService;
    private readonly NhsLoginHttpClient _nhsLoginHttpClient;

    public NhsLoginService(IMediator mediator, IHeaderService headerService,
        ILogger<NhsLoginService> logger, EmailSettings emailSettings,
        IEmailService emailService, IParticipantService participantService,
        NhsLoginHttpClient nhsLoginHttpClient)
    {
        _mediator = mediator;
        _headerService = headerService;
        _logger = logger;
        _emailSettings = emailSettings;
        _emailService = emailService;
        _participantService = participantService;
        _nhsLoginHttpClient = nhsLoginHttpClient;
    }

    public async Task<Response<NhsLoginResponse>> NhsLoginAsync(string code, string redirectUrl)
    {
        try
        {
            var tokens = await _nhsLoginHttpClient.GetTokensFromAuthorizationCode(code, redirectUrl);

            var response = new NhsLoginResponse
            {
                IdToken = tokens.IdToken,
                AccessToken = tokens.AccessToken,
            };

            var nhsUserInfo = await _nhsLoginHttpClient.GetUserInfoAsync(tokens.AccessToken);

            // check if nhsUserInfo.DateOfBirth is under 18 and return an error if so
            if (nhsUserInfo.DateOfBirth.HasValue && AgeHelper.IsUnder18(nhsUserInfo.DateOfBirth.Value))
            {
                return Response<NhsLoginResponse>.CreateErrorMessageResponse(
                    ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.UserIsUnderage,
                    "User is under 18",
                    _headerService.GetConversationId()
                );
            }

            await _participantService.NhsLoginAsync(new ParticipantDetails
            {
                ConsentRegistration = false,
                DateOfBirth = nhsUserInfo.DateOfBirth,
                Email = nhsUserInfo.Email,
                Firstname = nhsUserInfo.FirstName,
                Lastname = nhsUserInfo.LastName,
                NhsId = nhsUserInfo.NhsId,
                NhsNumber = nhsUserInfo.NhsNumber,
            });

            return Response<NhsLoginResponse>.CreateSuccessfulContentResponse(response,
                _headerService.GetConversationId());
        }
        catch (HttpServiceException ex)
        {
            if (ex.ResponseContent !=
                JsonConvert.SerializeObject(new { Message = ErrorCode.UnableToMatchAccounts }))
            {
                return HandleNhsLoginException(ex);
            }

            var errorResponse = Response<NhsLoginResponse>.CreateErrorMessageResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.UnableToMatchAccounts,
                "Unable to match account details",
                _headerService.GetConversationId());

            return errorResponse;
        }
        catch (Exception ex)
        {
            return HandleNhsLoginException(ex);
        }
    }
    
    public async Task<Response<SignUpResponse>> NhsSignUpAsync(bool consentRegistration, string token)
    {
        try
        {
            var nhsUserInfo = await _nhsLoginHttpClient.GetUserInfoAsync(token);

            await _mediator.Send(new CreateParticipantDetailsCommand("", nhsUserInfo.Email,
                nhsUserInfo.FirstName, nhsUserInfo.LastName,
                consentRegistration, nhsUserInfo.NhsId, nhsUserInfo.DateOfBirth.Value, nhsUserInfo.NhsNumber));

            var baseUrl = _emailSettings.WebAppBaseUrl;
            var htmlBody = EmailTemplate.GetHtmlTemplate().Replace("###TITLE_REPLACE1###",
                    "New Be Part of Research Account")
                .Replace("###TEXT_REPLACE1###",
                    $"Thank you for registering for Be Part of Research using your NHS login or through the NHS App. You will need to use the NHS login option on the <a href=\"{baseUrl}Participants/Options\">Be Part of Research</a> website each time you access your account.")
                .Replace("###TEXT_REPLACE2###",
                    "By signing up, you are joining our community of amazing volunteers who are helping researchers to understand more about health and care conditions. Please visit the <a href=\"https://bepartofresearch.nihr.ac.uk/taking-part/how-to-take-part\">How to take part</a> section of the website to find out about other ways to take part in health and care research.")
                .Replace("###TEXT_REPLACE3###",
                    "<a href=\"https://nihr.us14.list-manage.com/subscribe?u=299dc02111e8a68172029095f&id=3b030a1027\">Sign up to our newsletter</a> to receive all our research news, studies you can take part in and other opportunities helping to shape health and care research from across the UK.")
                .Replace("###TEXT_REPLACE4###",
                    "If you close your NHS login account, your Be Part of Research account will remain open and if you would also like to close your Be Part of Research account you will need to email <a href=\"mailto:Bepartofresearch@nihr.ac.uk\">Bepartofresearch@nihr.ac.uk</a>.")
                .Replace("###LINK_REPLACE###", "")
                .Replace("###LINK_DISPLAY_VALUE_REPLACE###", "block")
                .Replace("###TEXT_REPLACE5###",
                    "Thank you for your ongoing commitment and support.")
                .Replace("###TEXT_REPLACE6###", "");

            await _emailService.SendEmailAsync(nhsUserInfo.Email, "Be Part of Research", htmlBody);

            return Response<SignUpResponse>.CreateSuccessfulContentResponse(
                new SignUpResponse { UserConsents = true, }, _headerService.GetConversationId());
        }
        catch (Exception ex)
        {
            var exceptionResponse = Response<SignUpResponse>.CreateExceptionResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
                _headerService.GetConversationId());
            _logger.LogError(ex,
                $"Unknown error logging in with NHS login\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
            return exceptionResponse;
        }
    }
    
    private Response<NhsLoginResponse> HandleNhsLoginException(Exception ex)
    {
        var exceptionResponse = Response<NhsLoginResponse>.CreateExceptionResponse(
            ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.InternalServerError, ex,
            _headerService.GetConversationId());
        _logger.LogError(ex,
            $"Unknown error logging in with NHS login\r\n{JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented)}");
        return exceptionResponse;
    }
}
