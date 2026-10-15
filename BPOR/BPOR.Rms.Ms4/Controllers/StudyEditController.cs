using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Repositories;
using BPOR.Rms.Ms4.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace BPOR.Rms.Ms4.Controllers;

/// <summary>
/// Defines additional study edit actions that are not part of the study creation flow
/// </summary>
[Route("study/{studyId:int}/edit/[action]")]
[Authorize(Roles = "Admin")]
public class StudyEditController(IStudyDraftRepository studyDraftRepository) : Controller
{
    private Study _study;
    private const string _statusUpdateConfirmationHeading = "Study status updated";

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var studyId = Convert.ToInt32(context.RouteData.Values["studyId"]);
        var study = await studyDraftRepository.GetStudyAsync(studyId, context.HttpContext.RequestAborted);
        if (study is null)
        {
            context.Result = NotFound();
        }
        else
        {
            _study = study;
            await base.OnActionExecutionAsync(context, next);
        }
    }
    
    [HttpGet]
    public IActionResult RecruitmentStartDate()
    {
        return View(GetViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> RecruitmentStartDate(
        StudyEditViewModel model,
        StudyEditViewModelValidator validator,
        CancellationToken cancellationToken)
    {
        if (validator.ValidateAndHasErrors(model, ModelState, i => i.RecruitmentStartDate))
        {
            return View(model);
        }

        _study.RecruitmentStartDate = model.RecruitmentStartDate.ToDateTime();
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);
        return StudyDetailsTab();
    }
    
    [HttpGet]
    public IActionResult RecruitmentTarget()
    {
        return View(GetViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> RecruitmentTarget(
        StudyEditViewModel model,
        StudyEditViewModelValidator validator,
        CancellationToken cancellationToken)
    {
        if (validator.ValidateAndHasErrors(model, ModelState, i => i.RecruitmentTarget))
        {
            return View(model);
        }

        _study.RecruitmentTarget = model.RecruitmentTarget;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);
        return StudyDetailsTab();
    }
    
    [HttpGet]
    public IActionResult IsRecruitingIdentifiableParticipants()
    {
        return View(GetViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> IsRecruitingIdentifiableParticipants(
        StudyEditViewModel model,
        StudyEditViewModelValidator validator,
        [FromServices] ParticipantDbContext dbContext,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model, i => i.IsRecruitingIdentifiableParticipants)
            .AddToModelState(ModelState);
        var hasCampaigns = await dbContext.Campaign.AnyAsync(i => i.FilterCriteria.StudyId == _study.Id, cancellationToken);
        if (hasCampaigns)
        {
            ModelState.AddModelError(nameof(model.IsRecruitingIdentifiableParticipants), 
                "This value cannot be modified as a campaign has already been created for this study.");
        }
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _study.IsRecruitingIdentifiableParticipants = model.IsRecruitingIdentifiableParticipants;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);
        return StudyDetailsTab();
    }
    
    [HttpGet]
    public IActionResult SubmissionOutcome()
    {
        return View(GetViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> SubmissionOutcome(
        StudyEditViewModel model,
        StudyEditViewModelValidator validator,
        CancellationToken cancellationToken)
    {
        if (validator.ValidateAndHasErrors(model, ModelState, i => i.SubmissionOutcome))
        {
            return View(model);
        }

        _study.SubmissionOutcomeId = model.SubmissionOutcome;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);
        return StudyFundingTab();
    }
    
    [HttpGet]
    public IActionResult InformationUrl()
    {
        return View(GetViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> InformationUrl(
        StudyEditViewModel model,
        StudyEditViewModelValidator validator,
        CancellationToken cancellationToken)
    {
        if (validator.ValidateAndHasErrors(model, ModelState, i => i.InformationUrl))
        {
            return View(model);
        }

        _study.InformationUrl = model.InformationUrl;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);
        return StudyDetailsTab();
    }
    
    [HttpGet]
    public IActionResult PreScreenerUrl()
    {
        return View(GetViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> PreScreenerUrl(
        StudyEditViewModel model,
        StudyEditViewModelValidator validator,
        CancellationToken cancellationToken)
    {
        if (validator.ValidateAndHasErrors(model, ModelState, i => i.PreScreenerUrl))
        {
            return View(model);
        }

        _study.PreScreenerUrl = model.PreScreenerUrl;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);
        return StudyDetailsTab();
    }
    
    [HttpGet]
    public IActionResult FundingCode()
    {
        return View(GetViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> FundingCode(
        StudyEditViewModel model,
        StudyEditViewModelValidator validator,
        CancellationToken cancellationToken)
    {
        if (validator.ValidateAndHasErrors(model, ModelState, i => i.FundingCode))
        {
            return View(model);
        }

        _study.FundingCode = model.FundingCode;
        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);
        return StudyFundingTab();
    }
    
    [HttpGet]
    public IActionResult Status()
    {
        ViewData.ShowBackLink();
        ViewData.SetBackLinkOverride(Url.Action("Details", "Study", new { Id = _study.Id }));
        
        return View(GetViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Status(
        StudyEditViewModel model,
        StudyEditViewModelValidator validator,
        CancellationToken cancellationToken)
    {
        validator.ValidateSpecificProperties(model, i => i.Status)
            .AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            ViewData.ShowBackLink();
            ViewData.SetBackLinkOverride(Url.Action("Details", "Study", new { Id = _study.Id }));
            
            return View(model);
        }
        
        if (model.Status == StudyStatusType.Withdrawn)
        {
            return RedirectToAction(nameof(WithdrawalReason), new { studyId = _study.Id });
        }

        if (model.Status == StudyStatusType.Rejected)
        {
            return RedirectToAction(nameof(RejectionReason), new { studyId = _study.Id });
        }

        _study.StudyStatusId = model.Status;
        AddStatusHistory(model.Status!.Value);

        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);
        
        return RedirectToConfirmationPage(_statusUpdateConfirmationHeading);
    }

    [HttpGet]
    public IActionResult WithdrawalReason()
    {
        ViewData.ShowBackLink();
        ViewData.SetBackLinkOverride(Url.Action("Status", "StudyEdit", new { StudyId = _study.Id }));
        
        return View(GetViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> WithdrawalReason(
        StudyEditViewModel model,
        StudyEditViewModelValidator validator,
        CancellationToken cancellationToken)
    {
        if (validator.ValidateAndHasErrors(
                model,
                ModelState,
                i => i.WithdrawnReasons,
                i => i.WithdrawnOtherReason))
        {
            ViewData.ShowBackLink();
            ViewData.SetBackLinkOverride(Url.Action("Status", "StudyEdit", new { StudyId = _study.Id }));
            
            return View(model);
        }

        _study.StudyStatusId = StudyStatusType.Withdrawn;

        var statusHistory = new StudyStatusHistory
        {
            StudyId = _study.Id,
            StudyStatusId = StudyStatusType.Withdrawn
        };

        foreach (var reason in model.WithdrawnReasons)
        {
            statusHistory.StudyStatusReasonHistories.Add(
                new StudyStatusReasonHistory
                {
                    WithdrawnReasonId = reason,
                    AdditionalReasonText = reason == WithdrawnReasonType.Other
                        ? model.WithdrawnOtherReason?.Trim()
                        : null
                });
        }

        _study.StudyStatusId = StudyStatusType.Withdrawn;
        _study.StudyStatusHistories.Add(statusHistory);

        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return RedirectToConfirmationPage(_statusUpdateConfirmationHeading);
    }
    
    [HttpGet]
    public IActionResult RejectionReason()
    {
        ViewData.ShowBackLink();
        ViewData.SetBackLinkOverride(Url.Action("Status", "StudyEdit", new { StudyId = _study.Id }));
        
        return View(GetViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> RejectionReason(
        StudyEditViewModel model,
        StudyEditViewModelValidator validator,
        CancellationToken cancellationToken)
    {
        if (validator.ValidateAndHasErrors(
                model,
                ModelState,
                i => i.RejectedReasons,
                i => i.RejectedMiscReason))
        {
            ViewData.ShowBackLink();
            ViewData.SetBackLinkOverride(Url.Action("Status", "StudyEdit", new { StudyId = _study.Id }));
            
            return View(model);
        }

        var statusHistory = new StudyStatusHistory
        {
            StudyId = _study.Id,
            StudyStatusId = StudyStatusType.Rejected
        };

        foreach (var reason in model.RejectedReasons)
        {
            statusHistory.StudyStatusReasonHistories.Add(
                new StudyStatusReasonHistory
                {
                    RejectedReasonId = reason,
                    AdditionalReasonText = reason == RejectedReasonType.Misc
                        ? model.WithdrawnOtherReason?.Trim()
                        : null
                });
        }

        _study.StudyStatusId = StudyStatusType.Rejected;
        _study.StudyStatusHistories.Add(statusHistory);

        await studyDraftRepository.SaveStudyAsync(_study, cancellationToken);

        return RedirectToConfirmationPage(_statusUpdateConfirmationHeading);
    }
    
    [HttpGet]
    public IActionResult Confirmation(string heading, string? message = null)
    {
        ViewData["ConfirmationHeading"] = heading;
        ViewData["ConfirmationMessage"] = message;

        return View(GetViewModel());
    }

    private IActionResult StudyDetailsTab() => RedirectToStudyDetailsPage("study-details");
    private IActionResult StudyFundingTab() => RedirectToStudyDetailsPage("funding-details");

    private IActionResult RedirectToStudyDetailsPage(string anchor)
    {
        var url = Url.Action("Details", "Study", new { Id = _study.Id }) + $"#{anchor}";
        return Redirect(url);
    }
    
    private IActionResult RedirectToConfirmationPage(string heading, string? message = null)
    {
        return RedirectToAction(nameof(Confirmation), new { studyId = _study.Id, heading,  message });
    }

    private StudyEditViewModel GetViewModel()
    {
        return new StudyEditViewModel
        {
            Id = _study.Id,
            RecruitmentStartDate = GovUkDate.FromDateTime(_study.RecruitmentStartDate),
            RecruitmentTarget = _study.RecruitmentTarget,
            IsRecruitingIdentifiableParticipants = _study.IsRecruitingIdentifiableParticipants,
            SubmissionOutcome = _study.SubmissionOutcomeId,
            InformationUrl = _study.InformationUrl,
            FundingCode = _study.FundingCode,
            PreScreenerUrl = _study.PreScreenerUrl,
            Status = _study.StudyStatusId,
            CurrentStatus =  _study.StudyStatusId
        };
    }
    
    private void AddStatusHistory(StudyStatusType status)
    {
        _study.StudyStatusHistories.Add(new StudyStatusHistory
        {
            StudyId = _study.Id,
            StudyStatusId = status
        });
    }
}