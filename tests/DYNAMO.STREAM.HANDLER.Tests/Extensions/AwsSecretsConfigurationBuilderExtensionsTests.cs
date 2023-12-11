using Dte.Common.Lambda.SecretsManagement.AwsSecretsManager;
using DYNAMO.STREAM.HANDLER.Extensions;
using Microsoft.Extensions.Configuration;

namespace DYNAMO.STREAM.HANDLER.Tests.Extensions;

public class AwsSecretsConfigurationBuilderExtensionsTests
{
    private readonly IConfigurationBuilder _configurationBuilder = new ConfigurationBuilder();

    [Fact]
    public void AddAwsSecrets_ReturnsSameConfigurationBuilder_WhenInDevelopment()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

        var result = _configurationBuilder.AddAwsSecrets();

        Assert.Same(_configurationBuilder, result);
    }


    [Fact]
    public void AddAwsSecrets_ThrowsException_WhenAwsSecretsNameIsNotSet()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
        Environment.SetEnvironmentVariable("AWS_SECRET_MANAGER_SECRET_NAME", null);

        Assert.Throws<Exception>(() => _configurationBuilder.AddAwsSecrets());
    }

    [Fact]
    public void AddAwsSecrets_AddsSecretsManager_WhenAwsSecretsNameIsSet()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
        Environment.SetEnvironmentVariable("AWS_SECRET_MANAGER_SECRET_NAME", "TestSecret");

        var result = _configurationBuilder.AddAwsSecrets();

        Assert.Single(result.Sources);
        Assert.IsType<SecretsManagerConfigurationSource>(result.Sources.First());
    }
}
