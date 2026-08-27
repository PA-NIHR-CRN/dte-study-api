using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Details;

public class StudyDescriptionValidator : AbstractValidator<StudyRequestViewModel>
{
    public StudyDescriptionValidator()
    {
        RuleFor(model => model.StudyTitle)
            .NotNull()
            .WithMessage("Enter a study title");
        
        RuleFor(model => model.StudyDescription)
            .NotNull()
            .WithMessage("Provide a description");
    }
}