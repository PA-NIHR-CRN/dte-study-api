namespace NIHR.Rts.Client.Settings;

public class RtsApiSettings
{
    public static string SectionName => "RtsApiSettings";
    public string BaseUrl { get; set; }
    public string TokenUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public double AddressCacheTtlMinutes { get; set; }
    public double TokenCacheTtlMinutes { get; set; }
}