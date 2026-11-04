using FluentValidation;
using FluentValidation.Validators;
using NIHR.Infrastructure;

namespace BPOR.Rms.Infrastructure.AspNetCore.Validation;

public class MaxWordValidator<T>(int maxWordCount) : PropertyValidator<T, string?>
{
    public override string Name => "MaxWordValidator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        return value == null || value.CountWords() <= maxWordCount;
    }

    protected override string GetDefaultMessageTemplate(string errorCode) {
        return Localized(errorCode, Name);
    }
}