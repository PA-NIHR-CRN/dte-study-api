namespace BPOR.Infrastructure.Models.Mfa;

public enum MfaValidationResult
{
    Success,
    UserNotFound,
    CodeExpired,
    CodeInvalid
}
