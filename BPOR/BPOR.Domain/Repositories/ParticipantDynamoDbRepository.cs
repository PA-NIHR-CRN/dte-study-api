using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using BPOR.Domain.Entities;
using BPOR.Domain.Interfaces;
using BPOR.Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BPOR.Domain.Repositories;

public class ParticipantDynamoDbRepository(
    IAmazonDynamoDB client,
    IDynamoDBContext context,
    IOptions<AwsSettings> awsSettings,
    ILogger<ParticipantDynamoDbRepository> logger)
    : IParticipantRepository
{
    private readonly DynamoDBOperationConfig _config = new()
        { OverrideTableName = awsSettings.Value.ParticipantRegistrationDynamoDbTableName };

    private static string ParticipantKey(string participantId) => $"PARTICIPANT#{participantId}";
    private static string ParticipantKey() => "PARTICIPANT#";

    public async Task<DynamoParticipant> GetParticipantAsync(string participantId, CancellationToken cancellationToken)
    {
        return await context.LoadAsync<DynamoParticipant>(ParticipantKey(participantId), ParticipantKey(),
            _config, cancellationToken);
    }

    public async Task CreateParticipantAsync(DynamoParticipant entity, CancellationToken cancellationToken)
    {
        entity.Pk = ParticipantKey(string.IsNullOrEmpty(entity.ParticipantId)
            ? entity.NhsId
            : entity.ParticipantId);
        entity.Sk = ParticipantKey();

        await context.SaveAsync(entity, _config, cancellationToken);
    }

    public async Task<DynamoParticipant> UpdateParticipantAsync(DynamoParticipant entity,
        CancellationToken cancellationToken)
    {
        await context.SaveAsync(entity, _config, cancellationToken);
        return entity;
    }

    public async Task DeleteParticipantAsync(string participantId, CancellationToken cancellationToken)
    {
        await context.DeleteAsync(participantId, _config, cancellationToken);
    }

    public async Task<DynamoParticipant> QueryIndexForParticipantAsync(string query, string colName,
        CancellationToken cancellationToken)
    {
        var request = new QueryRequest
        {
            TableName = _config.OverrideTableName,
            KeyConditionExpression = $"{colName} = :value",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":value", new AttributeValue { S = query } }
            },
            IndexName = $"{colName}Index",
        };

        logger.LogInformation("request: {Request}", JsonConvert.SerializeObject(request, Formatting.Indented));

        try
        {
            var response = await client.QueryAsync(request, cancellationToken);

            logger.LogInformation("response: {Response}", JsonConvert.SerializeObject(response, Formatting.Indented));

            var items = response.Items;
            if (items.Count == 0)
            {
                return null;
            }

            var item = items.OrderByDescending(x => DateTime.Parse(x["CreatedAtUtc"].S)).First();

            logger.LogInformation("item: {Item}", JsonConvert.SerializeObject(item, Formatting.Indented));

            var participant = context.FromDocument<DynamoParticipant>(Document.FromAttributeMap(item));
            return await GetParticipantAsync(participant.Pk.Replace("PARTICIPANT#", ""), cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
