using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Dynamo.Stream.Ingestor.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Dynamo.Stream.Ingestor.Repository
{
    public class DynamoParticipantRepository : IDynamoParticipantRepository
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly DynamoDbSettings _dynamoDbSettings;


        public DynamoParticipantRepository(IAmazonDynamoDB dynamoDbClient, IConfiguration configuration, IOptions<DynamoDbSettings > dynamoDbSettings)
        {
            _dynamoDbClient = dynamoDbClient;
            _dynamoDbSettings = dynamoDbSettings.Value;
        }

        public async Task<IEnumerable<Dictionary<string, AttributeValue>>> GetAllParticipantsAsAttributeMapsAsync(
            CancellationToken cancellationToken = default)
        {
            var request = new ScanRequest
            {
                TableName = _dynamoDbSettings.TableName,
            };

            var response = await _dynamoDbClient.ScanAsync(request, cancellationToken);
            return response.Items;
        }
    }
}
