using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Models.Overview;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Overview;

public class FinishRecruitingViewModelValidator : AbstractValidator<FinishRecruitingViewModel>
{
    public FinishRecruitingViewModelValidator()
    {
        RuleFor(x => x.Day)
            .NotEmpty()
            .WithMessage("Enter the day recruitment will finish");

        RuleFor(x => x.Month)
            .NotEmpty()
            .WithMessage("Enter the month recruitment will finish");

        RuleFor(x => x.Year)
            .NotEmpty()
            .WithMessage("Enter the year recruitment will finish");

        RuleFor(x => x)
            .Must(BeAValidDate)
            .WithMessage("Enter a valid recruitment end date")
            .When(x =>
                !string.IsNullOrWhiteSpace(x.Day) &&
                !string.IsNullOrWhiteSpace(x.Month) &&
                !string.IsNullOrWhiteSpace(x.Year));

        RuleFor(x => x)
            .Must(BeInFuture)
            .WithMessage("Recruitment end date must be today or in the future")
            .When(BeAValidDate);
    }

    private static bool BeAValidDate(FinishRecruitingViewModel model)
    {
        return DateOnly.TryParse(
            $"{model.Year}-{model.Month}-{model.Day}",
            out _);
    }

    private static bool BeInFuture(FinishRecruitingViewModel model)
    {
        if (!DateOnly.TryParse(
                $"{model.Year}-{model.Month}-{model.Day}",
                out var date))
        {
            return false;
        }

        return date >= DateOnly.FromDateTime(DateTime.Today);
    }
}