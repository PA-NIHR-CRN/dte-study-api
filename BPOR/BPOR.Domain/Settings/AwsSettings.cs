namespace BPOR.Domain.Settings;

public class AwsSettings
{
    public static string SectionName => "AwsSettings";
    public string ParticipantRegistrationDynamoDbTableName { get; set; }
    public string ServiceUrl { get; set; }
    public string CognitoPoolId { get; set; }
    public string[] CognitoAppClientIds { get; set; }
    public string CognitoRegion { get; set; }
}
