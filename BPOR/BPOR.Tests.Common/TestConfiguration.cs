using Microsoft.Extensions.Configuration;

namespace BPOR.Tests.Common
{
    public static class TestConfiguration
    {
        public static IConfiguration GetStandardConfiguration() =>
             new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", true)
            .AddJsonFile("appsettings.user.json", true)
            .Build();
    }
}
