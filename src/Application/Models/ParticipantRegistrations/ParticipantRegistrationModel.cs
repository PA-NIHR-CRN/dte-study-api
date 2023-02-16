using System;
using Application.Mappings.AutoMapper;
using Application.Mappings.Participants;
using Application.Models.Participants;
using AutoMapper;
using Domain.Entities.ParticipantRegistrations;

namespace Application.Models.ParticipantRegistrations
{
    public class ParticipantRegistrationModel : IMapFrom<ParticipantRegistration>
    {
        public long StudyId { get; set; }
        public string SiteId { get; set; }
        public string ParticipantId { get; set; }
        public string ParticipantRegistrationStatus { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public DateTime SubmittedAtUtc { get; set; }
        public ParticipantDetailsModel ParticipantDetails { get; set; }
        public ParticipantDemographicsModel ParticipantDemographics { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ParticipantRegistration, ParticipantRegistrationModel>()
                .ForMember(x => x.ParticipantRegistrationStatus, opt => { opt.MapFrom(source => Enum.GetName(typeof(ParticipantRegistrationStatus), source.ParticipantRegistrationStatus)); })
                .ForMember(x => x.ParticipantDetails, opt =>
                {
                    opt.MapFrom(source => new ParticipantDetailsModel
                    {
                        Firstname = PrivateDetailsAllowed(source) ? source.Firstname : null,
                        Lastname = PrivateDetailsAllowed(source) ? source.Lastname : null,
                        Email = PrivateDetailsAllowed(source) ? source.Email : null,
                    });
                })
                .ForMember(x => x.ParticipantDemographics, opt =>
                {
                    opt.MapFrom(source => new ParticipantDemographicsModel
                    {
                        MobileNumber = PrivateDetailsAllowed(source) ? source.MobileNumber : null,
                        LandlineNumber = PrivateDetailsAllowed(source) ? source.LandlineNumber : null,
                        Address = PrivateDetailsAllowed(source) ? ParticipantMapper.MapTo(source.Address) : null,
                        DateOfBirth = PrivateDetailsAllowed(source) ? source.DateOfBirth : (DateTime?)null,
                        SexRegisteredAtBirth = source.SexRegisteredAtBirth,
                        GenderIsSameAsSexRegisteredAtBirth = source.GenderIsSameAsSexRegisteredAtBirth,
                        EthnicGroup = source.EthnicGroup,
                        EthnicBackground = source.EthnicBackground,
                        Disability = source.Disability,
                        DisabilityDescription = source.DisabilityDescription,
                        HealthConditionInterests = source.HealthConditionInterests,
                    });
                });
        }

        private static bool PrivateDetailsAllowed(ParticipantRegistration source)
        {
            // Not for applied but only for 'Screening', 'Enrolled', 'Not enrolled', 'Withdrawn'
            return source.ParticipantRegistrationStatus == Domain.Entities.ParticipantRegistrations.ParticipantRegistrationStatus.Screening ||
                   source.ParticipantRegistrationStatus == Domain.Entities.ParticipantRegistrations.ParticipantRegistrationStatus.Enrolled ||
                   source.ParticipantRegistrationStatus == Domain.Entities.ParticipantRegistrations.ParticipantRegistrationStatus.NotEnrolled ||
                   source.ParticipantRegistrationStatus == Domain.Entities.ParticipantRegistrations.ParticipantRegistrationStatus.Withdrawn;
        }
    }
}