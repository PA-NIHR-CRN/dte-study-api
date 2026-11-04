using System.ComponentModel.DataAnnotations;

namespace NIHR.Infrastructure.AspNetCore.Services;

public interface IRedirectUriValidator
{
    ValidationResult? Validate(string redirectUri);
    ValidationResult? Validate(Uri uri);
}