using Amazon.DynamoDBv2.Model;

namespace Dynamo.Stream.Ingestor.Repository;

public interface IDynamoParticipantRepository
{
    public Task<IEnumerable<Dictionary<string, AttributeValue>>> GetAllParticipantsAsAttributeMapsAsync(
        CancellationToken cancellationToken = default);
}
