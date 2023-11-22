using Amazon.Lambda.DynamoDBEvents;

namespace DYNAMO.STREAM.HANDLER.Contracts;

public interface IStreamHandler
{
    Task ProcessStream(DynamoDBEvent dynamoDbEvent);
}
