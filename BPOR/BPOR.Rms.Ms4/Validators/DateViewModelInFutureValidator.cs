using BPOR.Rms.Ms4.Models;
using FluentValidation;
using FluentValidation.Validators;

namespace BPOR.Rms.Ms4.Validators;

public class DateViewModelInFutureValidator<T> : PropertyValidator<T, DateViewModel>
{
    public override string Name => "DateViewModelInFutureValidator";

    public override bool IsValid(ValidationContext<T> context, DateViewModel value)
    {
        return !value.IsComplete || 
               !DateHelper.IsValidDate(value.Year.Value,  value.Month.Value, value.Day.Value) ||
               new DateTime(value.Year.Value, value.Month.Value, value.Day.Value) > DateTime.Now;
    }

    protected override string GetDefaultMessageTemplate(string errorCode) {
        return Localized(errorCode, Name);
    }
}