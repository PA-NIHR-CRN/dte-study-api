using System;
using System.Collections.Generic;
using Application.Mappings.AutoMapper;
using AutoMapper;
using Domain.Entities.Participants;

namespace Application.Models.Participants
{
    public class ParticipantDemographicsModel : IMapFrom<ParticipantDemographics>
    {
        public string MobileNumber { get; set; }
        public string LandlineNumber { get; set; }
        public ParticipantAddressModel Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string SexRegisteredAtBirth { get; set; }
        public bool? GenderIsSameAsSexRegisteredAtBirth { get; set; }
        public string EthnicGroup { get; set; }
        public string EthnicBackground { get; set; }
        public bool? Disability { get; set; }
        public string DisabilityDescription { get; set; }
        public IEnumerable<string> HealthConditionInterests { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ParticipantAddress, ParticipantAddressModel>();
            profile.CreateMap<ParticipantDemographics, ParticipantDemographicsModel>();
        }
    }
}