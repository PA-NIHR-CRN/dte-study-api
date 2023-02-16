using Amazon.DynamoDBv2.DataModel;

namespace Domain.Entities.Studies
{
    /// <summary>
    /// Will decide on the permissions a userid has against a site depending on the Role.
    /// </summary>
    public class ResearcherSite
    {
        [DynamoDBHashKey("PK")] public string Pk { get; set; }
        [DynamoDBRangeKey("SK")] public string Sk { get; set; }
        
        [DynamoDBProperty] public string UserId { get; set; }
        [DynamoDBProperty] public string SiteId { get; set; }
        [DynamoDBProperty] public ResearcherStudyRole Role { get; set; }
    }
}