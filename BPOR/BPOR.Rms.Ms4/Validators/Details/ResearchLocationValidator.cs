using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Details;

public class ResearchLocationValidator : AbstractValidator<StudyRequestViewModel>
{
    public ResearchLocationValidator()
    {
        RuleFor(model => model.HasMultipleResearchLocations)
            .NotNull()
            .WithMessage("Select an option");
    }
}