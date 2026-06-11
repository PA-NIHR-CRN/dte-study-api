namespace NIHR.Rts.Client.Settings;

public class RtsApiSettings
{
    public static string SectionName => "RtsApiSettings";
    public string BaseUrl { get; set; }
    public string TokenUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string AddressCacheTimeSpanMinutes { get; set; }
    public string TokenCacheTimeSpanHours { get; set; }
}