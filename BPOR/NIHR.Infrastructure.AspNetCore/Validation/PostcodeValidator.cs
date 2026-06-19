using FluentValidation;
using FluentValidation.Validators;
using NIHR.Infrastructure.AspNetCore;

namespace BPOR.Rms.Validators;

public class PostcodeValidator<T> : PropertyValidator<T, string?>, INotEmptyValidator
{
    public override string Name => "PostcodeValidator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        return string.IsNullOrEmpty(value) || Postcode.TryParse(value, out _);
    }

    protected override string GetDefaultMessageTemplate(string errorCode) {
        return Localized(errorCode, Name);
    }
}