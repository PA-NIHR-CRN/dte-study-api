using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NIHR.Infrastructure.AspNetCore.Validation;

public static class Extensions
{
    public static bool AddValidationResult(this ModelStateDictionary modelState, ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return validationResult.Errors.Any();
    }

    public static bool AddToModelState(this ValidationResult validationResult, ModelStateDictionary modelState)
        => modelState.AddValidationResult(validationResult);

    public static ValidationResult ValidateSpecificProperties<T>(this IValidator<T> validator, T instance,
        params Expression<Func<T, object?>>[] properties)
    {
        return validator.Validate(instance, options => options.IncludeProperties(properties));
    }
    
    public static bool ValidateSpecificProperties<T>(this IValidator<T> validator, T instance,
        ModelStateDictionary modelState, params Expression<Func<T, object?>>[] properties) =>
        validator.Validate(instance, options => options.IncludeProperties(properties))
            .AddToModelState(modelState);

    /// <summary>
    /// Validates that a UK postcode is syntactically correct.
    /// </summary>
    public static IRuleBuilderOptions<T, string?> Postcode<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
        ruleBuilder.SetValidator(new PostcodeValidator<T>());

    /// <summary>
    /// Validates that a string contains a maximum number of words.
    /// </summary>
    public static IRuleBuilderOptions<T, string?> MaxWords<T>(this IRuleBuilder<T, string?> ruleBuilder,
        int maxWordCount) =>
        ruleBuilder.SetValidator(new MaxWordValidator<T>(maxWordCount));

    /// <summary>
    /// Validates a URI as per RFC 3986 and, per convention, allows a maximum of 2048 characters.
    /// </summary>
    /// <param name="uriKind"> The kind of URI to allow (absolute or relative). Defaults to absolute</param>
    /// <param name="schemes"> The URI schemes to allow. Defaults to https only.</param>
    public static IRuleBuilderOptions<T, string?> Uri<T>(this IRuleBuilder<T, string?> ruleBuilder,
        UriKind uriKind = UriKind.Absolute, params string[] schemes) =>
        ruleBuilder.SetValidator(new UriValidator(uriKind, schemes));
}