using Amazon.DynamoDBv2.Model;

namespace DYNAMO.STREAM.HANDLER.Extensions;

public static class Extensions
{
    public static string PK (this Dictionary<string,AttributeValue>  record)
    {
        return record["PK"].S;
    }
}
