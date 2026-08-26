using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Models.Overview;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Overview;

public class NihrFundingViewModelValidator : AbstractValidator<NihrFundingViewModel>
{
    public NihrFundingViewModelValidator()
    {
        RuleFor(model => model.NihrFundingStatus)
            .NotNull()
            .WithMessage("Select an option");
    }
}