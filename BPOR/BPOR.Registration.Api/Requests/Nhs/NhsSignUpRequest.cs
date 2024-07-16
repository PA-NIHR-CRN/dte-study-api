namespace BPOR.Registration.Api.Requests.Nhs;

public class NhsSignUpRequest
{
    public bool ConsentRegistration { get; set; }
    public string SelectedLocale { get; set; }
}
