using BPOR.Rms.VolunteerInformation.Models;
using FluentValidation;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.VolunteerInformation.Validators;

public class VsiValidator : AbstractValidator<VsiEditModel>
{
    public VsiValidator()
    {
        RuleFor(i => i.Description)
            .NotEmpty().WithMessage("You must enter a description of your study.")
            .MaxWords(60).WithMessage("You can add up to 60 words.");
        RuleFor(i => i.StudyType)
            .NotNull().WithMessage("Select an option to continue");
    }
}