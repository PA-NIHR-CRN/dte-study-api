using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Overview;

public class FinishRecruitingValidator : AbstractValidator<OverviewViewModel>
{
    public FinishRecruitingValidator()
    {
        RuleFor(x => x.FinishRecruitingDay)
            .NotEmpty()
            .WithMessage("Enter the day recruitment will finish");

        RuleFor(x => x.FinishRecruitingMonth)
            .NotEmpty()
            .WithMessage("Enter the month recruitment will finish");

        RuleFor(x => x.FinishRecruitingYear)
            .NotEmpty()
            .WithMessage("Enter the year recruitment will finish");

        RuleFor(x => x)
            .Must(BeAValidDate)
            .WithMessage("Enter a valid recruitment end date")
            .When(x =>
                !string.IsNullOrWhiteSpace(x.FinishRecruitingDay) &&
                !string.IsNullOrWhiteSpace(x.FinishRecruitingMonth) &&
                !string.IsNullOrWhiteSpace(x.FinishRecruitingYear));

        RuleFor(x => x)
            .Must(BeInFuture)
            .WithMessage("Recruitment end date must be today or in the future")
            .When(BeAValidDate);
    }

    private static bool BeAValidDate(OverviewViewModel model)
    {
        return DateOnly.TryParse(
            $"{model.FinishRecruitingYear}-{model.FinishRecruitingMonth}-{model.FinishRecruitingDay}",
            out _);
    }

    private static bool BeInFuture(OverviewViewModel model)
    {
        if (!DateOnly.TryParse(
                $"{model.FinishRecruitingYear}-{model.FinishRecruitingMonth}-{model.FinishRecruitingDay}",
                out var date))
        {
            return false;
        }

        return date >= DateOnly.FromDateTime(DateTime.Today);
    }
}