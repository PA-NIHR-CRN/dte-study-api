using Amazon.DynamoDBv2.Model;

namespace Dynamo.Stream.Ingestor.Repository;

public interface IDynamoParticipantRepository
{
    public IAsyncEnumerable<Dictionary<string, AttributeValue>> GetAllParticipantsAsAttributeMapsAsync(
        CancellationToken cancellationToken = default);
}
