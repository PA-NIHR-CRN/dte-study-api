using System.Globalization;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using BPOR.Domain.Entities;
using BPOR.Domain.Interfaces;
using BPOR.Domain.Settings;
using BPOR.Domain.Utils;
using BPOR.Infrastructure.Constants;
using BPOR.Infrastructure.Enum;
using BPOR.Infrastructure.Interfaces;
using BPOR.Infrastructure.Mappers;
using Dte.Common;
using Dte.Common.Contracts;
using Dte.Common.Exceptions;
using Dte.Common.Exceptions.Common;
using Dte.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Interfaces;

namespace BPOR.Infrastructure.Services;

public class ParticipantService(
    IParticipantRepository participantRepository,
    ILogger<ParticipantService> logger,
    IUserService userService,
    IAmazonCognitoIdentityProvider provider,
    IOptions<AwsSettings> awsSettings,
    IContentfulService contentfulService,
    IOptions<ContentfulSettings> contentfulSettings,
    IEmailService emailService)
    : IParticipantService
{
    public async Task CreateParticipantAsync(DynamoParticipant entity, CancellationToken cancellationToken)
    {
        entity.ConsentRegistrationAtUtc = DateTime.UtcNow;
        entity.CreatedAtUtc = DateTime.UtcNow;
        await participantRepository.CreateParticipantAsync(entity, cancellationToken);
    }

    public async Task<DynamoParticipant> GetParticipantAsync(string participantId, CancellationToken cancellationToken)
    {
        var participant = await participantRepository.GetParticipantAsync(participantId, cancellationToken);
        if (participant == null)
        {
            logger.LogWarning("Participant {ParticipantId} not found in the repository", participantId);
            return null;
        }

        return participant;
    }


    public async Task<DynamoParticipant> UpdateParticipantAsync(DynamoParticipant request,
        CancellationToken cancellationToken)
    {
        request.UpdatedAtUtc = DateTime.UtcNow;
        return await participantRepository.UpdateParticipantAsync(request, cancellationToken);
    }

    public async Task DeleteParticipantAsync(string participantId, CancellationToken cancellationToken)
    {
        var entity = await participantRepository.GetParticipantAsync(participantId, cancellationToken);
        if (entity == null) return;

        var contentfulEmailRequest = new EmailContentRequest
        {
            EmailName = contentfulSettings.Value.EmailTemplates.DeleteAccount,
            SelectedLocale = new CultureInfo(entity.SelectedLocale ?? SelectedLocale.Default)
        };

        var contentfulEmail = await contentfulService.GetEmailContentAsync(contentfulEmailRequest);

        await emailService.SendEmailAsync(entity.Email, contentfulEmail.EmailSubject, contentfulEmail.EmailBody,
            cancellationToken);

        var linkedEmail = entity.Email;
        await SaveAnonymisedDemographicParticipantDataAsync(entity, cancellationToken);
        await RemoveParticipantDataAsync(entity, cancellationToken);


        var linkedEntity = await GetParticipantDetailsByEmailAsync(linkedEmail, cancellationToken);
        if (linkedEntity == null) return;
        await RemoveParticipantDataAsync(linkedEntity, cancellationToken);
    }

    public async Task StoreMfaCodeAsync(string username, string code, CancellationToken cancellationToken)
    {
        var participant = await participantRepository.GetParticipantAsync(username, cancellationToken);
        if (participant == null)
        {
            logger.LogWarning("Participant {Username} not found in the repository", username);
            throw new NotFoundException($"No participant found for username: {username}");
        }

        participant.MfaChangePhoneCode = code;
        participant.MfaChangePhoneCodeExpiry = DateTime.UtcNow.AddMinutes(5);
        await participantRepository.UpdateParticipantAsync(participant, cancellationToken);
    }

    public async Task<MfaValidationResultEnum> ValidateMfaCodeAsync(string username, string code,
        CancellationToken cancellationToken)
    {
        var participant = await participantRepository.GetParticipantAsync(username, cancellationToken);

        if (participant == null)
        {
            return MfaValidationResultEnum.UserNotFound;
        }

        if (participant.MfaChangePhoneCodeExpiry < DateTime.UtcNow)
        {
            return MfaValidationResultEnum.CodeExpired;
        }

        if (participant.MfaChangePhoneCode != code)
        {
            return MfaValidationResultEnum.CodeInvalid;
        }

        return MfaValidationResultEnum.Success;
    }

    //TODO confirm if this is needed
    private async Task CreateUserAndDeactivateOldUserAsync(DynamoParticipant request, DynamoParticipant participant,
        CancellationToken cancellationToken)
    {
        var entity = request.MapNewUserFromRequestAndParticipant(participant);

        await participantRepository.CreateParticipantAsync(entity, cancellationToken);

        var response = await provider.AdminDisableUserAsync(new AdminDisableUserRequest
        {
            Username = participant.ParticipantId,
            UserPoolId = awsSettings.Value.CognitoPoolId
        }, cancellationToken);

        if ((int)response.HttpStatusCode < 200 || (int)response.HttpStatusCode > 299)
        {
            throw new AmazonCognitoIdentityProviderException($"Unable to disable user account: {response}");
        }
    }

    public async Task NhsLoginAsync(DynamoParticipant request, CancellationToken cancellationToken)
    {
        var participant = await participantRepository.GetParticipantAsync(request.NhsId, cancellationToken);
        if (participant != null)
        {
            participant.Email = request.Email.ToLowerInvariant();
            participant.Firstname = request.Firstname;
            participant.Lastname = request.Lastname;
            participant.DateOfBirth = request.DateOfBirth;
            participant.NhsNumber = request.NhsNumber;
            participant.UpdatedAtUtc = DateTime.UtcNow;
            participant.SelectedLocale = request.SelectedLocale;

            await participantRepository.UpdateParticipantAsync(participant, cancellationToken);
            return;
        }

        participant = await GetParticipantByNhsNumberAsync(request.NhsNumber, cancellationToken);
        // If participant is found, create new user and deactivate old user
        if (participant != null)
        {
            await CreateUserAndDeactivateOldUserAsync(request, participant, cancellationToken);
        }

        // Look up participant by email and date of birth
        var emailRequestId = await userService.AdminGetUserAsync(request.Email, cancellationToken);
        if (emailRequestId == null)
        {
            return;
        }

        participant = await participantRepository.GetParticipantAsync(emailRequestId.Username, cancellationToken);
        if (participant == null)
        {
            return;
        }

        // check if string version of date of birth matches without time
        if (participant.DateOfBirth.HasValue && request.DateOfBirth.HasValue &&
            participant.DateOfBirth.Value.Date == request.DateOfBirth.Value.Date)
        {
            await CreateUserAndDeactivateOldUserAsync(request, participant, cancellationToken);
        }
        else
        {
            // pass back error message to be displayed
            throw new ConflictException(ErrorCode.UnableToMatchAccounts);
        }
    }

    public async Task<DynamoParticipant> GetParticipantDetailsByEmailAsync(string email,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email))
        {
            return null;
        }

        email = email.ToLowerInvariant();

        var participant = await participantRepository.QueryIndexForParticipantAsync(email, "Email", cancellationToken);

        return participant;
    }

    //TODO do I need this?
    public async Task<DynamoParticipant> GetParticipantByNhsNumberAsync(string nhsNumber,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(nhsNumber))
        {
            return null;
        }

        var participant =
            await participantRepository.QueryIndexForParticipantAsync(nhsNumber, "NhsNumber", cancellationToken);

        return participant;
    }

    private async Task RemoveParticipantDataAsync(DynamoParticipant entity, CancellationToken cancellationToken)
    {
        var participantId = KeyUtils.StripPrimaryKey(entity.Pk);
        if (entity.NhsId == null)
        {
            await RemoveCognitoUserAsync(participantId, cancellationToken);
        }

        await participantRepository.DeleteParticipantAsync(entity, cancellationToken);
    }

    private async Task RemoveCognitoUserAsync(string username, CancellationToken cancellationToken)
    {
        await provider.AdminDeleteUserAsync(new AdminDeleteUserRequest
        {
            UserPoolId = awsSettings.Value.CognitoPoolId,
            Username = username
        }, cancellationToken);
    }

    private async Task SaveAnonymisedDemographicParticipantDataAsync(DynamoParticipant entity,
        CancellationToken cancellationToken)
    {
        var primaryKey = KeyUtils.DeletedKey(Guid.NewGuid());
        var anonEntity = new DynamoParticipant
        {
            Pk = primaryKey,
            Sk = KeyUtils.DeletedKey(),
            ConsentRegistration = false,
            RemovalOfConsentRegistrationAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow,
            CreatedAtUtc = entity.CreatedAtUtc,
            NhsId = entity.NhsId ?? null,
            ParticipantId = entity.ParticipantId ?? null,
            DateOfBirth = entity.DateOfBirth,
            GenderIsSameAsSexRegisteredAtBirth = entity.GenderIsSameAsSexRegisteredAtBirth,
            EthnicGroup = entity.EthnicGroup,
            Disability = entity.Disability,
            Address = entity.Address.Clear()
        };

        await participantRepository.CreateParticipantAsync(anonEntity, cancellationToken);
    }
}
