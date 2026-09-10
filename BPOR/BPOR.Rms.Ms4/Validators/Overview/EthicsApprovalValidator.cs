using BPOR.Rms.Ms4.Models;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Overview;

public class EthicsApprovalValidator : AbstractValidator<StudyRequestViewModel>
{
    public EthicsApprovalValidator()
    {
        RuleFor(model => model.HasEthicsApproval)
            .NotNull()
            .WithMessage("Select an option");
    }
}