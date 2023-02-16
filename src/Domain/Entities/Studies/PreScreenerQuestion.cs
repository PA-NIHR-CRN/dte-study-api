using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;
using Domain.Converters;

namespace Domain.Entities.Studies
{
    public class PreScreenerQuestion
    {
        [DynamoDBHashKey("PK")] public string Pk { get; set; }
        [DynamoDBRangeKey("SK")] public string Sk { get; set; }

        [DynamoDBProperty] public string QuestionText { get; set; }
        [DynamoDBProperty] public string Explanation { get; set; }
        [DynamoDBProperty] public string Reference { get; set; }
        [DynamoDBProperty] public string AnswerOptionType { get; set; }
        [DynamoDBProperty] public List<string> AnswerOptions { get; set; }
        [DynamoDBProperty] public List<string> ValidAnswerOptions { get; set; }
        [DynamoDBProperty] public int Sequence { get; set; }
        
        [DynamoDBProperty(typeof(DateTimeUtcConverter))] public DateTime CreatedAtUtc { get; set; }
        [DynamoDBProperty] public string CreatedById { get; set; }
        [DynamoDBProperty(typeof(DateTimeUtcConverter))] public DateTime? UpdatedAtUtc { get; set; }
        [DynamoDBProperty] public string UpdatedById { get; set; }
    }
}