namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public class AccessToken
{
    public AccessToken()
    {
    }

    public AccessToken(string role)
    {
        Role = role;
    }

    public AccessToken WithRoute(string routeName, string routeValue)
    {
        RouteValues.Add(routeName, routeValue);
        return this;
    }

    public string Role { get; set; }
    public Dictionary<string, string> RouteValues { get; set; } = new();
}