using System.ComponentModel.DataAnnotations;

namespace NIHR.Infrastructure.AspNetCore.Services;

public interface IRedirectValidator
{
    ValidationResult? Validate(string redirectUri);
    ValidationResult? Validate(Uri uri);
}