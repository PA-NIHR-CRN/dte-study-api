using Amazon.DynamoDBv2.Model;

namespace BPOR.Domain.Extensions;

public static class KeyExtensions
{
    public static string PK(this Dictionary<string, AttributeValue> record)
    {
        return record.TryGetValue("PK", out var value) ? value.S : string.Empty;
    }
}
