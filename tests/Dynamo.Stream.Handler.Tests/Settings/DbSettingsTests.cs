using Dynamo.Stream.Handler.Settings;

namespace Dynamo.Stream.Handler.Tests.Settings;

public class DbSettingsTests
{
    [Fact]
    public void BuildConnectionString_ReturnsCorrectValue_WhenAllPropertiesAreSet()
    {
        // Arrange
        var dbSettings = new DbSettings
        {
            Username = "TestUser",
            Password = "TestPassword",
            Host = "TestHost",
            Port = 3306,
            Database = "TestDatabase"
        };

        // Act
        var result = dbSettings.BuildConnectionString();

        // Assert
        Assert.Equal(
            "Server=TestHost;Port=3306;User ID=TestUser;Password=TestPassword;Database=TestDatabase",
            result);
    }

    [Theory]
    [InlineData("Username", "User ID=")]
    [InlineData("Password", "Password=")]
    [InlineData("Database", "Database=")]
    public void BuildConnectionString_ReturnsConnectionStringWithoutProperty_WhenPropertyIsNull(string propertyName,
        string expectedMissingPart)
    {
        // Arrange
        var dbSettings = new DbSettings
        {
            Username = "TestUser",
            Password = "TestPassword",
            Host = "TestHost",
            Port = 3306,
            Database = "TestDatabase"
        };

        typeof(DbSettings).GetProperty(propertyName)?.SetValue(dbSettings, null);

        // Act
        var result = dbSettings.BuildConnectionString();

        // Assert
        Assert.Contains("Server=TestHost", result);
        Assert.Contains("Port=3306", result);

        Assert.DoesNotContain(expectedMissingPart, result);
    }
}
