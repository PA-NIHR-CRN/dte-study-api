using Microsoft.AspNetCore.Http;

namespace NIHR.GovUk.AspNetCore.Mvc
{
    public class GovUkOptions
    {
        public string? ServiceName { get; set; }

        public CookieOptions Cookies { get; set; } = new CookieOptions();

        public bool HasServiceName() => !string.IsNullOrWhiteSpace(ServiceName);
    }
}
