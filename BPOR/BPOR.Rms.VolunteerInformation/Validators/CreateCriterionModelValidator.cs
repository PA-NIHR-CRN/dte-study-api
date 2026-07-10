using BPOR.Rms.VolunteerInformation.Models;
using FluentValidation;

namespace BPOR.Rms.VolunteerInformation.Validators;

public class CreateCriterionModelValidator : AbstractValidator<CreateCriterionPostbackModel>
{
    public CreateCriterionModelValidator()
    {
        RuleFor(i => i.Criterion)
            .NotEmpty().WithMessage("You must provide inclusion/exclusion criteria for this group.")
            .MaximumLength(200).WithMessage("You can add up to 200 characters.");
    }
}