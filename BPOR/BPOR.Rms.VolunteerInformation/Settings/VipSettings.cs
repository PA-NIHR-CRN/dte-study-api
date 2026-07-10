namespace BPOR.Rms.VolunteerInformation.Settings;

public class VipSettings
{
    public string GoogleDocUri { get; set; } = "https://docs.google.com/document/d/11diU2-gtufQ5UjwWqrggQrFgv7XVCz8rADXCJde28-s/edit?usp=sharing";
    public string BporVipUri { get; set; }
    public string S3BucketName { get; set; }
    
    public bool EnableDevelopmentFeatures { get; set; }
    public string VipGoogleDocUrl { get; set; }
    public string BporContentApiKey { get; set; }
    public string RrvPrescreenerApiKey { get; set; }
}