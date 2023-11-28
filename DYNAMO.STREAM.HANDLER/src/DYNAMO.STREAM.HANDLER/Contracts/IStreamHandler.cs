using Amazon.Lambda.DynamoDBEvents;

namespace DYNAMO.STREAM.HANDLER.Contracts;

public interface IStreamHandler
{
    Task ProcessStreamAsync(DynamoDBEvent dynamoDbEvent, CancellationToken cancellationToken);
}
