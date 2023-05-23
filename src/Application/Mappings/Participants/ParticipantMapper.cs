using Application.Models.Participants;
using Application.Participants.V1.Commands.Participants;
using Application.Responses.V1.Participants;
using Domain.Entities.Participants;

namespace Application.Mappings.Participants
{
    public static class ParticipantMapper
    {
        public static ParticipantAddressModel MapTo(ParticipantAddress source)
        {
            if (source == null) return null;
            
            return new ParticipantAddressModel
            {
                AddressLine1 = source.AddressLine1,
                AddressLine2 = source.AddressLine2,
                AddressLine3 = source.AddressLine3,
                AddressLine4 = source.AddressLine4,
                Town = source.Town,
                Postcode = source.Postcode,
            };
        }

        public static ParticipantDetailsResponse MapTo(ParticipantDetails source)
        {
            return new ParticipantDetailsResponse
            {
                Firstname = source.Firstname,
                Lastname = source.Lastname,
                Email = source.Email,
                ConsentRegistration = source.ConsentRegistration,
                ConsentRegistrationAtUtc = source.ConsentRegistrationAtUtc,
                RemovalOfConsentRegistrationAtUtc = source.RemovalOfConsentRegistrationAtUtc,
            };
        }

        public static ParticipantDemographicsResponse MapTo(ParticipantDemographics source)
        {
            return new ParticipantDemographicsResponse
            {
                MobileNumber = source.MobileNumber,
                LandlineNumber = source.LandlineNumber,
                Address = MapTo(source.Address),
                DateOfBirth = source.DateOfBirth,
                SexRegisteredAtBirth = source.SexRegisteredAtBirth,
                GenderIsSameAsSexRegisteredAtBirth = source.GenderIsSameAsSexRegisteredAtBirth,
                EthnicGroup = source.EthnicGroup,
                EthnicBackground = source.EthnicBackground,
                Disability = source.Disability,
                DisabilityDescription = source.DisabilityDescription,
                HealthConditionInterests = source.HealthConditionInterests,
                HasDemographics = source.HasDemographics
            };
        }
    }
}