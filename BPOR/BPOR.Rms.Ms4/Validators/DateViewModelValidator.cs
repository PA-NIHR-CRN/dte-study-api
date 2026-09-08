using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators;

public class DateViewModelValidator : AbstractValidator<DateViewModel>
{
    public DateViewModelValidator()
    {
        RuleFor(x => x.Day)
            .NotEmpty()
            .WithMessage("Enter a day");

        RuleFor(x => x.Month)
            .NotEmpty()
            .WithMessage("Enter a month");

        RuleFor(x => x.Year)
            .NotEmpty()
            .WithMessage("Enter a year");
    }
}