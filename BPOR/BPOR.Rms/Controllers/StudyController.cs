using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Ms4;
using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Repositories;
using BPOR.Rms.Startup;
using BPOR.Rms.Validators;
using BPOR.Rms.VolunteerInformation.Data;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.Infrastructure.AspNetCore.Validation;
using NIHR.Infrastructure.Paging;
using UserRole = BPOR.Domain.Enums.UserRole;

namespace BPOR.Rms.Controllers;

public class StudyController(
    ParticipantDbContext context,
    IPaginationService paginationService,
    ICurrentUserProvider<User> currentUserProvider,
    ILogger<StudyController> logger,
    IStudyDraftRepository studyDraftRepository
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string? searchTerm, bool hasBeenReset = false,
        CancellationToken token = default)
    {
        if (hasBeenReset)
        {
            TempData["HasBeenReset"] = true;
            return RedirectToAction(nameof(Index));
        }

        bool userHasResearcherRole = currentUserProvider.User.HasRole(Domain.Enums.UserRole.Researcher);

        var studiesQuery = context.Studies.AsQueryable();

        if (userHasResearcherRole)
        {
            string userEmail = currentUserProvider?.User?.ContactEmail ?? string.Empty;
            studiesQuery = studiesQuery.Where(s => s.EmailAddress == userEmail);
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.Trim();
            var isParsedInt = int.TryParse(searchTerm, out var searchInt);
            studiesQuery = studiesQuery.Where(s => (isParsedInt && s.Id == searchInt)
                                                   || (isParsedInt && s.CpmsId == searchInt)
                                                   // TODO investigate full text search
                                                   || s.StudyName.Contains(searchTerm));
        }

        var deferredStudiesPage = studiesQuery
            .AsStudyListModel()
            .OrderByDescending(s => s.Id)
            .DeferredPage(paginationService);

        var viewModel = new StudiesViewModel
        {
            Studies = await deferredStudiesPage.ValueAsync(token),
            HasSearched = Request.Query.ContainsKey(nameof(searchTerm)),
            SearchTerm = searchTerm ?? string.Empty,
        };

        return View(viewModel);
    }


    // GET: Study/Details/5
    public async Task<IActionResult> Details([FromServices] IVipRepository repository, int? id,
        CancellationToken cancellationToken)
    {
        if (id == null)
        {
            return NotFound();
        }

        var study = await context.Studies
            .Include(s => s.StudyStatus)
            .Where(s => s.Id == id)
            .AsStudyDetailsViewModel()
            .FirstOrDefaultAsync(cancellationToken);

        if (study == null)
        {
            logger.LogWarning("[HttpGet]Details called with non-existent study: {StudyId}", id);
            return NotFound();
        }
        
        var isAdmin = currentUserProvider.User.HasRole(UserRole.Admin);
        var isResearcher = currentUserProvider.User.HasRole(UserRole.Researcher);
        var isIdentifiable = study.Study.IsRecruitingIdentifiableParticipants;
        var updateRecruitmentAction = isIdentifiable ? "UpdateRecruited" : "UpdateAnonymousRecruited";
        var updateRecruitmentButtonText = isIdentifiable ? "Add enrolments" : "Update recruitment total";

        var canUpdateRecruitmentTotal = isIdentifiable
            ? study.HasCampaigns
            : isAdmin || (study.HasCampaigns && isResearcher);

        var vsiStatus = await repository.GetVipStatus(id.Value, cancellationToken);
        ViewData["VsiStatus"] = vsiStatus;
        
        study.ActionLinks = new();

        if (isAdmin)
        {
            switch (vsiStatus)
            {
                case null:
                    study.ActionLinks.Add(new ActionLink
                    {
                        Text = "Create volunteer study information page",
                        Url = Url.Action("Start", "VolunteerInformationStart", new { studyId = id.Value })
                    });
                    break;
                case VsiStatus.Draft:
                    study.ActionLinks.Add(new ActionLink
                    {
                        Text = "Resume volunteer study information page",
                        Url = Url.Action("Start", "VolunteerInformationStart", new { studyId = id.Value })
                    });
                    break;
                case VsiStatus.Active:
                    study.ActionLinks.Add(new ActionLink
                    {
                        Text = "Preview volunteer study information page",
                        Url = Url.Action("PreviewVip", "VolunteerInformationPage", new { studyId = id.Value }),
                        Target = HyperlinkTarget.Blank
                    });
                    break;
            }
        }

        if (canUpdateRecruitmentTotal)
        {
            study.ActionLinks.Add(new ActionLink
            {
                Text = updateRecruitmentButtonText,
                Url = Url.Action(updateRecruitmentAction, "Volunteer", new { studyId = id })
            });
        }

        if (isAdmin)
        {
            study.ActionLinks.AddRange(
            [
                new ActionLink
                {
                    Text = "Find volunteers",
                    Url = Url.Action("Index", "Filter", new { studyId = id })
                },
                new ActionLink
                {
                    Text = "Send an email",
                    Url = Url.Action("Index", "ResearcherEmail", new { studyId = id })
                }
            ]);
        }

        return View(study);
    }

    // GET: Study/Create
    public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
    {
        var study = new Study();
        var studyId = await studyDraftRepository.CreateDraftStudyAsync(study, cancellationToken);
        
        var isAdmin = currentUserProvider.User.HasRole(UserRole.Admin);

        var uri = Url.GetUrl(StudyRequestEditFlow.EthicsApproval, new StudyRequestEditContext
        {
            StudyId = studyId,
            FlowType = isAdmin ? StudyRequestEditFlowType.AdminCreate : StudyRequestEditFlowType.ResearcherCreate
        });

        return Redirect(uri);
    }

    // POST: Study/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind(@$"
            {nameof(StudyFormViewModel.Id)}, 
            {nameof(StudyFormViewModel.FullName)},
            {nameof(StudyFormViewModel.EmailAddress)},
            {nameof(StudyFormViewModel.StudyName)},
            {nameof(StudyFormViewModel.CpmsId)}, 
            {nameof(StudyFormViewModel.IsRecruitingIdentifiableParticipants)}, 
            {nameof(StudyFormViewModel.Step)},
            {nameof(StudyFormViewModel.AllowEditIsRecruitingIdentifiableParticipants)}")]
        StudyFormViewModel model, string action)
    {
        if (!model.AllowEditIsRecruitingIdentifiableParticipants)
        {
            // This should never happen, but we still need to guard against it.
            logger.LogWarning("[HttpPost]Create called with IsRecruitingIdentifiableParticipants set to false");
            return BadRequest("Model must allow editing of IsRecruitingIdentifiableParticipants");
        }
        
        if (action == "Next" || action == "Save")
        {
            if (model.Step == 1)
            {
                ModelState.AddValidationResult(ValidateStep(model, 1));
                
                if (ModelState.IsValid)
                {
                    model.GotoNextStep();
                }
            }
            else if (model.Step == 2)
            {
                // We need to re-validate step 1 since the data has been round-tripped to the browser
                // since it was first validated.
                ModelState.AddValidationResult(ValidateStep(model, 1));
                ModelState.AddValidationResult(ValidateStep(model, 2));

                if (ModelState.IsValid)
                {
                    var study = new Study
                    {
                        FullName = model.FullName,
                        EmailAddress = model.EmailAddress,
                        StudyName = model.StudyName,
                        CpmsId = model.CpmsId,
                        IsRecruitingIdentifiableParticipants = model.IsRecruitingIdentifiableParticipants ?? false
                    };

                    context.Add(study);
                    await context.SaveChangesAsync();

                    return RedirectToAction(nameof(AddStudySuccess), new AddStudySuccessViewModel
                    {
                        Id = study.Id,
                        StudyName = study.StudyName,
                    });
                }
            }
            else
            {
                logger.LogWarning("[HttpPost]Create called with step out of range: {Step}", model.Step);
                return BadRequest($"Step out of range: {model.Step}");
            }
        }
        else if (action == "Back")
        {
            // Clear validation when clicking back link
            // TODO: Needs to be more robust when there are other action names
            ModelState.Clear();
            model.Step--;

            if (model.Step < 1)
            {
                // Back link is exiting the process.
                // Return to a known entry point.
                // TODO: add referer as a query parameter
                // at the start of the journey so we can start
                // from any location and the back link
                // will exit correctly.
                return RedirectToAction("Index");
            }
        }
        else
        {
            logger.LogWarning("[HttpPost]Create called with action out of range: {Action}", action);
            return BadRequest($"Action out of range: {action}");
        }

        return View(model);
    }
    

    // success
    public IActionResult AddStudySuccess(AddStudySuccessViewModel viewModel)
    {
        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int id, int field)
    {
        var studyModel = await context.Studies
            .AsStudyFormViewModel()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (studyModel == null)
        {
            logger.LogWarning("[HttpGet]Edit called with non-existent study: {StudyId}", id);
            return NotFound();
        }

        studyModel.AllowEditIsRecruitingIdentifiableParticipants = !studyModel.HasCampaigns;
        studyModel.Step = field;
        return View(studyModel);
    }

    static ValidationResult ValidateStep(StudyFormViewModel model, int step )
    {
        StudyFormModelValidator validator = new();
        switch (step)
        {
            case 1:
                return validator.ValidateSpecificProperties(model, i => i.FullName, i => i.EmailAddress);
            case 2:
                return validator.ValidateSpecificProperties(model, i => i.StudyName, i => i.IsRecruitingIdentifiableParticipants, i=>i.CpmsId);
            case 3:
                return validator.ValidateSpecificProperties(model, i => i.InformationUrl);
            case 4:
                return validator.ValidateSpecificProperties(model, i => i.HasMultipleResearchLocations);
            case 5:
                return validator.ValidateSpecificProperties(model, i => i.SinglePersonResponsibleForRecruiting);
            case 6:
                return validator.ValidateSpecificProperties(model, i => i.PreScreenerUrl);
            default:
                throw new ArgumentOutOfRangeException(nameof(model.Step));
        }
    }

    // POST: Study/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind(@$"
            {nameof(StudyFormViewModel.FullName)},
            {nameof(StudyFormViewModel.EmailAddress)},
            {nameof(StudyFormViewModel.StudyName)},
            {nameof(StudyFormViewModel.CpmsId)}, 
            {nameof(StudyFormViewModel.Step)},
            {nameof(StudyFormViewModel.InformationUrl)},
            {nameof(StudyFormViewModel.AllowEditIsRecruitingIdentifiableParticipants)},
            {nameof(StudyFormViewModel.IsRecruitingIdentifiableParticipants)},
            {nameof(StudyFormViewModel.SinglePersonResponsibleForRecruiting)},
            {nameof(StudyFormViewModel.HasMultipleResearchLocations)},
            {nameof(StudyFormViewModel.PreScreenerUrl)}")]
        StudyFormViewModel model)
    {
        model.Id = id;

        if (model.Step is < 1 or > 6)
        {
            logger.LogWarning("[HttpPost]Edit called with step out of range: {Step}", model.Step);
            return BadRequest($"Step out of range: {model.Step}");
        }
        
        ModelState.AddValidationResult(ValidateStep(model, model.Step));

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var studyToUpdate = await context.Studies.FirstOrDefaultAsync(s => s.Id == id);

            if (studyToUpdate == null)
            {
                logger.LogWarning("[HttpPost]Edit called with non-existent study: {StudyId}", id);
                return NotFound();
            }
            
            switch (model.Step)
            {
                case 1:
                    studyToUpdate.FullName = model.FullName;
                    studyToUpdate.EmailAddress = model.EmailAddress;
                    break;
                case 2:
                    studyToUpdate.StudyName = model.StudyName;
                    studyToUpdate.CpmsId = model.CpmsId;
                    
                    if (model.AllowEditIsRecruitingIdentifiableParticipants)
                    {
                        var hasCampaigns = await context.FilterCriterias.AnyAsync(fc => fc.StudyId == studyToUpdate.Id && fc.Campaign.Any());
                        var isRecruitmentFlagChanging = model.IsRecruitingIdentifiableParticipants != studyToUpdate.IsRecruitingIdentifiableParticipants;

                        if (hasCampaigns && isRecruitmentFlagChanging)
                        {
                            ModelState.AddModelError(
                                nameof(model.IsRecruitingIdentifiableParticipants),
                                "The recruitment type cannot be updated once a campaign has been sent for a study.");
                            model.AllowEditIsRecruitingIdentifiableParticipants = false;
                            
                            return View(model);
                        }

                        if (!hasCampaigns)
                        {
                            studyToUpdate.IsRecruitingIdentifiableParticipants = (bool)model.IsRecruitingIdentifiableParticipants;
                        }
                    }

                    break;
                case 3:
                    studyToUpdate.InformationUrl = string.IsNullOrWhiteSpace(model.InformationUrl)
                        ? null
                        : model.InformationUrl.Trim();
                    break;
                case 4:
                    studyToUpdate.HasMultipleResearchLocations = model.HasMultipleResearchLocations;
                    break;
                case 5:
                    studyToUpdate.SinglePersonResponsibleForRecruiting = model.SinglePersonResponsibleForRecruiting;
                    break;
                case 6:
                    studyToUpdate.PreScreenerUrl = model.PreScreenerUrl;
                    break;
            }
                  
            studyToUpdate.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudyExists(id))
            {
                logger.LogWarning("[HttpPost]Edit called with non-existent study following concurrency exception: {StudyId}", id);
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        this.AddNotification(new NotificationBannerModel
        {
            IsSuccess = true,
            Title = "Study details updated",
            Body = $"{model.StudyName} has been successfully updated"
        });

        return RedirectToAction(nameof(Details), new { id });
        
    }

    private bool StudyExists(int id)
    {
        return context.Studies.Any(e => e.Id == id);
    }

    public async Task<IActionResult> SendIntroductoryEmail(int id)
    {
        var studyModel = await context.Studies
            .Where(s => s.Id == id)
            .AsStudyDetailsViewModel()
            .FirstOrDefaultAsync();

        if (studyModel == null)
        {
            logger.LogWarning("[HttpGet]Edit called with non-existent study: {StudyId}", id);
            return NotFound();
        }

        if (!studyModel.Study.IsEligibilityCriteriaComplete)
        {
            return BadRequest(ModelState);
        }

        return View(studyModel);
    }
    
    [HttpGet("[controller]/{studyId:int}/status/change")]
    public async Task<IActionResult> ChangeStatus(int studyId)
    {
        var model = await context.Studies
            .Include(x => x.StudyStatus)
            .FirstOrDefaultAsync(s => s.Id == studyId);

        if (model == null)
        {
            logger.LogWarning("[HttpGet]Edit called with non-existent study: {StudyId}", studyId);
            return NotFound();
        }

        var viewModel = new StudyStatusViewModel
        {
            StudyId = model.Id,
            StudyStatusCode = model.StudyStatus?.Code
        };

        await PopulateReferenceDataAsync(viewModel, model.StudyStatusId!.Value);

        return View("Status/Edit", viewModel);
    }

    [HttpPost("[controller]/{studyId:int}/status/change")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(int studyId, StudyStatusViewModel viewModel)
    {
        var study = await context.Studies
            .Include(x => x.StudyStatus)
            .FirstOrDefaultAsync(x => x.Id == studyId);

        if (study == null)
        {
            logger.LogWarning("[HttpPost] ChangeStatus called with non-existent study: {StudyId}", studyId);
            return NotFound();
        }

        if (viewModel.StudyStatusId == null)
        {
            ModelState.AddModelError(nameof(viewModel.StudyStatusId), "Select a study status");
        }
        else
        {
            var statusExists = await context.SysRefStudyStatus
                .AnyAsync(x =>
                    x.Id == viewModel.StudyStatusId.Value &&
                    x.Id != study.StudyStatusId &&
                    x.Id != StudyStatusType.Draft);

            if (!statusExists)
            {
                ModelState.AddModelError(nameof(viewModel.StudyStatusId), "Select a valid study status");
            }
        }

        var selectedWithdrawnReasons = viewModel.WithdrawnReasons?
            .Where(x => x.IsSelected)
            .ToList() ?? [];

        var selectedRejectedReasons = viewModel.RejectedReasons?
            .Where(x => x.IsSelected)
            .ToList() ?? [];

        switch (viewModel.StudyStatusId)
        {
            case StudyStatusType.Withdrawn:
            {
                if (selectedWithdrawnReasons.Count == 0)
                {
                    ModelState.AddModelError(nameof(viewModel.WithdrawnReasons), "Select at least one withdrawn reason");
                }

                var otherSelected = selectedWithdrawnReasons.Any(x => 
                    x.Id == (int)WithdrawnReasonType.Other);

                if (otherSelected && string.IsNullOrWhiteSpace(viewModel.WithdrawnOtherReason))
                {
                    ModelState.AddModelError(nameof(viewModel.WithdrawnOtherReason), "Enter details for Other");
                }

                var validWithdrawnReasonIds = context.SysRefWithdrawnReason
                    .Select(x => (int)x.Id)
                    .ToHashSet();

                if (selectedWithdrawnReasons.Any(x => !validWithdrawnReasonIds.Contains(x.Id)))
                {
                    ModelState.AddModelError(
                        nameof(viewModel.WithdrawnReasons),
                        "One or more selected withdrawn reasons are invalid");
                }

                break;
            }
            case StudyStatusType.Rejected:
            {
                if (selectedRejectedReasons.Count == 0)
                {
                    ModelState.AddModelError(
                        nameof(viewModel.RejectedReasons),
                        "Select at least one rejected reason");
                }

                var miscSelected = selectedRejectedReasons.Any(x => 
                    x.Id == (int)RejectedReasonType.Misc);

                if (miscSelected && string.IsNullOrWhiteSpace(viewModel.RejectedMiscReason))
                {
                    ModelState.AddModelError(
                        nameof(viewModel.RejectedMiscReason),
                        "Enter details for Misc");
                }

                var validRejectedReasonIds = context.SysRefRejectedReason
                    .Select(x => (int)x.Id)
                    .ToHashSet();

                if (selectedRejectedReasons.Any(x => !validRejectedReasonIds.Contains(x.Id)))
                {
                    ModelState.AddModelError(
                        nameof(viewModel.RejectedReasons),
                        "One or more selected rejected reasons are invalid");
                }

                break;
            }
        }

        if (!ModelState.IsValid)
        {
            viewModel.StudyStatusCode = study.StudyStatus?.Code;

            await PopulateReferenceDataAsync(viewModel, study.StudyStatusId!.Value);

            return View("Status/Edit", viewModel);
        }

        var selectedStatus = viewModel.StudyStatusId!.Value;

        study.StudyStatusId = selectedStatus;

        var statusHistory = new StudyStatusHistory
        {
            StudyId = study.Id,
            StudyStatusId = selectedStatus
        };

        context.StudyStatusHistory.Add(statusHistory);

        switch (selectedStatus)
        {
            case StudyStatusType.Withdrawn:
            {
                foreach (var reason in selectedWithdrawnReasons)
                {
                    context.StudyStatusReasonHistory.Add(
                        new StudyStatusReasonHistory
                        {
                            StudyStatusHistory = statusHistory,
                            WithdrawnReasonId = (WithdrawnReasonType)reason.Id,
                            AdditionalReasonText =
                                reason.Id == (int)WithdrawnReasonType.Other
                                    ? viewModel.WithdrawnOtherReason?.Trim()
                                    : null
                        });
                }

                break;
            }
            case StudyStatusType.Rejected:
            {
                foreach (var reason in selectedRejectedReasons)
                {
                    context.StudyStatusReasonHistory.Add(
                        new StudyStatusReasonHistory
                        {
                            StudyStatusHistory = statusHistory,
                            RejectedReasonId = (RejectedReasonType)reason.Id,
                            AdditionalReasonText =
                                reason.Id == (int)RejectedReasonType.Misc
                                    ? viewModel.RejectedMiscReason?.Trim()
                                    : null
                        });
                }

                break;
            }
        }

        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id = study.Id });
    }

    private async Task PopulateReferenceDataAsync(StudyStatusViewModel viewModel, StudyStatusType currentStatus)
    {
        viewModel.AvailableStatuses = await context.SysRefStudyStatus
            .Where(x => x.Id != currentStatus &&
                        x.Id != StudyStatusType.Draft)
            .ToListAsync();

        viewModel.WithdrawnReasons = await context.SysRefWithdrawnReason
            .Select(x => new StudyStatusReasonViewModel
            {
                Id = (int)x.Id,
                Code = x.Code,
                Description = x.Description
            })
            .ToListAsync();

        viewModel.RejectedReasons = await context.SysRefRejectedReason
            .Select(x => new StudyStatusReasonViewModel
            {
                Id = (int)x.Id,
                Code = x.Code,
                Description = x.Description
            })
            .ToListAsync();
    }
}