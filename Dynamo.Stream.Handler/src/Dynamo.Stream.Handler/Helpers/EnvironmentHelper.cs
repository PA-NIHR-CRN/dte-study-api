using Microsoft.Extensions.Hosting;

namespace Dynamo.Stream.Handler.Helpers;

public static class EnvironmentHelper
{
    public static bool IsDevelopment()
    {
        return string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            Environments.Development, StringComparison.OrdinalIgnoreCase);
    }
}
