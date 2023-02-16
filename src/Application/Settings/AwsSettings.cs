namespace Application.Settings
{
    public class AwsSettings
    {
        public static string SectionName => "AwsSettings";
        public string StudyRegistrationDynamoDbTableName { get; set; }
        public string ParticipantRegistrationDynamoDbTableName { get; set; }
        public string ResearcherStudyDynamoDbTableName { get; set; }
        public string AccessWhitelistDynamoDbTableName { get; set; }
        public string SomeQueueUrl { get; set; }
        public string ServiceUrl { get; set; }
        public string CognitoPoolId { get; set; }
        public string[] CognitoAppClientIds { get; set; }
        public string CognitoRegion { get; set; }
    }
}