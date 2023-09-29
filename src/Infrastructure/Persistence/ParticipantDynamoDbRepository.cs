using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Application.Contracts;
using Application.Settings;
using Domain.Entities.Participants;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Persistence
{
    public class ParticipantDynamoDbRepository : IParticipantRepository
    {
        private readonly IAmazonDynamoDB _client;
        private readonly IDynamoDBContext _context;
        private readonly ILogger<ParticipantDynamoDbRepository> _logger;
        private readonly DynamoDBOperationConfig _config;

        private static string ParticipantKey(string participantId) => $"PARTICIPANT#{participantId}";
        private static string ParticipantKey() => "PARTICIPANT#";

        public ParticipantDynamoDbRepository(IAmazonDynamoDB client, IDynamoDBContext context, AwsSettings awsSettings,
            ILogger<ParticipantDynamoDbRepository> logger)
        {
            _client = client;
            _context = context;
            this._logger = logger;
            _config = new DynamoDBOperationConfig
                { OverrideTableName = awsSettings.ParticipantRegistrationDynamoDbTableName };
        }

        public async Task<ParticipantDetails> GetParticipantDetailsAsync(string participantId,
            CancellationToken cancellationToken = default)
        {
            return await _context.LoadAsync<ParticipantDetails>(ParticipantKey(participantId), ParticipantKey(),
                _config, cancellationToken);
        }

        public async Task<ParticipantDetails> QueryIndexForParticipantDetailsAsync(string query, string colName,
            CancellationToken cancellationToken = default)
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

            _logger.LogInformation("request: {Request}", JsonConvert.SerializeObject(request, Formatting.Indented));
            var response = await _client.QueryAsync(request, cancellationToken);

            _logger.LogInformation("response: {Response}",
                JsonConvert.SerializeObject(response, Formatting.Indented));

            var items = response.Items;
            if (items.Count == 0) return null;
            var item = items.OrderByDescending(x => DateTime.Parse(x["CreatedAtUtc"].S)).First();

            _logger.LogInformation("item: {Item}", JsonConvert.SerializeObject(item, Formatting.Indented));

            var participant = _context.FromDocument<ParticipantDetails>(Document.FromAttributeMap(item));
            return await GetParticipantDetailsAsync(participant.Pk.Replace("PARTICIPANT#", ""), cancellationToken);
        }

        public async Task<ParticipantDemographics> GetParticipantDemographicsAsync(string participantId,
            CancellationToken cancellationToken = default)
        {
            return await _context.LoadAsync<ParticipantDemographics>(ParticipantKey(participantId), ParticipantKey(),
                _config, cancellationToken);
        }

        public async Task CreateParticipantDetailsAsync(ParticipantDetails entity,
            CancellationToken cancellationToken = default)
        {
            // TODO pull out this logic into a mapper
            entity.Pk = ParticipantKey(string.IsNullOrEmpty(entity.ParticipantId)
                ? entity.NhsId
                : entity.ParticipantId);
            entity.Sk = ParticipantKey();

            await _context.SaveAsync(entity, _config, cancellationToken);
        }

        public async Task UpdateParticipantDetailsAsync(ParticipantDetails entity,
            CancellationToken cancellationToken = default)
        {
            await _context.SaveAsync(entity, _config, cancellationToken);
        }

        public async Task CreateParticipantDemographicsAsync(ParticipantDemographics entity,
            CancellationToken cancellationToken = default)
        {
            entity.Pk = ParticipantKey(entity.ParticipantId);
            entity.Sk = ParticipantKey();

            await _context.SaveAsync(entity, _config, cancellationToken);
        }

        public async Task AddDemographicsToNhsUserAsync(ParticipantDemographics entity, string nhsId,
            CancellationToken cancellationToken = default)
        {
            entity.Pk = ParticipantKey(nhsId);
            entity.Sk = ParticipantKey();

            await _context.SaveAsync(entity, _config, cancellationToken);
        }

        public async Task UpdateParticipantDemographicsAsync(ParticipantDemographics entity,
            CancellationToken cancellationToken = default)
        {
            await _context.SaveAsync(entity, _config, cancellationToken);
        }

        public async Task DeleteParticipantDetailsAsync(ParticipantDetails entity,
            CancellationToken cancellationToken = default)
        {
            await _context.DeleteAsync(entity, _config, cancellationToken);
        }

        public async Task CreateAnonymisedDemographicParticipantDataAsync(ParticipantDetails entity,
            CancellationToken cancellationToken = default)
        {
            await _context.SaveAsync(entity, _config, cancellationToken);
        }

        public async Task<Participant> GetParticipantAsync(string pk, CancellationToken cancellationToken = default)
        {
            return await _context.LoadAsync<Participant>(pk, ParticipantKey(),
                _config, cancellationToken);
        }

        public async Task DeleteParticipantAsync(Participant entity, CancellationToken cancellationToken = default)
        {
            await _context.DeleteAsync(entity, _config, cancellationToken);
        }

        public async Task CreateParticipantAsync(Participant entity, CancellationToken cancellationToken = default)
        {
            await _context.SaveAsync(entity, _config, cancellationToken);
        }

        public async IAsyncEnumerator<Participant> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            var search = _context.ScanAsync<Participant>(null, _config);

            while (!search.IsDone)
            {
                var page = await search.GetNextSetAsync(cancellationToken);
                foreach (var item in page)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return item;
                }
            }
        }
    }
}