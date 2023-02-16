using System;
using Amazon.DynamoDBv2.DataModel;
using Domain.Converters;

namespace Domain.Entities.StudyRegistrations
{
    public class StudyRegistration
    {
        [DynamoDBHashKey("PK")] public string Pk { get; set; }

        [DynamoDBProperty] public long StudyId { get; set; }
        [DynamoDBProperty] public string Title { get; set; }
        [DynamoDBProperty] public string ResearcherId { get; set; }
        [DynamoDBProperty] public string ResearcherFirstname { get; set; }
        [DynamoDBProperty] public string ResearcherLastname { get; set; }
        [DynamoDBProperty] public string ResearcherEmail { get; set; }
        [DynamoDBGlobalSecondaryIndexHashKey] public StudyRegistrationStatus StudyRegistrationStatus { get; set; }
        [DynamoDBProperty] public string ApprovedById { get; set; }
        [DynamoDBProperty(typeof(DateTimeUtcConverter))] public DateTime? ApprovedAtUtc { get; set; }
        [DynamoDBProperty] public string RejectedById { get; set; }
        [DynamoDBProperty(typeof(DateTimeUtcConverter))] public DateTime? RejectedAtUtc { get; set; }
        [DynamoDBProperty(typeof(DateTimeUtcConverter))] public DateTime SubmittedAtUtc { get; set; }
    }
}