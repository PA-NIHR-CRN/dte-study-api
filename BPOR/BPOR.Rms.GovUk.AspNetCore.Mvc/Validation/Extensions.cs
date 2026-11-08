using FluentValidation;

namespace NIHR.GovUk.AspNetCore.Mvc.Validation;

public static class Extensions
{
    public static IRuleBuilderOptions<T, GovUkDate> IsInFuture<T>(this IRuleBuilder<T, GovUkDate> ruleBuilder)
        => ruleBuilder.SetValidator(new DateViewModelInFutureValidator<T>());

    public static IRuleBuilderOptions<T, GovUkDate> IsValidDate<T>(this IRuleBuilder<T, GovUkDate> ruleBuilder) 
        => ruleBuilder.SetValidator(new DateViewModelIsValidDateValidator<T>());
    
    public static IRuleBuilderOptions<T, GovUkDate> IsComplete<T>(this IRuleBuilder<T, GovUkDate> ruleBuilder) 
        => ruleBuilder.SetValidator(new DateViewModelCompleteValidator());
}