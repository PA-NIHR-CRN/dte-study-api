using BPOR.Domain.Entities;

namespace BPOR.Infrastructure.Mappers;

public static class NhsParticipantMapper
{
    public static DynamoParticipant MapNewUserFromRequestAndParticipant(this DynamoParticipant request, DynamoParticipant participant)
    {
        return new DynamoParticipant
        {
            NhsId = request.NhsId,
            NhsNumber = request.NhsNumber,
            Email = request.Email,
            ParticipantId = request.NhsId,
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            ConsentRegistration = participant.ConsentRegistration,
            DateOfBirth = request.DateOfBirth,
            ConsentRegistrationAtUtc = participant.ConsentRegistration ? DateTime.UtcNow : (DateTime?)null,
            RemovalOfConsentRegistrationAtUtc = (DateTime?)null,
            CreatedAtUtc = DateTime.UtcNow,
            SelectedLocale = request.SelectedLocale,
            GenderIsSameAsSexRegisteredAtBirth = participant.GenderIsSameAsSexRegisteredAtBirth,
            HealthConditionInterests = participant.HealthConditionInterests,
            EthnicGroup = participant.EthnicGroup,
            Disability = participant.Disability,
            Address = participant.Address,
            SexRegisteredAtBirth = participant.SexRegisteredAtBirth,
            DisabilityDescription = participant.DisabilityDescription,
            EthnicBackground = participant.EthnicBackground,
            Stage2CompleteUtc = participant.Stage2CompleteUtc,
            MobileNumber = participant.MobileNumber,
            LandlineNumber = participant.LandlineNumber
        };
    }
}
