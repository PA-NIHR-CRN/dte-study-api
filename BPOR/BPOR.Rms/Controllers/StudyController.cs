using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.Infrastructure.Paging;

namespace BPOR.Rms.Controllers;
public class StudyController(ParticipantDbContext context, IPaginationService paginationService, ICurrentUserProvider<User> currentUserProvider
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string? searchTerm, bool hasBeenReset = false, CancellationToken token = default)
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
            return NotFound();
        }

        return View(study);
    }

    // GET: Study/Create
    public IActionResult Create()
    {
        var model = new StudyFormViewModel();
        return View(model);
    }

    // POST: Study/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,FullName,EmailAddress,StudyName,CpmsId,IsRecruitingIdentifiableParticipants,Step")]
        StudyFormViewModel model, string action)
    {
        if (action == "Next" || action == "Save")
        {
            if (model.Step == 1)
            {
                ModelState.Remove("StudyName");
                ModelState.Remove("IsRecruitingIdentifiableParticipants");
                ModelState.Remove("CpmsId");

                if (ModelState.IsValid)
                {
                    model.GotoNextStep();
                }
            }
            else if (model.Step == 2)
            {
                if (string.IsNullOrEmpty(model.StudyName))
                {
                    ModelState.AddModelError("StudyName", "Enter the study name");
                }
                else if (model.StudyName.Length > 255)
                {
                    ModelState.AddModelError("StudyName", "Study name must be less than 255 characters");
                }

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

        return View(model);
    }

    // succss
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
            return NotFound();
        }

        ViewData["IsEditMode"] = true;
        studyModel.Step = field;
        return View(studyModel);
    }

    // POST: Study/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("FullName,EmailAddress,StudyName,CpmsId, Step")]
        StudyFormViewModel model)
    {
        ModelState.Remove("IsRecruitingIdentifiableParticipants");

        if (model.StudyName.Length > 255)
        {
            ModelState.AddModelError("StudyName", "Study name must be less than 255 characters");
        }

        if (ModelState.IsValid)
        {
            try
            {
                var studyToUpdate = await context.Studies.FirstOrDefaultAsync(s => s.Id == id);

                if (studyToUpdate == null)
                {
                    return NotFound();
                }

                studyToUpdate.FullName = model.FullName;
                studyToUpdate.EmailAddress = model.EmailAddress;
                studyToUpdate.StudyName = model.StudyName;
                studyToUpdate.CpmsId = model.CpmsId;
                studyToUpdate.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudyExists(id))
                {
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

        model.Id = id;
        ViewData["IsEditMode"] = true;

        return View(model);
    }

    private bool StudyExists(int id)
    {
        return context.Studies.Any(e => e.Id == id);
    }
}
