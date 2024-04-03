using BPOR.Domain.Entities;
using BPOR.Domain.Extensions;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Study;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.Interfaces;

namespace BPOR.Rms.Controllers;

public class StudyController(AuroraDbContext context, IIdentityProviderService identityProviderService) : Controller
{
    public async Task<IActionResult> Index(string? searchString, int currentPage = 1)
    {
        var pageSize = 4;
        var studiesQuery = context.Studies.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            searchString = searchString.Trim();
            var isParsedInt = int.TryParse(searchString, out var searchInt);
            studiesQuery = studiesQuery.Where(s => (isParsedInt && s.Id == searchInt) ||
                                                   s.StudyName.Contains(
                                                       searchString) // TODO investigate full text search
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
            HasSearched = !string.IsNullOrEmpty(searchString),
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
            .FirstOrDefaultAsync(m => m.Id == id);
        if (study == null)
        {
            return NotFound();
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
        [Bind("Id,FullName,EmailAddress,StudyName,CpmsId,IsAnonymousEnrollment,Step")]
        StudyFormViewModel model, string action)
    {
        if (action == "Next" && model.Step == 1)
        {
            ModelState.Remove("StudyName");
            ModelState.Remove("IsAnonymous");
            ModelState.Remove("CpmsId");

            if (ModelState.IsValid)
            {
                model.Step = 2;
                return View(model);
            }
        }
        else if (action == "Save" && model.Step == 2)
        {
            ModelState.Remove("FullName");
            ModelState.Remove("EmailAddress");

            if (ModelState.IsValid)
            {
                var study = new Study
                {
                    FullName = model.FullName,
                    EmailAddress = model.EmailAddress,
                    StudyName = model.StudyName,
                    CpmsId = model.CpmsId,
                    IsAnonymous = model.AnonymousEnrolment,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                context.Add(study);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // For "Back" action or if validation fails, just return to the current view
        return View(model);
    }

    // GET: Study/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var study = await context.Studies.FindAsync(id);
        if (study == null)
        {
            return NotFound();
        }

        return View(study);
    }

    // POST: Study/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,FullName,EmailAddress,StudyName,CpmsId,IsDeleted,CreatedAt,UpdatedAt")]
        Study studyController)
    {
        if (id != studyController.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(studyController);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudyExists(studyController.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        return View(studyController);
    }

    // GET: Study/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var study = await context.Studies
            .FirstOrDefaultAsync(m => m.Id == id);
        if (study == null)
        {
            return NotFound();
        }

        return View(study);
    }

    // POST: Study/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var study = await context.Studies.FindAsync(id);
        if (study != null)
        {
            context.Studies.Remove(study);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StudyExists(int id)
    {
        return context.Studies.Any(e => e.Id == id);
    }
}
