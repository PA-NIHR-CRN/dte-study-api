using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Application.Contracts;
using Application.Settings;
using Domain.Entities.Participants;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ParticipantDetails> GetParticipantDetailsAsync(string participantId)
        {
            return await _context.LoadAsync<ParticipantDetails>(ParticipantKey(participantId), ParticipantKey(),
                _config);
        }

        private async Task<IEnumerable<ParticipantDetails>> QueryIndexAsync(string query, string colName)
        {
            var results = new List<ParticipantDetails>();
            Dictionary<string, AttributeValue> lastEvaluatedKey = null;

            do
            {
                var request = new QueryRequest
                {
                    TableName = _config.OverrideTableName,
                    KeyConditionExpression = $"{colName} = :value",
                    ExpressionAttributeValues =
                        new Dictionary<string, AttributeValue>
                        {
                            { ":value", new AttributeValue { S = query } }
                        },
                    IndexName = $"{colName}Index",
                    ExclusiveStartKey = lastEvaluatedKey
                };

                var response = await _client.QueryAsync(request);

                foreach (var item in response.Items)
                {
                    var participant = _context.FromDocument<ParticipantDetails>(Document.FromAttributeMap(item));
                    if (participant != null)
                    {
                        results.Add(participant);
                    }
                }

                lastEvaluatedKey = response.LastEvaluatedKey?.Count > 0 ? response.LastEvaluatedKey : null;
            }
            while (lastEvaluatedKey != null);

            return results;
        }


        public async Task<ParticipantDetails> QueryIndexForParticipantDetailsAsync(string query, string colName)
        {
            var items = await QueryIndexAsync(query, colName);

            if (!items.Any())
            {
                return null;
            }

            return items.OrderByDescending(x => x.CreatedAtUtc).First();
        }

        public async Task<IReadOnlyCollection<ParticipantDetails>> GetAllParticipantDetailsByEmailAsync(string email)
        {
            return (await QueryIndexAsync(email, "Email")).ToList().AsReadOnly();
        }

        public async Task<ParticipantDemographics> GetParticipantDemographicsAsync(string participantId)
        {
            return await _context.LoadAsync<ParticipantDemographics>(ParticipantKey(participantId), ParticipantKey(),
                _config);
        }

        public async Task CreateParticipantDetailsAsync(ParticipantDetails entity)
        {
            // TODO pull out this logic into a mapper
            entity.Pk = ParticipantKey(string.IsNullOrEmpty(entity.ParticipantId)
                ? entity.NhsId
                : entity.ParticipantId);
            entity.Sk = ParticipantKey();

            await _context.SaveAsync(entity, _config);
        }

        public async Task UpdateParticipantDetailsAsync(ParticipantDetails entity)
        {
            await _context.SaveAsync(entity, _config);
        }

        public async Task CreateParticipantDemographicsAsync(ParticipantDemographics entity)
        {
            entity.Pk = ParticipantKey(entity.ParticipantId);
            entity.Sk = ParticipantKey();

            await _context.SaveAsync(entity, _config);
        }

        public async Task UpdateParticipantDemographicsAsync(ParticipantDemographics entity)
        {
            await _context.SaveAsync(entity, _config);
        }

        public async Task DeleteParticipantDetailsAsync(ParticipantDetails entity)
        {
            await _context.DeleteAsync(entity, _config);
        }

        public async Task CreateAnonymisedDemographicParticipantDataAsync(ParticipantDetails entity)
        {
            await _context.SaveAsync(entity, _config);
        }
    }
}