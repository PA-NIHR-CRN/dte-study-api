namespace NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;

public class FixedApiKey
{
    public string ApiKey { get; set; }
    public List<FixedApiKeyClaim>?  Claims { get; set; }
    public List<string>? Roles { get; set; }
}