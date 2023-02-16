using Amazon.DynamoDBv2.DataModel;

namespace Domain.Entities.Users
{
    public class AccessWhitelist
    {
        [DynamoDBHashKey("PK")] public string Pk { get; set; }
	
        [DynamoDBProperty] public string Email { get; set; }
    }
}