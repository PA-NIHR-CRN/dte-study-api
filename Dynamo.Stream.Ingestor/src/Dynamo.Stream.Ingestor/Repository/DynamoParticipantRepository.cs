using System.Runtime.CompilerServices;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Dynamo.Stream.Ingestor.Settings;
using Microsoft.Extensions.Options;

namespace Dynamo.Stream.Ingestor.Repository
{
    public class DynamoParticipantRepository : IDynamoParticipantRepository
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly DynamoDbSettings _dynamoDbSettings;


        public DynamoParticipantRepository(IAmazonDynamoDB dynamoDbClient, IOptions<DynamoDbSettings> dynamoDbSettings)
        {
            _dynamoDbClient = dynamoDbClient;
            _dynamoDbSettings = dynamoDbSettings.Value;
        }

        public async IAsyncEnumerable<Dictionary<string, AttributeValue>> GetAllParticipantsAsAttributeMapsAsync(
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var request = new ScanRequest
            {
                TableName = _dynamoDbSettings.TableName,
            };
            
            ScanResponse response;

            do
            {
                response = await _dynamoDbClient.ScanAsync(request, cancellationToken);
                foreach (var item in response.Items)
                {
                    yield return item;
                }
                request.ExclusiveStartKey = response.LastEvaluatedKey;
            } while (response.LastEvaluatedKey.Any());
        }
    }
}
