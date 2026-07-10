using BPOR.Rms.VolunteerInformation.Models;
using FluentValidation;

namespace BPOR.Rms.VolunteerInformation.Validators;

public class CreateGroupModelValidator : AbstractValidator<CreateGroupModel>
{
    public CreateGroupModelValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("You must enter a group name to proceed.")
            .MaximumLength(200).WithMessage("You can add up to 200 characters.");
    }
}