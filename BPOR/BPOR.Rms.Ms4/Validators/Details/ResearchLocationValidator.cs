using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Details;

public class ResearchLocationValidator : AbstractValidator<StudyDetailsViewModel>
{
    public ResearchLocationValidator()
    {
        RuleFor(model => model.HasMoreThanOneResearchLocation)
            .NotNull()
            .WithMessage("Select an option");
    }
}