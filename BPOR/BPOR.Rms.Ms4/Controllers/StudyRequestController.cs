using System.Security.Claims;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Repositories;
using BPOR.Rms.Ms4.Validators;
using CpmsCore.Web.Authorization;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.Ms4.Controllers;

[Authorize(AuthenticationSchemes = $"{AccessTokenAuthenticationOptions.AuthenticationScheme}, {CookieAuthenticationDefaults.AuthenticationScheme}")]
[AuthorizeAnyPolicy(PolicyNames.IsResearcherCreatingStudy, PolicyNames.IsAdmin)]
[Route("[controller]/{studyId:int}/[action]")]
public class StudyRequestController(
    IStudyDraftRepository studyDraftRepository,
    IUrlAccessTokenService urlAccessTokenService,
    StudyRequestViewModelValidator validator,
    IMvcFlowHelper mvcFlowHelper)
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
        else if (study.StudyStatusId is not StudyStatusType.Draft && !User.HasClaim(i => i is { Type: ClaimTypes.Role, Value: "Admin" }))
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
    public IActionResult EthicsApproval(StudyRequestEditContext context)
    {
        return View("Overview/EthicsApproval", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> EthicsApproval(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model, i => i.HasEthicsApproval).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("Overview/EthicsApproval", context, model);
        }

        _study.HasEthicsApproval = model.HasEthicsApproval;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult InclusionInRdnPortfolio(StudyRequestEditContext context)
    {
        return View("Overview/InclusionInRdnPortfolio", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> InclusionInRdnPortfolio(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.InclusionInRdnPortfolioStatus, 
            i => i.CpmsId).AddToModelState(ModelState);
        if (!ModelState.IsValid)
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
    public IActionResult NihrFunding(StudyRequestEditContext context)
    {
        return View("Overview/NihrFunding", context, MapViewModel(_study));
    }

    [HttpPost]
    public async Task<IActionResult> NihrFunding(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.NihrFundingStatus).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("Overview/NihrFunding", context, model);
        }

        _study.HasNihrFunding = model.NihrFundingStatus;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);
        
        return GetNextAction(context);
    }

    [HttpGet]
    public IActionResult FinishRecruiting(StudyRequestEditContext context)
    {
        return View("Overview/FinishRecruiting", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> FinishRecruiting(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.FinishRecruiting).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("Overview/FinishRecruiting", context, model);
        }

        _study.RecruitmentEndDate = model.FinishRecruiting.ToDateTime();

        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult MoreInformationRequired(StudyRequestEditContext context)
    {
        return View("MoreInformationRequired", context, MapViewModel(_study));
    }
    
    [HttpGet]
    public IActionResult StudyDescription(StudyRequestEditContext context)
    {
        return View("Details/StudyDescription", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> StudyDescription(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.StudyTitle, 
            i => i.StudyDescription).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("Details/StudyDescription", context, model);
        }
        
        _study.StudyName = model.StudyTitle;
        _study.Description = model.StudyDescription;

        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult ResearchLocations(StudyRequestEditContext context)
    {
        return View("Details/ResearchLocation", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> ResearchLocations(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.HasMultipleResearchLocations).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("Details/ResearchLocation", context, model);
        }
        
        _study.HasMultipleResearchLocations = model.HasMultipleResearchLocations;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult ResearchManager(StudyRequestEditContext context)
    {
        return View("Details/ResearchManager", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> ResearchManager(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.SinglePersonResponsibleForRecruiting).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("Details/ResearchManager", context, model);
        }

        _study.SinglePersonResponsibleForRecruiting = model.SinglePersonResponsibleForRecruiting;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult ChiefInvestigator(StudyRequestEditContext context)
    {
        return View("Details/ChiefInvestigator", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> ChiefInvestigator(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.ChiefInvestigatorEmail, 
            i => i.ChiefInvestigatorName).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("Details/ChiefInvestigator", context, model);
        }

        _study.ChiefInvestigatorEmail = model.ChiefInvestigatorEmail;
        _study.ChiefInvestigator = model.ChiefInvestigatorName;

        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult ChiefInvestigatorContact(StudyRequestEditContext context)
    {
        return View("Details/ChiefInvestigatorContact", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> ChiefInvestigatorContact(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        [FromServices] ChiefInvestigatorContactValidator ciContactValidator,
        CancellationToken cancellationToken)
    {
        (await ciContactValidator.ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("Details/ChiefInvestigatorContact", context, model);
        }

        if (model.IsChiefInvestigatorMainContact == true)
        {
            _study.FullName = _study.ChiefInvestigator;
            _study.EmailAddress = _study.ChiefInvestigatorEmail;
            _study.MainContactRole = "Chief Investigator";
            await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);
        }

        // The answer to this question is not persisted in the model, so copy it into the model for flow control purposes.
        return GetNextAction(context, i => i.IsChiefInvestigatorMainContact = model.IsChiefInvestigatorMainContact == true);
    }
    
    [HttpGet]
    public IActionResult MainContact(StudyRequestEditContext context)
    {
        return View("Details/MainContact", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> MainContact(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.MainContactEmail, 
            i => i.MainContactName,
            i => i.MainContactRole).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("Details/MainContact", context, model);
        }
        
        _study.FullName = model.MainContactName;
        _study.EmailAddress = model.MainContactEmail;
        _study.MainContactRole = model.MainContactRole;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult Section2Check(StudyRequestEditContext context)
    {
        return View("Details/Section2Check", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> Section2Check(
        StudyRequestEditContext context,
        CancellationToken cancellationToken)
    {
        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult SponsorOrganisation(StudyRequestEditContext context)
    {
        return View("Sponsorship/SponsorOrganisation", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> SponsorOrganisation(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.SponsorName).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("Sponsorship/SponsorOrganisation", context, model);
        }

        _study.Sponsors = model.SponsorName;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult Section3Check(StudyRequestEditContext context)
    {
        return View("Sponsorship/Section3Check", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> Section3Check(
        StudyRequestEditContext context,
        CancellationToken cancellationToken)
    {
        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult ParticipantDetails(StudyRequestEditContext context)
    {
        return View("ParticipantDetails/ParticipantDetails", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> ParticipantDetails(
        StudyRequestEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.InclusionCriteria).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("ParticipantDetails/ParticipantDetails", context, model);
        }

        _study.InclusionCriteria = model.InclusionCriteria;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }

    [HttpGet]
    public IActionResult Summary(StudyRequestEditContext context)
    {
        return View("summary", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> Summary(StudyRequestEditContext context, CancellationToken cancellationToken)
    {
        var model = MapViewModel(_study);
        (await validator.ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("summary", context, model);
        }
        
        await studyDraftRepository.SubmitStudyAsync(context.StudyId, cancellationToken);
        return RedirectToAction("ApplicationSubmitted", "StudyRequestStart", context);
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
            FinishRecruiting = GovUkDate.FromDateTime(study.RecruitmentEndDate),
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
    
    
    private IActionResult GetNextAction(StudyRequestEditContext context, Action<StudyRequestViewModel>? modifyModel = null)
    {
        var result = GetRelatedUrl(context, MvcFlowAction.Next, modifyModel);
        return Redirect(result);
    }

    private string? GetRelatedUrl(StudyRequestEditContext context, MvcFlowAction action, Action<StudyRequestViewModel>? modifyModel = null)
    {
        var model = MapViewModel(_study);
        modifyModel?.Invoke(model);
        var nextAction = StudyRequestEditFlow.Graph.ApplyTransition(mvcFlowHelper.CurrentActionKey, context, model, action);

        if (nextAction == null)
        {
            return null;
        }

        string? result = Url.GetUrl(nextAction);
        if (result == null)
        {
            throw new Exception($"{nextAction.NodeKey} could not be mapped to a URL");
        }
        result = urlAccessTokenService.AddCurrentAccessToken(result);
        return result;
    }
    
    private IActionResult View([AspMvcView]string viewName, StudyRequestEditContext context, StudyRequestViewModel model)
    {
        var backUrl = GetRelatedUrl(context, MvcFlowAction.Back);
        if (string.IsNullOrWhiteSpace(backUrl))
        {
            ViewData.ShowBackLink(false);
        }
        else
        {
            ViewData.ShowBackLink();
            ViewData.SetBackLinkOverride(backUrl);
        }

        ViewData["Progress"] = StudyRequestEditFlow.Graph.CalculateBestCaseProgress(context,
            StudyRequestEditFlow.EthicsApproval, StudyRequestEditFlow.Summary, mvcFlowHelper.CurrentActionKey);
        ViewData["StudyEditContext"] = context;
        return View(viewName, model);
    }

}