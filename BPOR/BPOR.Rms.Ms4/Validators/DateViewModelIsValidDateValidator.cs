using BPOR.Rms.Ms4.Models;
using FluentValidation;
using FluentValidation.Validators;
using NIHR.GovUk.AspNetCore.Mvc;

namespace BPOR.Rms.Ms4.Validators;

public class DateViewModelIsValidDateValidator<T> : PropertyValidator<T, GovUkDate>
{
    public override string Name => "DateViewModelInFutureValidator";

    public override bool IsValid(ValidationContext<T> context, GovUkDate value)
    {
        return !value.HasValue || 
               DateHelper.IsValidDate(value.Year.Value,  value.Month.Value, value.Day.Value);
    }

    protected override string GetDefaultMessageTemplate(string errorCode) {
        return Localized(errorCode, Name);
    }
}