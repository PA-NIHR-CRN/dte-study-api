using Amazon.DynamoDBv2.Model;

namespace DYNAMO.STREAM.INGESTOR.Repository;

public interface IDynamoParticipantRepository
{
    public Task<IEnumerable<Dictionary<string, AttributeValue>>> GetAllParticipantsAsAttributeMapsAsync(
        CancellationToken cancellationToken = default);
}
