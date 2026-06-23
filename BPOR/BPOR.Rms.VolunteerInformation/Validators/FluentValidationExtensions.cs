using FluentValidation;
using FluentValidation.Validators;

namespace BPOR.Rms.VolunteerInformation.Validators;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string?> MaximumLengthWithStandardMessage<T>(this IRuleBuilder<T, string?> ruleBuilder, int maximumLength) {
        return ruleBuilder.SetValidator(new MaximumLengthValidator<T>(maximumLength))
            .WithMessage($"You can add up to {maximumLength} characters");
    }
}