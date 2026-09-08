using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators;

public static class ValidationExtensions
{
    /// <summary>
    /// Validates a URI as per RFC 3986 and, per convention, allows a maximum of 2048 characters.
    /// </summary>
    public static IRuleBuilderOptions<T, DateViewModel>
        IsInFuture<T>(this IRuleBuilder<T, DateViewModel> ruleBuilder) =>
        ruleBuilder.SetValidator(new DateViewModelInFutureValidator<T>());

    public static IRuleBuilderOptions<T, DateViewModel>
        IsValidDate<T>(this IRuleBuilder<T, DateViewModel> ruleBuilder) =>
        ruleBuilder.SetValidator(new DateViewModelIsValidDateValidator<T>());

    public static IRuleBuilderOptions<T, DateViewModel> IsComplete<T>(this IRuleBuilder<T, DateViewModel> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new DateViewModelValidator());
    }
}