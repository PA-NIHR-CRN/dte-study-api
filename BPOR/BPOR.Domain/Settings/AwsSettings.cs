using System.ComponentModel.DataAnnotations;

namespace BPOR.Domain.Settings;

public class AwsSettings : IValidatableObject
{
    public static string SectionName => "AwsSettings";
    public string ParticipantRegistrationDynamoDbTableName { get; set; }
    public string ServiceUrl { get; set; }
    public string CognitoPoolId { get; set; }
    public string[] CognitoAppClientIds { get; set; }
    public string CognitoRegion { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(ParticipantRegistrationDynamoDbTableName))
        {
            yield return new ValidationResult("ParticipantRegistrationDynamoDbTableName is required", new[] { nameof(ParticipantRegistrationDynamoDbTableName) });
        }
        if (string.IsNullOrWhiteSpace(CognitoPoolId))
        {
            yield return new ValidationResult("CognitoPoolId is required", new[] { nameof(CognitoPoolId) });
        }
        if (CognitoAppClientIds == null || CognitoAppClientIds.Length == 0)
        {
            yield return new ValidationResult("CognitoAppClientIds is required", new[] { nameof(CognitoAppClientIds) });
        }
        if (string.IsNullOrWhiteSpace(CognitoRegion))
        {
            yield return new ValidationResult("CognitoRegion is required", new[] { nameof(CognitoRegion) });
        }
    }
}
