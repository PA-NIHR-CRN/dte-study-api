using Amazon.Lambda.DynamoDBEvents;
using static Amazon.Lambda.DynamoDBEvents.StreamsEventResponse;

namespace DYNAMO.STREAM.HANDLER.Handlers;

public interface IStreamHandler
{
    Task<IEnumerable<BatchItemFailure>> ProcessStreamAsync(DynamoDBEvent dynamoDbEvent, CancellationToken cancellationToken);
}
