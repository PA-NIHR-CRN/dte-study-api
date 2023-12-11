using Amazon.DynamoDBv2.Model;
using DYNAMO.STREAM.HANDLER.Extensions;

namespace DYNAMO.STREAM.HANDLER.Tests.Extensions;

public class ExtensionsTests
{
    [Fact]
    public void PK_ReturnsCorrectValue_WhenRecordContainsPK()
    {
        var record = new Dictionary<string, AttributeValue>
        {
            { "PK", new AttributeValue { S = "TestValue" } }
        };

        var result = record.PK();

        Assert.Equal("TestValue", result);
    }

    [Fact]
    public void PK_ThrowsException_WhenRecordDoesNotContainPK()
    {
        var record = new Dictionary<string, AttributeValue>();

        Assert.Throws<KeyNotFoundException>(() => record.PK());
    }

    [Fact]
    public void PK_ThrowsException_WhenPKValueIsNull()
    {
        var record = new Dictionary<string, AttributeValue>
        {
            { "PK", new AttributeValue { S = null } }
        };
        
        var result = record.PK();

        Assert.Null(result);
    }
}
