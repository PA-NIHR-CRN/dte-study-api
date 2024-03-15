using BPOR.Domain.Entities;
using BPOR.Registration.Api.Requests.Participants;

namespace BPOR.Registration.Api.Mappers;

public static class UpdateParticipantMapper
{
    public static DynamoParticipant MapTo(this UpdateParticipantRequest source, DynamoParticipant destination)
    {
        destination.Email = source.Email.ToLowerInvariant();
        destination.Firstname = source.Firstname;
        destination.Lastname = source.Lastname;
        destination.MobileNumber = source.MobileNumber;
        destination.LandlineNumber = source.LandlineNumber;
        destination.DateOfBirth = source.DateOfBirth;
        destination.EthnicGroup = source.EthnicGroup;
        destination.EthnicBackground = source.EthnicBackground;
        destination.GenderIsSameAsSexRegisteredAtBirth = source.GenderIsSameAsSexRegisteredAtBirth;
        destination.Disability = source.Disability;
        destination.DisabilityDescription = source.DisabilityDescription;
        destination.HealthConditionInterests = source.HealthConditionInterests;
        destination.UpdatedAtUtc = DateTime.Now;
        Registration.Api.Mappers.ParticipantAddressMapper.Map(source.Address, destination.Address);
        destination.SelectedLocale = source.SelectedLocale;
        destination.SexRegisteredAtBirth = source.SexRegisteredAtBirth;

        return destination;
    }
}
