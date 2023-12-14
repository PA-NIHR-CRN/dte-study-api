using Dynamo.Stream.Handler.Helpers;
using Microsoft.Extensions.Hosting;

namespace Dynamo.Stream.Handler.Tests.Helpers;

public class IsDevelopmentTests
{
    [Fact]
    public void IsDevelopment_ReturnsTrue_WhenEnvironmentIsDevelopment()
    {
        // Arrange
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", Environments.Development);

        // Act
        var result = EnvironmentHelper.IsDevelopment();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDevelopment_ReturnsFalse_WhenEnvironmentIsNotDevelopment()
    {
        // Arrange
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", Environments.Production);

        // Act
        var result = EnvironmentHelper.IsDevelopment();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsDevelopment_ReturnsFalse_WhenEnvironmentIsNull()
    {
        // Arrange
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", null);

        // Act
        var result = EnvironmentHelper.IsDevelopment();

        // Assert
        Assert.False(result);
    }
}
