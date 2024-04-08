using BPOR.Domain.Entities;
using BPOR.Domain.Extensions;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Study;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NIHR.Infrastructure.Interfaces;

namespace BPOR.Rms.Controllers;

public class StudyController(AuroraDbContext context) : Controller
{
    public async Task<IActionResult> Index(string? searchTerm, int currentPage = 1)
    {
        var pageSize = 9;
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

        // TODO create paginated results<T> generic class look at rider annotations, what am I getting back? Can we get a paginated result back that is a generic type?List<T> whatever it is
        // selector, source, pageIndex, pageSize(which can be defaulted)
        var paginatedStudies = await studiesQuery.OrderByDescending(s => s.Id)
            .ToPaginatedListAsync(Projections.StudyAsStudyListModel(), currentPage, pageSize);

        // TODO create paginated results<T> generic class
        var viewModel = new StudiesViewModel
        {
            Studies = paginatedStudies.Items,
            CurrentPage = currentPage,
            TotalPages = (int)Math.Ceiling((double)paginatedStudies.TotalCount / pageSize),
            HasSearched = !string.IsNullOrEmpty(searchTerm),
            SearchTerm = searchTerm,
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
                JsonConvert.DeserializeObject<NotificationBannerModel>(TempData["Notification"].ToString());
        }

        return View(study);
    }

    // GET: Study/Create
    public IActionResult Create()
    {
        return View(new StudyFormViewModel());
    }

    // POST: Study/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,FullName,EmailAddress,StudyName,CpmsId,AnonymousEnrolment,Step")]
        StudyFormViewModel model, string action)
    {
        if (action == "Next" && model.Step == 1)
        {
            ModelState.Remove("StudyName");
            ModelState.Remove("AnonymousEnrolment");
            ModelState.Remove("CpmsId");

            if (ModelState.IsValid)
            {
                model.Step = 2;
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
                    IsAnonymous = model.AnonymousEnrolment ?? false,
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

        ModelState.Remove("AnonymousEnrolment");

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

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        return View(model);
    }

    private bool StudyExists(int id)
    {
        return context.Studies.Any(e => e.Id == id);
    }
}
