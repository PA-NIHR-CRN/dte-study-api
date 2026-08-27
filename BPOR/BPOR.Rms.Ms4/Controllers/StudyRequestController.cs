using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Models.Enums;
using BPOR.Rms.Ms4.Validators.Details;
using BPOR.Rms.Ms4.Validators.Overview;
using BPOR.Rms.Ms4.Validators.ParticipantDetails;
using BPOR.Rms.Ms4.Validators.Sponsorship;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.Ms4.Controllers;

public class StudyRequestController : Controller
{
    [HttpGet]
    public IActionResult Start()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Start(
        StudyRequestStartViewModel model,
        [FromServices] IValidator<StudyRequestStartViewModel> validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View(model);
        }

        return RedirectToAction(nameof(EthicsApproval));
    }
    
    [HttpGet]
    public IActionResult EthicsApproval(CancellationToken cancellationToken)
    {
        return View("Overview/EthicsApproval");
    }

    [HttpPost]
    public async Task<IActionResult> EthicsApproval(
        StudyRequestViewModel model,
        [FromServices] EthicsApprovalValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/EthicsApproval", model);
        }

        return RedirectToAction(nameof(InclusionInRdnPortfolio));
    }
    
    [HttpGet]
    public IActionResult InclusionInRdnPortfolio(CancellationToken cancellationToken)
    {
        return View("Overview/InclusionInRdnPortfolio");
    }

    [HttpPost]
    public async Task<IActionResult> InclusionInRdnPortfolio(
        StudyRequestViewModel model,
        [FromServices] InclusionInRdnPortfolioValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/InclusionInRdnPortfolio", model);
        }

        return RedirectToAction(model.InclusionInRdnPortfolioStatus == InclusionInRdnPortfolioStatus.HasApproval ? nameof(FinishRecruiting) : nameof(NihrFunding));
    }
    
    [HttpGet]
    public ActionResult NihrFunding(CancellationToken cancellationToken)
    {
        return View("Overview/NihrFunding");
    }

    [HttpPost]
    public async Task<IActionResult> NihrFunding(
        StudyRequestViewModel model,
        [FromServices] NihrFundingValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/NihrFunding", model);
        }

        return RedirectToAction(model.NihrFundingStatus == NihrFundingStatus.NoNihrFunding ? nameof(MoreInformationRequired) : nameof(FinishRecruiting));
    }
    
    [HttpGet]
    public IActionResult FinishRecruiting(CancellationToken cancellationToken)
    {
        return View("Overview/FinishRecruiting");
    }

    [HttpPost]
    public async Task<IActionResult> FinishRecruiting(
        StudyRequestViewModel model,
        [FromServices] FinishRecruitingValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/FinishRecruiting", model);
        }

        return RedirectToAction(nameof(StudyDescription));
    }
    
    [HttpGet]
    public IActionResult MoreInformationRequired(CancellationToken cancellationToken)
    {
        return View("MoreInformationRequired");
    }
    
    [HttpGet]
    public IActionResult StudyDescription(CancellationToken cancellationToken)
    {
        return View("Details/StudyDescription");
    }

    [HttpPost]
    public async Task<IActionResult> StudyDescription(
        StudyRequestViewModel model,
        [FromServices] StudyDescriptionValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/StudyDescription", model);
        }

        return RedirectToAction(nameof(ResearchLocations));
    }
    
    [HttpGet]
    public IActionResult ResearchLocations(CancellationToken cancellationToken)
    {
        return View("Details/researchLocation");
    }

    [HttpPost]
    public async Task<IActionResult> ResearchLocations(
        StudyRequestViewModel model,
        [FromServices] ResearchLocationValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ResearchLocation", model);
        }

        return RedirectToAction(nameof(ResearchManager));
    }
    
    [HttpGet]
    public IActionResult ResearchManager(CancellationToken cancellationToken)
    {
        return View("Details/ResearchManager");
    }

    [HttpPost]
    public async Task<IActionResult> ResearchManager(
        StudyRequestViewModel model,
        [FromServices] ResearchManagerValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ResearchManager", model);
        }

        return RedirectToAction(nameof(ChiefInvestigator));
    }
    
    [HttpGet]
    public IActionResult ChiefInvestigator(CancellationToken cancellationToken)
    {
        return View("Details/ChiefInvestigator");
    }

    [HttpPost]
    public async Task<IActionResult> ChiefInvestigator(
        StudyRequestViewModel model,
        [FromServices] ChiefInvestigatorValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ChiefInvestigator", model);
        }

        return RedirectToAction(nameof(ChiefInvestigatorContact));
    }
    
    [HttpGet]
    public IActionResult ChiefInvestigatorContact(CancellationToken cancellationToken)
    {
        return View("Details/ChiefInvestigatorContact");
    }

    [HttpPost]
    public async Task<IActionResult> ChiefInvestigatorContact(
        StudyRequestViewModel model,
        [FromServices] ChiefInvestigatorContactValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ChiefInvestigatorContact", model);
        }

        if (!model.IsChiefInvestigatorMainContact!.Value)
        {
            return RedirectToAction(nameof(MainContact));
        }

        return RedirectToAction(nameof(SponsorOrganisation));
    }
    
    [HttpGet]
    public IActionResult MainContact(CancellationToken cancellationToken)
    {
        return View("Details/MainContact");
    }

    [HttpPost]
    public async Task<IActionResult> MainContact(
        StudyRequestViewModel model,
        [FromServices] MainContactValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/MainContact", model);
        }

        return RedirectToAction(nameof(SponsorOrganisation));
    }
    
    [HttpGet]
    public IActionResult SponsorOrganisation(CancellationToken cancellationToken)
    {
        return View("Sponsorship/SponsorOrganisation");
    }

    [HttpPost]
    public async Task<IActionResult> SponsorOrganisation(
        StudyRequestViewModel model,
        [FromServices] SponsorOrganisationValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Sponsorship/SponsorOrganisation", model);
        }

        return RedirectToAction(nameof(ParticipantDetails));
    }
    
    [HttpGet]
    public IActionResult ParticipantDetails(CancellationToken cancellationToken)
    {
        return View("ParticipantDetails/ParticipantDetails");
    }

    [HttpPost]
    public async Task<IActionResult> ParticipantDetails(
        StudyRequestViewModel model,
        [FromServices] ParticipantDetailsValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("ParticipantDetails/ParticipantDetails", model);
        }

        return RedirectToAction(nameof(Summary));
    }
    
    [HttpGet]
    public IActionResult Summary(CancellationToken cancellationToken)
    {
        var test = new StudyRequestViewModel()
        {
            HasEthicsApproval = true
        };
        
        return View(test);
    }

    [HttpPost]
    public async Task<IActionResult> Summary(
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        return RedirectToAction(nameof(Start));
    }
    
    [HttpGet]
    public IActionResult ApplicationSubmitted(CancellationToken cancellationToken)
    {
        return View();
    }

    private async Task<bool> ValidateAsync<TValidator, TModel>(
        TValidator validator,
        TModel model,
        CancellationToken cancellationToken)
        where TValidator : IValidator<TModel>
    {
        var result = await validator.ValidateAsync(
            model,
            cancellationToken);

        result.AddToModelState(ModelState);

        return result.IsValid;
    }
}