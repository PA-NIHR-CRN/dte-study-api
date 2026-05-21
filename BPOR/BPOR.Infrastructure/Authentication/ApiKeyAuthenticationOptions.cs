using Microsoft.AspNetCore.Authentication;

namespace BPOR.Infrastructure.Authentication;

public sealed class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "ApiKey";
    public const string HeaderName = "Authorization";

    public string ApiKey { get; set; } = "QCuvqKsDZeJJaanAzRTvgUTr";
}