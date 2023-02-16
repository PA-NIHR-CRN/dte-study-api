using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Application.Contracts;
using Application.Settings;
using Domain.Entities.Users;
using Dte.Common.Persistence;

namespace Infrastructure.Persistence
{
    public class AccessWhitelistRepository : BaseDynamoDbRepository, IAccessWhitelistRepository
    {
        private readonly IAmazonDynamoDB _client;
        private readonly IDynamoDBContext _context;
        private readonly DynamoDBOperationConfig _config;
        
        private static string EmailKey(string email) => $"EMAIL#{email.ToLower()}";
        
        public AccessWhitelistRepository(IAmazonDynamoDB client, IDynamoDBContext context, AwsSettings awsSettings) : base(client, context)
        {
            _client = client;
            _context = context;
            _config = new DynamoDBOperationConfig { OverrideTableName = awsSettings.AccessWhitelistDynamoDbTableName };
        }

        public async Task<AccessWhitelist> GetWhitelistByEmail(string email)
        {
            return await _context.LoadAsync<AccessWhitelist>(EmailKey(email), _config);
        }

        public async Task<IEnumerable<AccessWhitelist>> GetWhitelist()
        {
            return await _context.ScanAsync<AccessWhitelist>(null, _config).GetRemainingAsync();
        }

        public async Task SaveWhitelist(IEnumerable<string> emails)
        {
            await BatchInsertAsync(emails.Select(x => new AccessWhitelist{Pk = EmailKey(x), Email = x}), _config);
        }
    }
}