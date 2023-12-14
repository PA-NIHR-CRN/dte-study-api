using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.DynamoDBEvents;

namespace Dynamo.Stream.Ingestor.Services;

public interface IDynamoDbEventService
{
    DynamoDBEvent CreateEvent(OperationType eventType, Dictionary<string, AttributeValue> newImage, Dictionary<string, AttributeValue> oldImage = null!);
}
