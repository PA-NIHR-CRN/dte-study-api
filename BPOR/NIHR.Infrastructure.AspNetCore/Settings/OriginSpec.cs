using System.ComponentModel.DataAnnotations;

namespace NIHR.Infrastructure.AspNetCore.Settings;

public class OriginSpec
{
    public string[] Schemes { get; set; } = ["https"];
    [Required]
    public string Authority { get; set; }

    public int? Port { get; set; }
    public bool AllowSubAuthorities { get; set; } = true;
}