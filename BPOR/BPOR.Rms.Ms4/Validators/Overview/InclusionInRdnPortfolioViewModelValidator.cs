using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Models.Overview;
using FluentValidation;

namespace BPOR.Rms.Ms4.Validators.Overview;

public class InclusionInRdnPortfolioViewModelValidator : AbstractValidator<InclusionInRdnPortfolioViewModel>
{
    public InclusionInRdnPortfolioViewModelValidator()
    {
        RuleFor(model => model.InclusionInRdnPortfolioStatus)
            .NotNull()
            .WithMessage("Select an option");

        RuleFor(model => model.CpmsId)
            .NotEmpty()
            .WithMessage("Enter CPMS ID to continue")
            .When(model => model.InclusionInRdnPortfolioStatus == InclusionInRdnPortfolioStatus.HasApproval);
    }
}