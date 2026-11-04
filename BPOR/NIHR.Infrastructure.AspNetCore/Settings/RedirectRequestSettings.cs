namespace NIHR.Infrastructure.Settings;

public class RedirectValidationSettings
{
    public bool AllowRelativeUrls { get; set; } = true;
    public bool AllowCurrentAuthority { get; set; } = true;
    public OriginSpec[] AllowOrigins { get; set; } = [];
}