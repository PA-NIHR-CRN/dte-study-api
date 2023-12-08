using Microsoft.Extensions.Hosting;

namespace DYNAMO.STREAM.HANDLER.Helpers;

public static class EnvironmentHelper
{
    public static bool IsDevelopment()
    {
        return string.Equals(System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            Environments.Development, System.StringComparison.OrdinalIgnoreCase);
    }
}
