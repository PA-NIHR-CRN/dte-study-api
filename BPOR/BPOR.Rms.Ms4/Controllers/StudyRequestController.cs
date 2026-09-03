using System.Security.Claims;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Repositories;
using BPOR.Rms.Ms4.Validators.Details;
using BPOR.Rms.Ms4.Validators.Overview;
using BPOR.Rms.Ms4.Validators.ParticipantDetails;
using BPOR.Rms.Ms4.Validators.Sponsorship;
using CpmsCore.Web.Authorization;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

namespace BPOR.Rms.Ms4.Controllers;

[Authorize(AuthenticationSchemes = $"{AccessTokenAuthenticationOptions.AuthenticationScheme}, {CookieAuthenticationDefaults.AuthenticationScheme}")]
[AuthorizeAnyPolicy(PolicyNames.IsResearcherCreatingStudy, PolicyNames.IsAdmin)]
[Route("[controller]/{studyId:int}/[action]")]
public class StudyRequestController(IStudyDraftRepository studyDraftRepository, IUrlAccessTokenService urlAccessTokenService)
    : Controller
{
    private Study _study = null!; // Initialised in OnActionExecutionAsync
    
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var studyId = Convert.ToInt32(context.RouteData.Values["studyId"]);
        var study = await studyDraftRepository.GetStudyAsync(studyId, context.HttpContext.RequestAborted);
        if (study is null)
        {
            context.Result = NotFound();
        }
        else if (study.StudyStatusId is not StudyStatusType.Draft && User.HasClaim(i => i.Type == ClaimTypes.Role && i.Value == "Admin"))
        {
            context.Result = Forbid();
        }
        else
        {
            _study = study;
            await base.OnActionExecutionAsync(context, next);
        }
    }

    [HttpGet]
    public IActionResult EthicsApproval(StudyEditContext context)
    {
        return View("Overview/EthicsApproval", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> EthicsApproval(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] EthicsApprovalValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/EthicsApproval", context, model);
        }

        _study.HasEthicsApproval = model.HasEthicsApproval;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult InclusionInRdnPortfolio(StudyEditContext context)
    {
        return View("Overview/InclusionInRdnPortfolio", context, MapViewModel(_study));
    }

    private IActionResult View([AspMvcView]string viewName, StudyEditContext context, StudyRequestViewModel model)
    {
        var backUrl = GetRelatedUrl(context, FlowAction.Back);
        if (string.IsNullOrWhiteSpace(backUrl))
        {
            ViewData.ShowBackLink(false);
        }
        else
        {
            ViewData.ShowBackLink();
            ViewData.SetBackLinkOverride(backUrl);
        }
        ViewData["StudyEditContext"] = context;
        return View(viewName, model);
    }

    [HttpPost]
    public async Task<IActionResult> InclusionInRdnPortfolio(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] InclusionInRdnPortfolioValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/InclusionInRdnPortfolio", context, model);
        }

        _study.SubmittedId = model.InclusionInRdnPortfolioStatus;
        _study.CpmsId = model.InclusionInRdnPortfolioStatus == SubmittedType.Yes ? model.CpmsId : null;

        if (model.InclusionInRdnPortfolioStatus == SubmittedType.Yes)
        {
            _study.NihrFundingStatus = null;
        }

        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult NihrFunding(StudyEditContext context)
    {
        return View("Overview/NihrFunding", context, MapViewModel(_study));
    }

    [HttpPost]
    public async Task<IActionResult> NihrFunding(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] NihrFundingValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/NihrFunding", context, model);
        }

        _study.HasNihrFunding = model.NihrFundingStatus;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }

    [HttpGet]
    public IActionResult FinishRecruiting(StudyEditContext context)
    {
        return View("Overview/FinishRecruiting", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> FinishRecruiting(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] FinishRecruitingValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/FinishRecruiting", context, model);
        }

        _study.RecruitmentEndDate = new DateTime(
            model.FinishRecruitingYear!.Value,
            model.FinishRecruitingMonth!.Value,
            model.FinishRecruitingDay!.Value);

        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult MoreInformationRequired(StudyEditContext context)
    {
        return View("MoreInformationRequired");
    }
    
    [HttpGet]
    public IActionResult StudyDescription(StudyEditContext context)
    {
        return View("Details/StudyDescription", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> StudyDescription(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] StudyDescriptionValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/StudyDescription", model);
        }
        
        _study.StudyName = model.StudyTitle;
        _study.Description = model.StudyDescription;

        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult ResearchLocations(StudyEditContext context)
    {
        return View("Details/ResearchLocation", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> ResearchLocations(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] ResearchLocationValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ResearchLocation", model);
        }
        
        _study.HasMultipleResearchLocations = model.HasMultipleResearchLocations;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult ResearchManager(StudyEditContext context)
    {
        return View("Details/ResearchManager", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> ResearchManager(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] ResearchManagerValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ResearchManager", model);
        }

        _study.SinglePersonResponsibleForRecruiting = model.SinglePersonResponsibleForRecruiting;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult ChiefInvestigator(StudyEditContext context)
    {
        return View("Details/ChiefInvestigator", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> ChiefInvestigator(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] ChiefInvestigatorValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ChiefInvestigator", model);
        }

        _study.ChiefInvestigatorEmail = model.ChiefInvestigatorEmail;
        _study.ChiefInvestigator = model.ChiefInvestigatorName;

        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult ChiefInvestigatorContact(StudyEditContext context)
    {
        return View("Details/ChiefInvestigatorContact", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> ChiefInvestigatorContact(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] ChiefInvestigatorContactValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ChiefInvestigatorContact", model);
        }

        if (model.IsChiefInvestigatorMainContact == true)
        {
            _study.FullName = null;
            _study.EmailAddress = null;
            _study.MainContactRole = null;
            await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);
        }

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult MainContact(StudyEditContext context)
    {
        return View("Details/MainContact", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> MainContact(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] MainContactValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/MainContact", model);
        }
        
        _study.FullName = model.MainContactName;
        _study.EmailAddress = model.MainContactEmail;
        _study.MainContactRole = model.MainContactRole;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult SponsorOrganisation(StudyEditContext context)
    {
        return View("Sponsorship/SponsorOrganisation", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> SponsorOrganisation(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] SponsorOrganisationValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Sponsorship/SponsorOrganisation", model);
        }

        _study.Sponsors = model.SponsorName;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult ParticipantDetails(StudyEditContext context)
    {
        return View("ParticipantDetails/ParticipantDetails", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> ParticipantDetails(
        StudyEditContext context,
        StudyRequestViewModel model,
        [FromServices] ParticipantDetailsValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("ParticipantDetails/ParticipantDetails", model);
        }

        _study.InclusionCriteria = model.InclusionCriteria;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }

    [HttpGet]
    public IActionResult Summary(StudyEditContext context)
    {
        return View("summary", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> SubmitStudy(StudyEditContext context, CancellationToken cancellationToken)
    {
        await studyDraftRepository.SubmitStudyAsync(context.StudyId, cancellationToken);

        return RedirectToAction(nameof(ApplicationSubmitted), context.StudyId);
    }
    
    [HttpGet]
    public IActionResult ApplicationSubmitted(int studyId)
    {
        return View();
    }

    private static StudyRequestViewModel MapViewModel(Study study)
    {
        return new StudyRequestViewModel
        {
            StudyId = study.Id,
            HasEthicsApproval = study.HasEthicsApproval,
            InclusionInRdnPortfolioStatusDisplay = study.Submitted?.Code,
            InclusionInRdnPortfolioStatus = study.SubmittedId,
            CpmsId = study.CpmsId,
            NihrFundingStatusDisplay = study.NihrFundingStatus?.Code,
            NihrFundingStatus = study.HasNihrFunding,
            RecruitmentEndDate = study.RecruitmentEndDate,
            StudyTitle = study.StudyName,
            StudyDescription = study.Description,
            HasMultipleResearchLocations = study.HasMultipleResearchLocations,
            SinglePersonResponsibleForRecruiting = study.SinglePersonResponsibleForRecruiting,
            ChiefInvestigatorName = study.ChiefInvestigator,
            ChiefInvestigatorEmail = study.ChiefInvestigatorEmail,
            MainContactName = study.FullName,
            MainContactEmail = study.EmailAddress,
            MainContactRole = study.MainContactRole,
            SponsorName = study.Sponsors,
            InclusionCriteria = study.InclusionCriteria
        };
    }

    private async Task<bool> ValidateAsync<TModel>(
        IValidator<TModel> validator,
        TModel model,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(model, cancellationToken);
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return validationResult.IsValid;
    }
    
    private IActionResult GetNextAction(StudyEditContext context)
    {
        var result = GetRelatedUrl(context, FlowAction.Next);
        return Redirect(result);
    }

    private string? GetRelatedUrl(StudyEditContext context, FlowAction action)
    {
        var currentActionKey = new MvcActionKey(
            RouteData.Values["controller"]!.ToString()!, RouteData.Values["action"]!.ToString()!);
        var model = MapViewModel(_study);
        var nextAction = StudyRequestFlow.Graph.ApplyTransition(currentActionKey, context, model, action);

        if (nextAction == null)
        {
            return null;
        }

        string result = StudyRequestFlow.GetUri(Url, nextAction.NodeKey, nextAction.Context);
        result = urlAccessTokenService.AddCurrentAccessToken(result);
        return result;
    }
}