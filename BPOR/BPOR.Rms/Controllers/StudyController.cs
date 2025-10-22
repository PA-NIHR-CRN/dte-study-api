using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Startup;
using BPOR.Rms.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.Infrastructure.Paging;

namespace BPOR.Rms.Controllers;

public class StudyController(
    ParticipantDbContext context,
    IPaginationService paginationService,
    ICurrentUserProvider<User> currentUserProvider,
    ILogger<StudyController> logger
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
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var study = await context.Studies
            .Where(s => s.Id == id)
            .AsStudyDetailsViewModel()
            .FirstOrDefaultAsync();

        if (study == null)
        {
            logger.LogWarning("[HttpGet]Details called with non-existent study: {StudyId}", id);
            return NotFound();
        }

        return View(study);
    }

    // GET: Study/Create
    public IActionResult Create()
    {
        var model = new StudyFormViewModel(){AllowEditIsRecruitingIdentifiableParticipants = true};
        return View(model);
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
            {nameof(StudyFormViewModel.IsRecruitingIdentifiableParticipants)}")]
        StudyFormViewModel model)
    {
        model.Id = id;

        if (model.Step is < 1 or > 3)
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
            var studyToUpdate = await context.Studies
                .Include(fc => fc.FilterCriterias)
                .ThenInclude(c => c.Campaign)
                .FirstOrDefaultAsync(s => s.Id == id);

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
                    
                    var hasCampaigns = studyToUpdate.FilterCriterias.Any(fc => fc.Campaign.Any());
                    
                    if (model.AllowEditIsRecruitingIdentifiableParticipants)
                    {
                        var isRecruitmentFlagChanging = model.IsRecruitingIdentifiableParticipants != studyToUpdate.IsRecruitingIdentifiableParticipants;

                        if (hasCampaigns && isRecruitmentFlagChanging)
                        {
                            ModelState.AddModelError(
                                nameof(model.IsRecruitingIdentifiableParticipants),
                                $"{model.StudyName} has been modified by another user - please check the study details and try again");
                            
                            model.StudyName = studyToUpdate.StudyName;
                            model.CpmsId = studyToUpdate.CpmsId;
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
}