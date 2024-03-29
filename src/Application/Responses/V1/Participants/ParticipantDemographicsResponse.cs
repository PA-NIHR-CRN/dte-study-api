using System;
using System.Collections.Generic;
using Application.Models.Participants;

namespace Application.Responses.V1.Participants
{
    public class ParticipantDemographicsResponse
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
        public string SelectedLocale { get; set; }
        public IEnumerable<string> HealthConditionInterests { get; set; }
        public bool ConsentContact { get; set; }
        public bool HasDemographics { get; set; }
    }
}