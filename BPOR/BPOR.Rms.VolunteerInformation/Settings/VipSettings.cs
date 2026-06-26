namespace BPOR.Rms.VolunteerInformation.Settings;

public class VipSettings
{
    public TimeSpan DraftTtl { get; set; } =  TimeSpan.FromHours(8);
    public string GoogleDocUri { get; set; } = "https://docs.google.com/document/d/11diU2-gtufQ5UjwWqrggQrFgv7XVCz8rADXCJde28-s/edit?usp=sharing";
    public string BporVipUri { get; set; }
    public bool EnableRrvApi { get; set; } = false;
    public string S3BucketName { get; set; }

}