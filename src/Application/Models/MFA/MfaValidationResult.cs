namespace Application.Models.MFA;

public enum MfaValidationResult
{
    Success,
    UserNotFound,
    CodeExpired,
    CodeInvalid
}