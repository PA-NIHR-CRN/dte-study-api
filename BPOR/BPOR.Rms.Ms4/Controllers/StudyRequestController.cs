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
        else if (study.StudyStatusId is not StudyStatusType.Draft && User.HasClaim(i => i is { Type: ClaimTypes.Role, Value: "Admin" }))
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
    public IActionResult InclusionInRdnPortfolio(StudyEditContext context)
    {
        return View("Overview/InclusionInRdnPortfolio", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> InclusionInRdnPortfolio(
        StudyEditContext context,
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
    public IActionResult NihrFunding(StudyEditContext context)
    {
        return View("Overview/NihrFunding", context, MapViewModel(_study));
    }

    [HttpPost]
    public async Task<IActionResult> NihrFunding(
        StudyEditContext context,
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
    public IActionResult FinishRecruiting(StudyEditContext context)
    {
        return View("Overview/FinishRecruiting", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> FinishRecruiting(
        StudyEditContext context,
        StudyRequestViewModel model,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.FinishRecruitingDay, 
            i => i.FinishRecruitingMonth,
            i => i.FinishRecruitingYear).AddToModelState(ModelState);
        if (!ModelState.IsValid)
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
        return View("MoreInformationRequired", context, MapViewModel(_study));
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
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.StudyTitle, 
            i => i.StudyDescription).AddToModelState(ModelState);
        if (!ModelState.IsValid)
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
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.HasMultipleResearchLocations).AddToModelState(ModelState);
        if (!ModelState.IsValid)
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
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.SinglePersonResponsibleForRecruiting).AddToModelState(ModelState);
        if (!ModelState.IsValid)
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
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.ChiefInvestigatorEmail, 
            i => i.ChiefInvestigatorName).AddToModelState(ModelState);
        if (!ModelState.IsValid)
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
        [FromServices] ChiefInvestigatorContactValidator ciContactValidator,
        CancellationToken cancellationToken)
    {
        (await ciContactValidator.ValidateAsync(model, cancellationToken)).AddToModelState(ModelState);
        if (!ModelState.IsValid)
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

        // The answer to this question is not persisted in the model, so copy it into the model for flow control purposes.
        return GetNextAction(context, i => i.IsChiefInvestigatorMainContact = model.IsChiefInvestigatorMainContact == true);
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
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.MainContactEmail, 
            i => i.MainContactName,
            i => i.MainContactRole).AddToModelState(ModelState);
        if (!ModelState.IsValid)
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
    public IActionResult Section2Check(StudyEditContext context)
    {
        return View("Details/Section2Check", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> Section2Check(
        StudyEditContext context,
        CancellationToken cancellationToken)
    {
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
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.SponsorName).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            return View("Sponsorship/SponsorOrganisation", model);
        }

        _study.Sponsors = model.SponsorName;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return GetNextAction(context);
    }
    
    [HttpGet]
    public IActionResult Section3Check(StudyEditContext context)
    {
        return View("Sponsorship/Section3Check", context, MapViewModel(_study));
    }
    
    [HttpPost]
    public async Task<IActionResult> Section3Check(
        StudyEditContext context,
        CancellationToken cancellationToken)
    {
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
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model,
            i => i.InclusionCriteria).AddToModelState(ModelState);
        if (!ModelState.IsValid)
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
    public async Task<IActionResult> Summary(StudyEditContext context, CancellationToken cancellationToken)
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
    
    
    private IActionResult GetNextAction(StudyEditContext context, Action<StudyRequestViewModel>? modifyModel = null)
    {
        var result = GetRelatedUrl(context, FlowAction.Next, modifyModel);
        return Redirect(result);
    }

    private string? GetRelatedUrl(StudyEditContext context, FlowAction action, Action<StudyRequestViewModel>? modifyModel = null)
    {
        var model = MapViewModel(_study);
        modifyModel?.Invoke(model);
        var nextAction = StudyRequestFlow.Graph.ApplyTransition(mvcFlowHelper.CurrentActionKey, context, model, action);

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

        ViewData["Progress"] = StudyRequestFlow.Graph.CalculateBestCaseProgress(context,
            StudyRequestFlow.EthicsApproval, StudyRequestFlow.Summary, mvcFlowHelper.CurrentActionKey);
        ViewData["StudyEditContext"] = context;
        return View(viewName, model);
    }

}