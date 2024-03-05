using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Application.Constants;
using Application.Contracts;
using Application.Mappings.Participants;
using Application.Models.MFA;
using Application.Responses.V1.Participants;
using Application.Settings;
using Domain.Entities.Participants;
using Dte.Common;
using Dte.Common.Contracts;
using Dte.Common.Exceptions;
using Dte.Common.Exceptions.Common;
using Dte.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class ParticipantService : IParticipantService
{
    private readonly IAmazonCognitoIdentityProvider _provider;
    private readonly AwsSettings _awsSettings;
    private readonly IParticipantRepository _participantRepository;
    private readonly IClock _clock;
    private readonly ILogger<UserService> _logger;
    private readonly IEmailService _emailService;
    private readonly IContentfulService _contentfulService;
    private readonly ContentfulSettings _contentfulSettings;
    private readonly EmailSettings _emailSettings;

    public ParticipantService(IParticipantRepository participantRepository, IClock clock,
        IAmazonCognitoIdentityProvider provider, AwsSettings awsSettings,
        ILogger<UserService> logger, EmailSettings emailSettings,
        IEmailService emailService, IContentfulService contentfulService, ContentfulSettings contentfulSettings)
    {
        _participantRepository = participantRepository;
        _clock = clock;
        _provider = provider;
        _awsSettings = awsSettings;
        _logger = logger;
        _emailService = emailService;
        _contentfulService = contentfulService;
        _contentfulSettings = contentfulSettings;
        _emailSettings = emailSettings;
    }

    private static string DeletedKey(Guid primaryKey) => $"DELETED#{primaryKey}";
    private static string DeletedKey() => "DELETED#";
    private static string StripPrimaryKey(string pk) => pk.Replace("PARTICIPANT#", "");

    private static bool IsSuccessHttpStatusCode(int httpStatusCode) =>
        httpStatusCode >= StatusCodes.Status200OK &&
        httpStatusCode <
        StatusCodes.Status300MultipleChoices;

    private async Task<string> AdminGetUserAsync(string email)
    {
        try
        {
            var response = await _provider.AdminGetUserAsync(new AdminGetUserRequest
            {
                UserPoolId = _awsSettings.CognitoPoolId,
                Username = email
            });

            return IsSuccessHttpStatusCode((int)response.HttpStatusCode)
                ? response.Username
                : null;
        }
        catch (UserNotFoundException)
        {
            return null;
        }
    }

    private async Task CreateUserAndDeactivateOldUserAsync(ParticipantDetails request,
        ParticipantDetails participant)
    {
        var entity = new ParticipantDetails
        {
            NhsId = request.NhsId,
            NhsNumber = request.NhsNumber,
            Email = request.Email,
            ParticipantId = request.NhsId,
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            ConsentRegistration = participant.ConsentRegistration,
            DateOfBirth = request.DateOfBirth,
            ConsentRegistrationAtUtc = participant.ConsentRegistration ? _clock.Now() : (DateTime?)null,
            RemovalOfConsentRegistrationAtUtc = (DateTime?)null,
            CreatedAtUtc = _clock.Now(),
            SelectedLocale = request.SelectedLocale
        };
        // check if demographic data is complete
        var demographics = await _participantRepository
            .GetParticipantDemographicsAsync(participant.Pk.Replace("PARTICIPANT#", ""));
        if (demographics.HasDemographics)
        {
            await _participantRepository.CreateParticipantDetailsAsync(entity);
            await _participantRepository.AddDemographicsToNhsUserAsync(demographics, entity.NhsId);
        }
        else if (participant.ConsentRegistration)
        {
            // create new user with consent and deactivate the old user
            await _participantRepository.CreateParticipantDetailsAsync(entity);
        }

        var response = await _provider.AdminDisableUserAsync(new AdminDisableUserRequest
            {
                Username = participant.ParticipantId,
                UserPoolId = _awsSettings.CognitoPoolId
            }
        );

        if ((int)response.HttpStatusCode < 200 || (int)response.HttpStatusCode > 299)
        {
            throw new AmazonCognitoIdentityProviderException($"Unable to disable user account: {response}");
        }
    }

    public async Task NhsLoginAsync(ParticipantDetails request)
    {
        // check and see if the user matches an existing user in the database by NHS ID
        var participant = await _participantRepository.GetParticipantDetailsAsync(request.NhsId);
        if (participant != null)
        {
            participant.Email = request.Email.ToLower();
            participant.Firstname = request.Firstname;
            participant.Lastname = request.Lastname;
            participant.DateOfBirth = request.DateOfBirth;
            participant.NhsNumber = request.NhsNumber;
            participant.UpdatedAtUtc = _clock.Now();
            participant.SelectedLocale = request.SelectedLocale;

            await _participantRepository.UpdateParticipantDetailsAsync(participant);
            return;
        }

        participant = await GetParticipantDetailsByNhsNumberAsync(request.NhsNumber);
        // If participant is found, create new user and deactivate old user
        if (participant != null)
        {
            await CreateUserAndDeactivateOldUserAsync(request, participant);
        }

        // Look up participant by email and date of birth
        var emailRequestId = await AdminGetUserAsync(request.Email);
        if (emailRequestId == null) return;
        participant = await _participantRepository.GetParticipantDetailsAsync(emailRequestId);
        if (participant == null) return;
        // check if string version of date of birth matches without time
        if (participant.DateOfBirth.HasValue && request.DateOfBirth.HasValue &&
            participant.DateOfBirth.Value.Date ==
            request.DateOfBirth.Value.Date)
        {
            await CreateUserAndDeactivateOldUserAsync(request, participant);
        }
        else
        {
            // pass back error message to be displayed
            throw new ConflictException(ErrorCode.UnableToMatchAccounts);
        }
    }

    public async Task UpdateParticipantEmailAsync(string participantId, string newEmail)
    {
        var participant = await _participantRepository.GetParticipantDetailsAsync(participantId);
        if (participant == null)
            throw new NotFoundException($"No participant found for participantId: {participantId}");

        participant.Email = newEmail.ToLower();
        participant.UpdatedAtUtc = _clock.Now();
        await _participantRepository.UpdateParticipantDetailsAsync(participant);
    }

    public async Task DeleteUserAsync(string participantId)
    {
        try
        {
            var entity = await _participantRepository.GetParticipantDetailsAsync(participantId);
            if (entity == null) return;
            
            var contentfulEmailRequest = new EmailContentRequest
            {
                EmailName = _contentfulSettings.EmailTemplates.DeleteAccount,
                SelectedLocale = new CultureInfo(entity.SelectedLocale ?? SelectedLocale.Default)
            };
                
            var contentfulEmail = await _contentfulService.GetEmailContentAsync(contentfulEmailRequest);

            await _emailService.SendEmailAsync(entity.Email, contentfulEmail.EmailSubject, contentfulEmail.EmailBody);

            var linkedEmail = entity.Email;
            await SaveAnonymisedDemographicParticipantDataAsync(entity);
            await RemoveParticipantDataAsync(entity);


            var linkedEntity = await GetParticipantDetailsByEmailAsync(linkedEmail);
            if (linkedEntity == null) return;
            await RemoveParticipantDataAsync(linkedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete-error = {EMessage}", ex.Message);
        }
    }

    public async Task StoreMfaCodeAsync(string username, string code)
    {
        var particpiant = await _participantRepository.GetParticipantDetailsAsync(username);
        if (particpiant == null)
            throw new NotFoundException($"No participant found for username: {username}");

        particpiant.MfaChangePhoneCode = code;
        particpiant.MfaChangePhoneCodeExpiry = _clock.Now().AddMinutes(5);
        await _participantRepository.UpdateParticipantDetailsAsync(particpiant);
    }

    public async Task<MfaValidationResult> ValidateMfaCodeAsync(string username, string code)
    {
        var participant = await _participantRepository.GetParticipantDetailsAsync(username);

        if (participant == null)
            return MfaValidationResult.UserNotFound;

        if (participant.MfaChangePhoneCodeExpiry < _clock.Now())
            return MfaValidationResult.CodeExpired;

        if (participant.MfaChangePhoneCode != code)
            return MfaValidationResult.CodeInvalid;

        return MfaValidationResult.Success;
    }


    private async Task RemoveParticipantDataAsync(ParticipantDetails entity)
    {
        var participantId = StripPrimaryKey(entity.Pk);
        if (entity.NhsId == null)
        {
            await RemoveCognitoUserAsync(participantId);
        }

        await _participantRepository.DeleteParticipantDetailsAsync(entity);
    }

    private async Task RemoveCognitoUserAsync(string username)
    {
        await _provider.AdminDeleteUserAsync(new AdminDeleteUserRequest
        {
            UserPoolId = _awsSettings.CognitoPoolId,
            Username = username
        });
    }

    private async Task SaveAnonymisedDemographicParticipantDataAsync(ParticipantDetails entity)
    {
        var primaryKey = DeletedKey(Guid.NewGuid());
        var anonEntity = new ParticipantDetails
        {
            Pk = primaryKey,
            Sk = DeletedKey(),
            ConsentRegistration = false,
            RemovalOfConsentRegistrationAtUtc = _clock.Now(),
            UpdatedAtUtc = _clock.Now(),
            CreatedAtUtc = entity.CreatedAtUtc,
            NhsId = entity.NhsId ?? null,
            ParticipantId = entity.ParticipantId ?? null,
            DateOfBirth = entity.DateOfBirth,
            GenderIsSameAsSexRegisteredAtBirth = entity.GenderIsSameAsSexRegisteredAtBirth,
            EthnicGroup = entity.EthnicGroup,
            Disability = entity.Disability,
            Address = entity.Address?.Clear()
        };

        await _participantRepository.CreateAnonymisedDemographicParticipantDataAsync(anonEntity);
        
    }

    public async Task<ParticipantDetails> GetParticipantDetailsAsync(string participantId)
    {
        var participantDetails = await _participantRepository.GetParticipantDetailsAsync(participantId);

        if (participantDetails == null)
        {
            throw new NotFoundException($"No participant details found for participantId: {participantId}");
        }

        return participantDetails;
    }

    public async Task<ParticipantDetails> GetParticipantDetailsByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email)) return null;

        email = email.ToLowerInvariant();

        var participantDetails =
            await _participantRepository.QueryIndexForParticipantDetailsAsync(email, "Email");

        _logger.LogInformation("participantDetails: {ParticipantDetails}",
            JsonConvert.SerializeObject(participantDetails));

        return participantDetails;
    }

    public async Task<ParticipantDetails> GetParticipantDetailsByNhsNumberAsync(string nhsNumber)
    {
        if (string.IsNullOrEmpty(nhsNumber)) return null;

        var participantDetails =
            await _participantRepository.QueryIndexForParticipantDetailsAsync(nhsNumber, "NhsNumber");

        _logger.LogInformation("participantDetails: {ParticipantDetails}",
            JsonConvert.SerializeObject(participantDetails));

        return participantDetails;
    }

    public async Task<ParticipantDemographicsResponse> GetParticipantDemographicsAsync(string participantId)
    {
        var participantDemographics = await _participantRepository.GetParticipantDemographicsAsync(participantId);

        if (participantDemographics == null)
        {
            throw new NotFoundException($"No participant demographics found for participantId: {participantId}");
        }

        return ParticipantMapper.MapTo(participantDemographics);
    }

    public async Task CreateParticipantDetailsAsync(ParticipantDetails request)
    {
        var entity = new ParticipantDetails
        {
            NhsId = request.NhsId,
            NhsNumber = request.NhsNumber,
            ParticipantId = request.ParticipantId,
            Email = request.Email.ToLower(),
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            ConsentRegistration = request.ConsentRegistration,
            DateOfBirth = request.DateOfBirth,
            ConsentRegistrationAtUtc = request.ConsentRegistration ? _clock.Now() : (DateTime?)null,
            RemovalOfConsentRegistrationAtUtc = (DateTime?)null,
            CreatedAtUtc = _clock.Now(),
        };

        await _participantRepository.CreateParticipantDetailsAsync(entity);
    }

    public async Task UpdateParticipantDetailsAsync(ParticipantDetails request)
    {
        var entity = await _participantRepository.GetParticipantDetailsAsync(request.ParticipantId);

        if (entity == null)
        {
            throw new NotFoundException($"Participant not found, Id: {request.ParticipantId}");
        }

        entity.Firstname = request.Firstname;
        entity.Lastname = request.Lastname;
        entity.UpdatedAtUtc = _clock.Now();

        await _participantRepository.UpdateParticipantDetailsAsync(entity);
    }

    public async Task CreateParticipantDemographicsAsync(ParticipantDemographics request)
    {
        var updateExisting = false;

        var entity = await _participantRepository.GetParticipantDemographicsAsync(request.ParticipantId);

        if (entity == null)
        {
            entity = new ParticipantDemographics
            {
                ParticipantId = request.ParticipantId,
            };
            updateExisting = false;
        }
        else
        {
            updateExisting = true;
        }

        entity.MobileNumber = request.MobileNumber;
        entity.LandlineNumber = request.LandlineNumber;
        entity.SexRegisteredAtBirth = request.SexRegisteredAtBirth;
        entity.GenderIsSameAsSexRegisteredAtBirth = request.GenderIsSameAsSexRegisteredAtBirth;
        entity.EthnicGroup = request.EthnicGroup;
        entity.EthnicBackground = request.EthnicBackground;
        entity.Disability = request.Disability;
        entity.DisabilityDescription = request.DisabilityDescription;
        entity.HealthConditionInterests = request.HealthConditionInterests?.ToList();

        if (request.Address != null)
        {
            entity.Address = ParticipantAddressMapper.MapTo(request.Address);
        }

        if (updateExisting)
        {
            await _participantRepository.UpdateParticipantDemographicsAsync(entity);
        }
        else
        {
            await _participantRepository.CreateParticipantDemographicsAsync(entity);
        }
    }

    public async Task UpdateParticipantDemographicsAsync(ParticipantDemographics request)
    {
        var entity = await _participantRepository.GetParticipantDemographicsAsync(request.ParticipantId);

        entity.MobileNumber = request.MobileNumber;
        entity.LandlineNumber = request.LandlineNumber;
        entity.SexRegisteredAtBirth = request.SexRegisteredAtBirth;
        entity.GenderIsSameAsSexRegisteredAtBirth = request.GenderIsSameAsSexRegisteredAtBirth;
        entity.EthnicGroup = request.EthnicGroup;
        entity.EthnicBackground = request.EthnicBackground;
        entity.Disability = request.Disability;
        entity.DisabilityDescription = request.DisabilityDescription;
        entity.HealthConditionInterests = request.HealthConditionInterests?.ToList();
        entity.DateOfBirth = request.DateOfBirth;
        entity.UpdatedAtUtc = _clock.Now();

        if (request.Address != null)
        {
            entity.Address = ParticipantAddressMapper.MapTo(request.Address);
        }

        await _participantRepository.UpdateParticipantDemographicsAsync(entity);
    }
}
