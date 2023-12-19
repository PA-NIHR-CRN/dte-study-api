using Amazon.Lambda.DynamoDBEvents;
using static Amazon.Lambda.DynamoDBEvents.StreamsEventResponse;

namespace Dynamo.Stream.Handler.Handlers;

public interface IStreamHandler
{
    Task<IEnumerable<BatchItemFailure>> ProcessStreamAsync(DynamoDBEvent dynamoDbEvent,
        IServiceProvider serviceProvider, CancellationToken cancellationToken);
}
