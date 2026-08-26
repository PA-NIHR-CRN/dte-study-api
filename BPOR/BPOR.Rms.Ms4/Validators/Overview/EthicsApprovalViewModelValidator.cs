using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Models.Overview;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Overview;

public class EthicsApprovalViewModelValidator : AbstractValidator<EthicsApprovalViewModel>
{
    public EthicsApprovalViewModelValidator()
    {
        RuleFor(model => model.HasEthicsApproval)
            .NotNull()
            .WithMessage("Select an option");
    }
}