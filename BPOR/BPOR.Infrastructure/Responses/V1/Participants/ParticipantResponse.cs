using BPOR.Infrastructure.Models.Address;

namespace BPOR.Infrastructure.Responses.V1.Participants;

public class ParticipantResponse
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
    public string MobileNumber { get; set; }
    public string LandlineNumber { get; set; }
    public ParticipantAddressModel Address { get; set; }
    public string SexRegisteredAtBirth { get; set; }
    public bool? GenderIsSameAsSexRegisteredAtBirth { get; set; }
    public string EthnicGroup { get; set; }
    public string EthnicBackground { get; set; }
    public bool? Disability { get; set; }
    public string DisabilityDescription { get; set; }
    public IEnumerable<string> HealthConditionInterests { get; set; }
    public bool ConsentContact { get; set; }
}
