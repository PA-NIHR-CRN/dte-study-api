using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Study;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NIHR.Infrastructure.Paging;

namespace BPOR.Rms.Controllers;

public class StudyController(ParticipantDbContext context, IPaginationService paginationService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string? searchTerm, bool hasBeenReset = false, CancellationToken token = default)
    {
        var studiesQuery = context.Studies.AsQueryable();

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

        study.HasEmailCampaigns = context.Studies.Any(s => s.FilterCriterias.Any(f => f.EmailCampaigns.Any()) && s.Id == study.Study.Id);

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

    public async Task<IActionResult> Edit(int id, int field)
    {
        var studyModel = await context.Studies
            .AsStudyFormViewModel(step: field, isEditMode: true)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (studyModel == null)
        {
            return NotFound();
        }

        return View(studyModel);
    }

    // POST: Study/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("FullName,EmailAddress,StudyName,CpmsId, Step")]
        StudyFormEditModel model)
    {
        ModelState.Remove("IsRecruitingIdentifiableParticipants");

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

            TempData["Notification"] = JsonConvert.SerializeObject(new NotificationBannerModel
            {
                IsSuccess = true,
                Heading = "Study details updated",
                Body = $"{model.StudyName} has been successfully updated",
            });

            return RedirectToAction(nameof(Details), new { id });
        }

        return View(model);
    }

    private bool StudyExists(int id)
    {
        return context.Studies.Any(e => e.Id == id);
    }
}
