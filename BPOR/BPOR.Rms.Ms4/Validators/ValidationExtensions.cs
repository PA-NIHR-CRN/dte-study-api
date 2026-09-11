using BPOR.Rms.Ms4.Models;
using FluentValidation;
using NIHR.GovUk.AspNetCore.Mvc;

namespace BPOR.Rms.Ms4.Validators;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, GovUkDate> IsInFuture<T>(this IRuleBuilder<T, GovUkDate> ruleBuilder)
        => ruleBuilder.SetValidator(new DateViewModelInFutureValidator<T>());

    public static IRuleBuilderOptions<T, GovUkDate> IsValidDate<T>(this IRuleBuilder<T, GovUkDate> ruleBuilder) 
        => ruleBuilder.SetValidator(new DateViewModelIsValidDateValidator<T>());
    
    public static IRuleBuilderOptions<T, GovUkDate> IsComplete<T>(this IRuleBuilder<T, GovUkDate> ruleBuilder) 
        => ruleBuilder.SetValidator(new DateViewModelCompleteValidator());
}