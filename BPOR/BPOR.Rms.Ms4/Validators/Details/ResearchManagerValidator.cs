using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Details;

public class ResearchManagerValidator : AbstractValidator<StudyDetailsViewModel>
{
    public ResearchManagerValidator()
    {
        RuleFor(model => model.HasOnePersonResponsibleForRecruiting)
            .NotNull()
            .WithMessage("Select an option");
    }
}