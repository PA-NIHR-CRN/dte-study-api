using Amazon.Lambda.DynamoDBEvents;

namespace Dynamo.Stream.Ingestor.Services;

public interface ILambdaStreamHandler
{
    public Task<IEnumerable<StreamsEventResponse.BatchItemFailure>> ProcessStreamAsync(DynamoDBEvent dynamoDbEvent,
        CancellationToken cancellationToken);
}
