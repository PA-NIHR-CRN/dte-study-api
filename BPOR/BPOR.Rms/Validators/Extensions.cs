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
    
    public static IRuleBuilderOptions<T, string?> UriOrNullOrWhitespace<T>(this IRuleBuilder<T, string?> ruleBuilder,
        UriKind uriKind = UriKind.Absolute)
    {
        return ruleBuilder.Must(value =>
            string.IsNullOrWhiteSpace(value) || Uri.IsWellFormedUriString(value.Trim(), uriKind));
    }
}