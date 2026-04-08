using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BPOR.Rms.Validators;

public static class Extensions
{
    public static void AddValidationResult(this ModelStateDictionary modelState, ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
    
    public static ValidationResult ValidateSpecificProperties<T>(this IValidator<T> validator, T instance,
        params Expression<Func<T, object?>>[] properties)
    {
        return validator.Validate(instance, options => options.IncludeProperties(properties));
    }
    
    /// <summary>
    /// Validates a URI as per RFC 3986 and, per convention, allows a maximum of 2048 characters.
    /// </summary>
    /// <param name="uriKind"> The kind of URI to allow (absolute or relative). Defaults to absolute</param>
    /// <param name="schemes"> The URI schemes to allow. Defaults to https only.</param>
    public static IRuleBuilder<T, string?> Uri<T>(this IRuleBuilder<T, string?> ruleBuilder,
        UriKind uriKind = UriKind.Absolute, params string[] schemes)
    {
        if (schemes.Length == 0)
        {
            schemes = ["https"];
        }

        return ruleBuilder.Custom((value, context) =>
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            value = value.Trim();

            if (value.Length > 2048)
            {
                context.AddFailure("The link must be less that 2049 characters");
            }

            if (!System.Uri.TryCreate(value, uriKind, out var uri))
            {
                context.AddFailure("The link you entered isn’t in the correct format");
                return;
            }

            if (!schemes.Contains(uri.Scheme, StringComparer.OrdinalIgnoreCase))
            {
                context.AddFailure($"The link must start with {string.Join(" or ", schemes.Select(i => $"{i}://"))}");
            }
        });
    }
}