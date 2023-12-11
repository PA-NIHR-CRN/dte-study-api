namespace DYNAMO.STREAM.HANDLER.Tests.Settings;

public class DbSettingsTests
{
    [Fact]
    public void BuildConnectionString_ReturnsCorrectValue_WhenAllPropertiesAreSet()
    {
        var dbSettings = new DbSettings
        {
            Username = "TestUser",
            Password = "TestPassword",
            Host = "TestHost",
            Port = 3306,
            Database = "TestDatabase"
        };

        var result = dbSettings.BuildConnectionString();

        Assert.Equal(
            "Server=TestHost;Port=3306;User ID=TestUser;Password=TestPassword;Database=TestDbClusterIdentifier",
            result);
    }

    [Theory]
    [InlineData("Username", "User ID=")]
    [InlineData("Password", "Password=")]
    [InlineData("DbClusterIdentifier", "Database=")]
    public void BuildConnectionString_ReturnsConnectionStringWithoutProperty_WhenPropertyIsNull(string propertyName,
        string expectedMissingPart)
    {
        var dbSettings = new DbSettings
        {
            Username = "TestUser",
            Password = "TestPassword",
            Host = "TestHost",
            Port = 3306,
            Database = "TestDatabase"
        };

        typeof(DbSettings).GetProperty(propertyName)?.SetValue(dbSettings, null);

        var result = dbSettings.BuildConnectionString();

        Assert.Contains("Server=TestHost", result);
        Assert.Contains("Port=3306", result);

        Assert.DoesNotContain(expectedMissingPart, result);
    }
}
