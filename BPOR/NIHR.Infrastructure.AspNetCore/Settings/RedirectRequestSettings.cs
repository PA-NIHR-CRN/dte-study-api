using NIHR.Infrastructure.Settings;

namespace NIHR.Infrastructure.AspNetCore.Settings;

public class RedirectUriValidationSettings
{
    public bool AllowRelative { get; set; } = true;
    public bool AllowCurrentAuthority { get; set; } = true;
    public OriginSpec[] AllowOrigins { get; set; } = [];
}