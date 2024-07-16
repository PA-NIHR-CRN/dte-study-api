using System.ComponentModel.DataAnnotations;

namespace BPOR.Domain.Settings;

public class NhsLoginSettings : IValidatableObject
{
    public string ClientId { get; set; }
    public string BaseUrl { get; set; }
    public string TokenEndpoint { get; set; }
    public string UserInfoEndpoint { get; set; }

    // Private key must be supplied using either PemFilePath or PrivateKey.
    // If both are specified, PrivateKey takes precedence.
    // PemFilePath specifies the file path to the PEM file holding the private key.
    public string PemFilePath { get; set; }

    // PrivateKey holds the key contents directly.
    public string PrivateKey { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(ClientId))
        {
            yield return new ValidationResult("ClientId is required", new[] { nameof(ClientId) });
        }
        if (string.IsNullOrWhiteSpace(BaseUrl))
        {
            yield return new ValidationResult("BaseUrl is required", new[] { nameof(BaseUrl) });
        }
        if (string.IsNullOrWhiteSpace(TokenEndpoint))
        {
            yield return new ValidationResult("TokenEndpoint is required", new[] { nameof(TokenEndpoint) });
        }
        if (string.IsNullOrWhiteSpace(UserInfoEndpoint))
        {
            yield return new ValidationResult("UserInfoEndpoint is required", new[] { nameof(UserInfoEndpoint) });
        }
        if (string.IsNullOrWhiteSpace(PemFilePath) && string.IsNullOrWhiteSpace(PrivateKey))
        {
            yield return new ValidationResult("PemFilePath or PrivateKey is required", new[] { nameof(PemFilePath), nameof(PrivateKey) });
        }
    }
}
