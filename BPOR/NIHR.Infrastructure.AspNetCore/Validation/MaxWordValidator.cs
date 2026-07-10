using FluentValidation;
using FluentValidation.Validators;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.Validators;

public class MaxWordValidator<T>(int maxWordCount) : PropertyValidator<T, string?>
{
    public override string Name => "NotEmptyValidator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        return value == null || value.CountWords() <= maxWordCount;
    }

    protected override string GetDefaultMessageTemplate(string errorCode) {
        return Localized(errorCode, Name);
    }
}