using Microsoft.AspNetCore.Http;

namespace NIHR.GovUk.AspNetCore.Mvc
{
    public class CookieOptions
    {
        public string? PolicyLink { get; set; }

        public string? Domain { get; set; }

        public CookieMode Mode { get; set; } = CookieMode.None;
    }
    
    public enum CookieMode
    {
        None,
        Essential,
        Additional,
        Analytics
    }
}
