using BPOR.Domain.Entities;
using BPOR.Domain.Utils;
using BPOR.Infrastructure.Clients;
using BPOR.Infrastructure.Constants;
using BPOR.Infrastructure.Interfaces;
using BPOR.Infrastructure.Responses.V1.Nhs;
using BPOR.Infrastructure.Responses.V1.Users;
using Dte.Common.Exceptions;
using Dte.Common.Exceptions.Common;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Dte.Common.Responses;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BPOR.Infrastructure.Services;

public class NhsService(
    NhsLoginHttpClient nhsLoginHttpClient,
    IParticipantService participantService,
    IHeaderService headerService,
    ILogger<NhsService> logger)
    : INhsService
{
    public async Task<Response<NhsLoginResponse>> NhsLoginAsync(string code, string redirectUrl, string selectedLocale,
        CancellationToken cancellationToken)
    {
        try
        {
            var tokens = await nhsLoginHttpClient.GetTokensFromAuthorizationCode(code, redirectUrl, cancellationToken);

            var response = new NhsLoginResponse
            {
                IdToken = tokens.IdToken,
                AccessToken = tokens.AccessToken,
            };

            var nhsUserInfo = await nhsLoginHttpClient.GetUserInfoAsync(tokens.AccessToken, cancellationToken);

            if (nhsUserInfo.DateOfBirth.HasValue && AgeUtils.IsUnder18(nhsUserInfo.DateOfBirth.Value))
            {
                return Response<NhsLoginResponse>.CreateErrorMessageResponse(ProjectAssemblyNames.ApiAssemblyName,
                    nameof(UserService), ErrorCode.UserIsUnderage, "User is under 18");
            }

            await participantService.NhsLoginAsync(new DynamoParticipant
            {
                ConsentRegistration = false,
                DateOfBirth = nhsUserInfo.DateOfBirth,
                Email = nhsUserInfo.Email,
                Firstname = nhsUserInfo.FirstName,
                Lastname = nhsUserInfo.LastName,
                NhsId = nhsUserInfo.NhsId,
                NhsNumber = nhsUserInfo.NhsNumber,
                SelectedLocale = selectedLocale
            }, cancellationToken);

            return Response<NhsLoginResponse>.CreateSuccessfulContentResponse(response,
                headerService.GetConversationId());
        }
        catch (ConflictException)
        {
            var errorResponse = Response<NhsLoginResponse>.CreateErrorMessageResponse(
                ProjectAssemblyNames.ApiAssemblyName, nameof(UserService), ErrorCode.UnableToMatchAccounts,
                "Unable to match accounts");

            return errorResponse;
        }
    }

    public async Task<Response<SignUpResponse>> NhsSignUpAsync(bool consent, string selectedLocale, string token,
        CancellationToken cancellationToken)
    {
        var nhsUserInfo = await nhsLoginHttpClient.GetUserInfoAsync(token, cancellationToken);

        await participantService.CreateParticipantAsync(new DynamoParticipant
        {
            ConsentRegistration = consent,
            DateOfBirth = nhsUserInfo.DateOfBirth.Value,
            Email = nhsUserInfo.Email,
            Firstname = nhsUserInfo.FirstName,
            Lastname = nhsUserInfo.LastName,
            NhsId = nhsUserInfo.NhsId,
            NhsNumber = nhsUserInfo.NhsNumber,
            SelectedLocale = selectedLocale
        }, cancellationToken);

        return Response<SignUpResponse>.CreateSuccessfulContentResponse(
            new SignUpResponse { UserConsents = true, });
    }
}
