namespace BPOR.Registration.Api.Requests.Participants;

public class UpdateParticipantRequest
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string SelectedLocale { get; set; }
    public string MobileNumber { get; set; }
    public string LandlineNumber { get; set; }
    public CreateParticipantAddressRequest Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string SexRegisteredAtBirth { get; set; }
    public bool? GenderIsSameAsSexRegisteredAtBirth { get; set; }
    public string EthnicGroup { get; set; }
    public string EthnicBackground { get; set; }
    public bool? Disability { get; set; }
    public string DisabilityDescription { get; set; }
    public List<string> HealthConditionInterests { get; set; }
}
