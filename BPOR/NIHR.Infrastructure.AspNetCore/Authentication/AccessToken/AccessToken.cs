using System.Text.Json.Serialization;

namespace NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

public class AccessToken
{
    [JsonConstructor]
    public AccessToken(string role,  Dictionary<string, string> routeValues)
    {
        Role = role;
        RouteValues = routeValues;
    }

    public AccessToken(string role)
    {
        Role = role;
        RouteValues = new();
    }

    public AccessToken WithRoute(string routeName, string routeValue)
    {
        RouteValues.Add(routeName, routeValue);
        return this;
    }

    public string Role { get; }
    public Dictionary<string, string> RouteValues { get; }
}