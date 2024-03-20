using Amazon.Lambda.DynamoDBEvents;

namespace BPOR.Registration.Stream.Handler.Handlers;

public interface IStreamHandler
{
    Task<IEnumerable<StreamsEventResponse.BatchItemFailure>> ProcessStreamAsync(DynamoDBEvent dynamoDbEvent,
        CancellationToken cancellationToken);
}
