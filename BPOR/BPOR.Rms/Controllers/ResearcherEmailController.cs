using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.ResearcherEmail;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.Controllers;

public class ResearcherEmailController(ParticipantDbContext context,
    ICurrentUserProvider<User> currentUserProvider,
    ILogger<StudyController> logger) : Controller
{
    // GET
    public async Task<IActionResult> Index(long studyId)
    {
        ResearcherEmailViewModel model = new ResearcherEmailViewModel();
        model.StudyId = studyId;
        
        var study = await context.Studies
            .Where(s => s.Id == studyId)
            .AsStudyDetailsViewModel()
            .FirstOrDefaultAsync();
        
        if (study == null)
        {
            logger.LogWarning("[HttpGet]ResearcherEmail called with non-existent study: {StudyId}", studyId);
            return NotFound();
        }
        
        model.IsEligibilityCriteriaComplete = study.Study.IsEligibilityCriteriaComplete;
        model.IsEligibleForPrescreener = study.Study.IsEligibleForPrescreener;
        
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(ResearcherEmailViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }
        
        return View();
    }
}