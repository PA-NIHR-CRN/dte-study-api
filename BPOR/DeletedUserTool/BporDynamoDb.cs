using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using BPOR.Domain.Entities;
using Microsoft.Extensions.Options;

namespace DeletedUserTool;

public class BporDynamoDb(IOptions<DynamoDbSettings> options)
{
    private DynamoDBContext CreateContext()
    {
        var dynamoDbConfig = new AmazonDynamoDBConfig();
        dynamoDbConfig.Profile = new Profile(options.Value.Profile);
        dynamoDbConfig.RegionEndpoint = RegionEndpoint.GetBySystemName(options.Value.RegionEndpoint);
        var client = new AmazonDynamoDBClient(dynamoDbConfig);
        return new DynamoDBContext(client);
    }

    public async Task<List<DynamoParticipant>> GetParticipantsByPk(string pk)
    {
        using var context = CreateContext();
        DynamoDBOperationConfig operationConfig = new DynamoDBOperationConfig()
        {
            OverrideTableName = options.Value.TableName
        };
        var query = context.QueryAsync<DynamoParticipant>(pk, operationConfig);
        return await query.GetRemainingAsync();
    }
    
    public async Task<List<DynamoParticipant>> GetParticipantsByEmail(IEnumerable<string> emails)
    {
        using var context = CreateContext();
        DynamoDBOperationConfig operationConfig = new DynamoDBOperationConfig
        {
            OverrideTableName = options.Value.TableName
        };
        ScanCondition scanCondition = new ScanCondition("Email", ScanOperator.In, emails.Cast<object>().ToArray());
        var query = context.ScanAsync<DynamoParticipant>([scanCondition], operationConfig);
        return await query.GetRemainingAsync();
    }
}