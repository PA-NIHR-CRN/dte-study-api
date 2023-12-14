using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Configuration;

namespace Dynamo.Stream.Ingestor.Repository
{
    public class DynamoParticipantRepository : IDynamoParticipantRepository
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly string _tableName;

        public DynamoParticipantRepository(IAmazonDynamoDB dynamoDbClient, IConfiguration configuration)
        {
            _dynamoDbClient = dynamoDbClient;
            _tableName = configuration["DynamoDB:TableName"];
        }

        public async Task<IEnumerable<Dictionary<string, AttributeValue>>> GetAllParticipantsAsAttributeMapsAsync(
            CancellationToken cancellationToken = default)
        {
            var request = new ScanRequest
            {
                TableName = _tableName
            };

            var response = await _dynamoDbClient.ScanAsync(request, cancellationToken);
            return response.Items;
        }
    }
}
