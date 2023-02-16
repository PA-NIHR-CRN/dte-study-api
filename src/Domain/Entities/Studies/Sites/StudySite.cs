using System;
using Amazon.DynamoDBv2.DataModel;
using Domain.Converters;

namespace Domain.Entities.Studies.Sites
{
    public class StudySite
    {
        [DynamoDBHashKey("PK")] public string Pk { get; set; }
        [DynamoDBRangeKey("SK")] public string Sk { get; set; }
        
        [DynamoDBProperty] public string Id { get; set; }
        [DynamoDBProperty] public string RtsIdentifier { get; set; }
        [DynamoDBProperty] public long StudyId { get; set; }
        [DynamoDBProperty] public string Name { get; set; }
        [DynamoDBProperty] public string Description { get; set; }
        [DynamoDBProperty] public SiteAddress Address { get; set; }
        [DynamoDBProperty] public StudySiteStatus Status { get; set; }
        
        [DynamoDBProperty(typeof(DateTimeUtcConverter))] public DateTime CreatedAtUtc { get; set; }
        [DynamoDBProperty] public string CreatedById { get; set; }
        [DynamoDBProperty(typeof(DateTimeUtcConverter))] public DateTime? UpdatedAtUtc { get; set; }
        [DynamoDBProperty] public string UpdatedById { get; set; }
    }
} 