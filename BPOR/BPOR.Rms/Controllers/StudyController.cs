using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Study;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NIHR.Infrastructure.AspNetCore;
using NIHR.Infrastructure.EntityFrameworkCore.Paging;

namespace BPOR.Rms.Controllers;

public class StudyController(ParticipantDbContext context, IPaginationService paginationService) : Controller
{
    
    [HttpPost]
    public IActionResult PerformSearch(string searchTerm)
    {
        return RedirectToAction("Index", new { searchTerm, paginationService.Page, hasSearched = true });
    }
    
    public async Task<IActionResult> Index(string? searchTerm, bool hasSearched = false, bool hasBeenReset = false, CancellationToken token = default)
    {
        var studiesQuery = context.Studies.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.Trim();
            var isParsedInt = int.TryParse(searchTerm, out var searchInt);
            studiesQuery = studiesQuery.Where(s => (isParsedInt && s.Id == searchInt) ||
                                                   s.StudyName.Contains(
                                                       searchTerm) // TODO investigate full text search
                                                   || (isParsedInt && s.CpmsId == searchInt));
        }

        var deferredStudiesPage = studiesQuery
            .AsStudyListModel()
            .OrderByDescending(s => s.Id)
            .DeferredPage(paginationService);            

        var viewModel = new StudiesViewModel
        {
            Studies = await deferredStudiesPage.ValueAsync(token),
            HasSearched = hasSearched,
            SearchTerm = searchTerm ?? string.Empty,
            HasBeenReset = hasBeenReset,
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
            .Select(Projections.StudyAsStudyDetailsViewModel())
            .FirstOrDefaultAsync();

        if (study == null)
        {
            return NotFound();
        }


        if (TempData["Notification"] != null)
        {
            study.Notification =
                JsonConvert.DeserializeObject<NotificationBannerModel>(TempData["Notification"]?.ToString());
        }

        return View(study);
    }

    // GET: Study/Create
    public IActionResult Create()
    {
        ViewData["ShowBackLink"] = true;
        ViewData["ShowProgressBar"] = true;
        ViewData["ProgressPercentage"] = 0;
        return View(new StudyFormViewModel());
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
        ViewData["ShowBackLink"] = true;
        ViewData["ShowProgressBar"] = true;
        ViewData["ProgressPercentage"] = (model.Step - 1) * 50;
        if (action == "Next" && model.Step == 1)
        {
            ModelState.Remove("StudyName");
            ModelState.Remove("IsRecruitingIdentifiableParticipants");
            ModelState.Remove("CpmsId");

            if (ModelState.IsValid)
            {
                model.Step = 2;
                ViewData["ProgressPercentage"] = (model.Step - 1) * 50;
                return View(model);
            }
        }
        else if (action == "Save" && model.Step == 2)
        {
            if (ModelState.IsValid)
            {
                var study = new Study
                {
                    FullName = model.FullName,
                    EmailAddress = model.EmailAddress,
                    StudyName = model.StudyName,
                    CpmsId = model.CpmsId,
                    IsRecruitingIdentifiableParticipants = model.IsRecruitingIdentifiableParticipants ?? false,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                context.Add(study);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(AddStudySuccess), new AddStudySuccessViewModel
                {
                    Id = study.Id,
                    StudyName = study.StudyName,
                });
            }

            return View(model);
        }

        // For "Back" action or if validation fails, just return to the current view
        return View(model);
    }

    // succss
    public IActionResult AddStudySuccess(AddStudySuccessViewModel viewModel)
    {
        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int? id, int field)
    {
        if (id == null)
        {
            return NotFound();
        }

        var studyModel = await context.Studies
            .Where(s => s.Id == id)
            .Select(Projections.StudyAsStudyListModel())
            .FirstOrDefaultAsync();
        if (studyModel == null)
        {
            return NotFound();
        }

        var studyFormViewModel = new StudyFormViewModel
        {
            Id = studyModel.Id,
            FullName = studyModel.FullName,
            EmailAddress = studyModel.EmailAddress,
            StudyName = studyModel.StudyName,
            CpmsId = studyModel.CpmsId,
            Step = field,
            IsEditMode = true,
        };

        return View(studyFormViewModel);
    }

    // POST: Study/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,FullName,EmailAddress,StudyName,CpmsId, Step")]
        StudyFormViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        ModelState.Remove("IsRecruitingIdentifiableParticipants");

        if (ModelState.IsValid)
        {
            try
            {
                var studyToUpdate = await context.Studies.FirstOrDefaultAsync(s => s.Id == model.Id);

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
                if (!StudyExists(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["Notification"] = JsonConvert.SerializeObject(new NotificationBannerModel
            {
                IsSuccess = true,
                Heading = "Study details updated",
                Body = $"{model.StudyName} has been successfully updated",
            });

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        return View(model);
    }

    private bool StudyExists(int id)
    {
        return context.Studies.Any(e => e.Id == id);
    }
}
