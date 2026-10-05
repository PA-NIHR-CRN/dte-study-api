using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators;

public class StudyRequestStartViewModelValidator : AbstractValidator<StudyRequestStartViewModel>
{
    public StudyRequestStartViewModelValidator()
    {
        RuleFor(x => x.TermsAccepted)
            .Equal(true)
            .WithMessage("Read the terms and conditions before starting the form.");
    }
}