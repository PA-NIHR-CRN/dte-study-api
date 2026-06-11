namespace BPOR.Rms.VolunteerInformation;

public class RrvTokenOptions
{
    public bool EnableRrvApi { get; set; } = false;
    public TimeSpan Ttl { get; set; } = TimeSpan.FromMinutes(5);
    public string Issuer { get; set; } = "RMS";
    public string Audience { get; set; } = "RRV";
    public string SymmetricKey { get; set; } = "ckyHinyNHwgGHum6VMUo6SwqIb1CTDtGBxFVCS3tuSgGQfONhO2AHiAlFzKVERk8"; // TODO: Remove this and force explicit config.
}