namespace BPOR.Rms.VolunteerInformation.Settings;

public class VsiSettings
{
    public TimeSpan DraftTtl { get; set; } =  TimeSpan.FromHours(8);
    public string GoogleDocUri { get; set; } = "https://docs.google.com/document/d/11diU2-gtufQ5UjwWqrggQrFgv7XVCz8rADXCJde28-s/edit?usp=sharing";
}