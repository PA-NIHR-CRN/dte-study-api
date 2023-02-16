using Amazon.DynamoDBv2.DataModel;

namespace Domain.Entities.Studies
{
    /// <summary>
    /// Will decide on the permissions a userid has against a study depending on the Role.
    /// </summary>
    public class ResearcherStudy
    {
        [DynamoDBHashKey("PK")] public string Pk { get; set; }
        [DynamoDBRangeKey("SK")] public string Sk { get; set; }
        
        [DynamoDBProperty] public string UserId { get; set; }
        [DynamoDBProperty] public string Firstname { get; set; }
        [DynamoDBProperty] public string Lastname { get; set; }
        [DynamoDBProperty] public string Email { get; set; }
        [DynamoDBProperty] public long StudyId { get; set; }
        [DynamoDBProperty] public ResearcherStudyRole Role { get; set; }
    }
}