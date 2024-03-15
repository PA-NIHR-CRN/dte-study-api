using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using BPOR.Domain.Entities;
using BPOR.Domain.Interfaces;
using BPOR.Domain.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BPOR.Domain.Repositories;

public class ParticipantDynamoDbRepository(
    IAmazonDynamoDB client,
    IDynamoDBContext context,
    ILogger<ParticipantDynamoDbRepository> logger,
    DynamoDBOperationConfig config)
    : IParticipantRepository
{
    public async Task<DynamoParticipant> GetParticipantAsync(string participantId, CancellationToken cancellationToken)
    {
        return await context.LoadAsync<DynamoParticipant>(KeyUtils.ParticipantKey(participantId),
            KeyUtils.ParticipantKey(),
            config, cancellationToken);
    }

    public async Task CreateParticipantAsync(DynamoParticipant entity, CancellationToken cancellationToken)
    {
        entity.Pk = KeyUtils.ParticipantKey(string.IsNullOrEmpty(entity.ParticipantId)
            ? entity.NhsId
            : entity.ParticipantId);
        entity.Sk = KeyUtils.ParticipantKey();

        await context.SaveAsync(entity, config, cancellationToken);
    }

    public async Task<DynamoParticipant> UpdateParticipantAsync(DynamoParticipant entity,
        CancellationToken cancellationToken)
    {
        await context.SaveAsync(entity, config, cancellationToken);
        return entity;
    }

    public async Task DeleteParticipantAsync(DynamoParticipant participant, CancellationToken cancellationToken)
    {
        await context.DeleteAsync(participant, config, cancellationToken);
    }

    public async Task<DynamoParticipant> QueryIndexForParticipantAsync(string query, string colName,
        CancellationToken cancellationToken)
    {
        var request = new QueryRequest
        {
            TableName = config.OverrideTableName,
            KeyConditionExpression = $"{colName} = :value",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":value", new AttributeValue { S = query } }
            },
            IndexName = $"{colName}Index",
        };

        logger.LogInformation("request: {Request}", JsonConvert.SerializeObject(request, Formatting.Indented));


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
        return await GetParticipantAsync(KeyUtils.StripPrimaryKey(participant.Pk), cancellationToken);
    }
}
