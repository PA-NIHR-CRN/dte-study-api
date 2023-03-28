using Application.Models.Participants;
using Application.Participants.V1.Commands.ParticipantRegistrations;
using Application.Participants.V1.Commands.Participants;
using Application.Responses.V1.Participants;
using Domain.Entities.Participants;
using Dte.Participant.Api.Client.Requests;
using Dte.Participant.Api.Client.Requests.Participants;

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

        public static ParticipantDetailsResponse MapTo(Dte.Participant.Api.Client.Responses.Participants.ParticipantDetailsResponse source)
        {
            return new ParticipantDetailsResponse
            {
                Firstname = source.Firstname,
                Lastname = source.Lastname,
                Email = source.Email,
                ConsentRegistration = source.ConsentRegistration,
                ConsentRegistrationAtUtc = source.ConsentRegistrationAtUtc,
                RemovalOfConsentRegistrationAtUtc = source.RemovalOfConsentRegistrationAtUtc,
                HasDemographics = source.HasDemographics
            };
        }

        public static CreateParticipantDetailsRequest MapTo(CreateParticipantDetailsCommand source)
        {
            return new CreateParticipantDetailsRequest
            {
                ParticipantId = source.ParticipantId,
                Email = source.Email,
                Firstname = source.Firstname,
                Lastname = source.Lastname,
                ConsentRegistration = source.ConsentRegistration
            };
        }
        
        public static UpdateParticipantDetailsRequest MapTo(UpdateParticipantDetailsCommand source)
        {
            return new UpdateParticipantDetailsRequest
            {
                Firstname = source.Firstname,
                Lastname = source.Lastname,
                ConsentRegistration = source.ConsentRegistration
            };
        }
        
        public static ParticipantDemographicsResponse MapTo(Dte.Participant.Api.Client.Responses.Participants.ParticipantDemographicsResponse source)
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
        
        public static ParticipantAddressResponse MapTo(Dte.Participant.Api.Client.Responses.Participants.ParticipantAddressResponse source)
        {
            return new ParticipantAddressResponse
            {
                AddressLine1 = source.AddressLine1,
                AddressLine2 = source.AddressLine2,
                AddressLine3 = source.AddressLine3,
                AddressLine4 = source.AddressLine4,
                Town = source.Town,
                Postcode = source.Postcode,
            };
        }

        public static CreateParticipantDemographicsRequest MapTo(CreateParticipantDemographicsCommand source)
        {
            return new CreateParticipantDemographicsRequest
            {
                ParticipantId = source.ParticipantId,
                MobileNumber = source.MobileNumber,
                LandlineNumber = source.LandlineNumber,
                Address = MapTo(source.Address),
                SexRegisteredAtBirth = source.SexRegisteredAtBirth,
                GenderIsSameAsSexRegisteredAtBirth = source.GenderIsSameAsSexRegisteredAtBirth,
                EthnicGroup = source.EthnicGroup,
                EthnicBackground = source.EthnicBackground,
                Disability = source.Disability,
                DisabilityDescription = source.DisabilityDescription,
                HealthConditionInterests = source.HealthConditionInterests
            };
        }
        
        public static UpdateParticipantDemographicsRequest MapTo(UpdateParticipantDemographicsCommand source)
        {
            return new UpdateParticipantDemographicsRequest
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
                HealthConditionInterests = source.HealthConditionInterests
            };
        }

        public static CreateParticipantAddressRequest MapTo(ParticipantAddressModel source)
        {
            return new CreateParticipantAddressRequest
            {
                AddressLine1 = source.AddressLine1,
                AddressLine2 = source.AddressLine2,
                AddressLine3 = source.AddressLine3,
                AddressLine4 = source.AddressLine4,
                Town = source.Town,
                Postcode = source.Postcode,
            };
        }

        public static ParticipantSuitabilityResponse MapTo(Dte.Participant.Api.Client.Responses.Participants.ParticipantSuitabilityResponse source)
        {
            return new ParticipantSuitabilityResponse
            {
                StudyTitle = source.StudyTitle,
                IsSuitable = source.IsSuitable
            };
        }

        public static CreateParticipantRegistrationRequest MapTo(CreateParticipantRegistrationCommand source)
        {
            return new CreateParticipantRegistrationRequest
            {
                StudyId = source.StudyId,
                SiteId = source.ParticipantId,
                ParticipantId = source.ParticipantId
            };
        }
    }
}