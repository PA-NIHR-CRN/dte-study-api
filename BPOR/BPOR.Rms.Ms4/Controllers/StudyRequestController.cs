using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Models.Details;
using BPOR.Rms.Ms4.Models.Overview;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.Infrastructure.AspNetCore;
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
        var result = await validator.ValidateAsync(model, cancellationToken);
        
        result.AddToModelState(ModelState);
        
        if (!ModelState.IsValid)
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
        EthicsApprovalViewModel model,
        [FromServices] IValidator<EthicsApprovalViewModel> validator,
        CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(model, cancellationToken);
        
        result.AddToModelState(ModelState);
        
        if (!ModelState.IsValid)
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
        InclusionInRdnPortfolioViewModel model,
        [FromServices] IValidator<InclusionInRdnPortfolioViewModel> validator,
        CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(model, cancellationToken);
        
        result.AddToModelState(ModelState);
        
        if (!ModelState.IsValid)
        {
            return View("Overview/InclusionInRdnPortfolio", model);
        }

        return RedirectToAction(nameof(NihrFunding));
    }
    
    [HttpGet]
    public ActionResult NihrFunding(CancellationToken cancellationToken)
    {
        return View("Overview/NihrFunding");
    }

    [HttpPost]
    public async Task<IActionResult> NihrFunding(
        NihrFundingViewModel model,
        [FromServices] IValidator<NihrFundingViewModel> validator,
        CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(model, cancellationToken);
        
        result.AddToModelState(ModelState);
        
        if (!ModelState.IsValid)
        {
            return View("Overview/NihrFunding", model);
        }

        if (model.NihrFundingStatus == NihrFundingStatus.NoNihrFunding)
        {
            return RedirectToAction(nameof(MoreInformationRequired));
        }

        return RedirectToAction(nameof(FinishRecruiting));
    }
    
    [HttpGet]
    public IActionResult FinishRecruiting(CancellationToken cancellationToken)
    {
        return View("Overview/FinishRecruiting");
    }

    [HttpPost]
    public async Task<IActionResult> FinishRecruiting(
        FinishRecruitingViewModel model,
        [FromServices] IValidator<FinishRecruitingViewModel> validator,
        CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(model, cancellationToken);
        
        result.AddToModelState(ModelState);
        
        if (!ModelState.IsValid)
        {
            return View("Overview/FinishRecruiting", model);
        }

        return RedirectToAction(nameof(StudyDescription));
    }
    
    [HttpGet]
    public IActionResult MoreInformationRequired(CancellationToken cancellationToken)
    {
        TempData.Put("Notification", new NotificationBannerModel
        {
            Title = "Important",
            Heading = "More information needed",
            IsSuccess = true
        });
        
        return View("Overview/MoreInformationRequired");
    }
    
    [HttpGet]
    public IActionResult StudyDescription(CancellationToken cancellationToken)
    {
        return View("Details/StudyDescription");
    }

    [HttpPost]
    public async Task<IActionResult> StudyDescription(
        StudyDescriptionViewModel model,
        [FromServices] IValidator<StudyDescriptionViewModel> validator,
        CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(model, cancellationToken);
        
        result.AddToModelState(ModelState);
        
        if (!ModelState.IsValid)
        {
            return View("Overview/FinishRecruiting", model);
        }

        return RedirectToAction(nameof(Start));
    }
}