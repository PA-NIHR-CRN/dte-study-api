using Amazon.DynamoDBv2.Model;

namespace Dynamo.Stream.Handler.Extensions;

public static class Extensions
{
    public static string PK(this Dictionary<string, AttributeValue> record)
    {
        return record["PK"].S;
    }
    
    public static void UpdatePrimaryKey(this Dictionary<string,AttributeValue> record)
    {
        var id = record.PK().StripPrimaryKey();
        record["PK"] = new AttributeValue($"PARTICIPANT#{id}");
        record["SK"] = new AttributeValue($"PARTICIPANT#");
    }
    
    public static string StripPrimaryKey(this string pk)
    {
        return pk.Replace("PARTICIPANT#", "");
    }
}
