using BPOR.Rms.Ms4.Models.Details;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Details;

public class StudyDescriptionViewModelValidator : AbstractValidator<StudyDescriptionViewModel>
{
    public StudyDescriptionViewModelValidator()
    {
        RuleFor(model => model.StudyTitle)
            .NotNull()
            .WithMessage("Enter a study title");
        
        RuleFor(model => model.StudyDescription)
            .NotNull()
            .WithMessage("Provide a description");
    }
}