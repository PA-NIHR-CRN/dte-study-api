using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Overview;

public class FinishRecruitingValidator : AbstractValidator<StudyRequestViewModel>
{
    public FinishRecruitingValidator()
    {
        RuleFor(x => x.FinishRecruitingDay)
            .NotEmpty()
            .WithMessage("Enter a day");

        RuleFor(x => x.FinishRecruitingMonth)
            .NotEmpty()
            .WithMessage("Enter a month");

        RuleFor(x => x.FinishRecruitingYear)
            .NotEmpty()
            .WithMessage("Enter a year");

        RuleFor(x => x)
            .Must(BeAValidDate)
            .WithMessage("Enter a real date")
            .DependentRules(() =>
            {
                RuleFor(x => x)
                    .Must(BeInFuture)
                    .WithMessage("Date of finishing study must be in the future");
            });
    }

    private static bool BeAValidDate(StudyRequestViewModel model)
    {
        if (!model.FinishRecruitingYear.HasValue || 
            !model.FinishRecruitingMonth.HasValue || 
            !model.FinishRecruitingDay.HasValue)
        {
            return false;
        }

        try
        {
            _ = new DateOnly(
                model.FinishRecruitingYear.Value, 
                model.FinishRecruitingMonth.Value, 
                model.FinishRecruitingDay.Value
            );
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    private static bool BeInFuture(StudyRequestViewModel model)
    {
        if (model.FinishRecruitingYear.HasValue && 
            model.FinishRecruitingMonth.HasValue && 
            model.FinishRecruitingDay.HasValue)
        {
            try
            {
                var targetDate = new DateOnly(
                    model.FinishRecruitingYear.Value, 
                    model.FinishRecruitingMonth.Value, 
                    model.FinishRecruitingDay.Value
                );

                return targetDate >= DateOnly.FromDateTime(DateTime.Today);
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        return false;
    }
}
