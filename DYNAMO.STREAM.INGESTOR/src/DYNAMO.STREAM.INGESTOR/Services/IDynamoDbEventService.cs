using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.DynamoDBEvents;

namespace DYNAMO.STREAM.INGESTOR.Services;

public interface IDynamoDbEventService
{
     DynamoDBEvent CreateParticipantInsertEvent(Dictionary<string, AttributeValue> participant);
}
