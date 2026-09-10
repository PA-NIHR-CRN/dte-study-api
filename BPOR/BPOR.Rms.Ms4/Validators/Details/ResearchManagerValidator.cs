using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Details;

public class ResearchManagerValidator : AbstractValidator<StudyRequestViewModel>
{
    public ResearchManagerValidator()
    {
        RuleFor(model => model.SinglePersonResponsibleForRecruiting)
            .NotNull()
            .WithMessage("Select an option");
    }
}