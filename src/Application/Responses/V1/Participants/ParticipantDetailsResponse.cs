using System;

namespace Application.Responses.V1.Participants
{
    public class ParticipantDetailsResponse
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string SelectedLocale { get; set; }
        public bool ConsentRegistration { get; set; }
        public string NhsNumber { get; set; }
        public string NhsId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? ConsentRegistrationAtUtc { get; set; }
        public DateTime? RemovalOfConsentRegistrationAtUtc { get; set; }
        public bool HasDemographics { get; set; }
    }
}