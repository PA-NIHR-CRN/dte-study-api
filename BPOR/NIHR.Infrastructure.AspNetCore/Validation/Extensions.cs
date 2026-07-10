using System.Linq.Expressions;
using BPOR.Rms.Validators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NIHR.Infrastructure.AspNetCore.Validation;

public static class Extensions
{
    const int maxUrlLength = 2048;
    
    public static void AddValidationResult(this ModelStateDictionary modelState, ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }

    public static void AddToModelState(this ValidationResult validationResult, ModelStateDictionary modelState)
        => modelState.AddValidationResult(validationResult);

    public static ValidationResult ValidateSpecificProperties<T>(this IValidator<T> validator, T instance,
        params Expression<Func<T, object?>>[] properties)
    {
        return validator.Validate(instance, options => options.IncludeProperties(properties));
    }
    
    public static int CountWords(this string value)
    {
        int result = 0;
        bool inWord = false;
        
        foreach (char c in value)
        {
            bool isWhitespace = char.IsWhiteSpace(c);
            if (!isWhitespace && !inWord)
            {
                result++;
            }
            inWord = !isWhitespace;
        }

        return result;
    }
    
    
    /// <summary>
    /// Validates a URI as per RFC 3986 and, per convention, allows a maximum of 2048 characters.
    /// </summary>
    public static IRuleBuilderOptions<T, string?> Postcode<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
        ruleBuilder.SetValidator(new PostcodeValidator<T>());

    /// <summary>
    /// Validates a URI as per RFC 3986 and, per convention, allows a maximum of 2048 characters.
    /// </summary>
    public static IRuleBuilderOptions<T, string?> MaxWords<T>(this IRuleBuilder<T, string?> ruleBuilder,
        int maxWordCount) =>
        ruleBuilder.SetValidator(new MaxWordValidator<T>(maxWordCount));

    /// <summary>
    /// Validates a URI as per RFC 3986 and, per convention, allows a maximum of 2048 characters.
    /// </summary>
    /// <param name="uriKind"> The kind of URI to allow (absolute or relative). Defaults to absolute</param>
    /// <param name="schemes"> The URI schemes to allow. Defaults to https only.</param>
    public static IRuleBuilderOptionsConditions<T, string?> Uri<T>(this IRuleBuilder<T, string?> ruleBuilder,
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

            if (value.Length > maxUrlLength)
            {
                context.AddFailure($"The link must be less that {maxUrlLength + 1} characters");
            }

            if (!System.Uri.TryCreate(value, uriKind, out var uri))
            {
                context.AddFailure("The link you entered isn’t in the correct format");
                return;
            }

            if (!schemes.Contains(uri.Scheme, StringComparer.OrdinalIgnoreCase))
            {
                context.AddFailure($"The link you entered isn’t in the correct format - it must start with {string.Join(" or ", schemes.Select(i => $"{i}://"))}");
            }
        });
    }
}