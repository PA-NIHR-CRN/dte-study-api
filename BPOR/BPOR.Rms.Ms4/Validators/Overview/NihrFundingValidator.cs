using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Overview;

public class NihrFundingValidator : AbstractValidator<StudyRequestViewModel>
{
    public NihrFundingValidator()
    {
        RuleFor(model => model.NihrFundingStatus)
            .NotNull()
            .WithMessage("Select an option");
    }
}