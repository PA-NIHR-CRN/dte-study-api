using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Application.Contracts;
using Application.Settings;
using Domain.Entities.StudyRegistrations;
using Dte.Common.Persistence;

namespace Infrastructure.Persistence
{
    public class StudyRegistrationDynamoDbRepository : BaseDynamoDbRepository, IStudyRegistrationRepository
    {
        private readonly IAmazonDynamoDB _client;
        private readonly IDynamoDBContext _context;
        private readonly DynamoDBOperationConfig _config;
        
        private static string StudyKey() => "STUDY#";
        private static string StudyKey(long studyId) => $"STUDY#{studyId}";

        public StudyRegistrationDynamoDbRepository(IAmazonDynamoDB client, IDynamoDBContext context, AwsSettings awsSettings) : base(client, context)
        {
            _client = client;
            _context = context;
            _config = new DynamoDBOperationConfig { OverrideTableName = awsSettings.StudyRegistrationDynamoDbTableName };
        }
        
        public async Task<StudyRegistration> GetStudyRegistrationAsync(long studyId)
        {
            return await _context.LoadAsync<StudyRegistration>(StudyKey(studyId), _config);
        }

        // TODO - return paged data for ALL queries
        public async Task<IEnumerable<StudyRegistration>> GetStudyRegistrationsByStatusAsync(StudyRegistrationStatus status)
        {
            var items = await _context.QueryAsync<StudyRegistration>
                (
                    status, QueryOperator.BeginsWith, new object [] { StudyKey() }, new DynamoDBOperationConfig
                    {
                        OverrideTableName = _config.OverrideTableName,
                        IndexName = "StudyStatusIndex"
                    }
                )
                .GetRemainingAsync();
            
            return items;
        }

        public async Task CreateStudyRegistrationAsync(StudyRegistration entity)
        {
            entity.Pk = StudyKey(entity.StudyId);

            await _context.SaveAsync(entity, _config);
        }

        public async Task SaveStudyRegistrationAsync(StudyRegistration entity)
        {
            await _context.SaveAsync(entity, _config);
        }
    }
}