using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.Models;
using BPOR.Rms.Models.ResearcherEmail;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.Controllers;

public class ResearcherEmailController(ParticipantDbContext context,
    ILogger<StudyController> logger) : Controller
{
    // GET
    public async Task<IActionResult> Index(int studyId, CancellationToken cancellationToken)
    {
        ResearcherEmailViewModel model = new ResearcherEmailViewModel();
        model.StudyId = studyId;
        
        var study = await context.Studies
            .Where(s => s.Id == studyId)
            .AsStudyDetailsViewModel()
            .FirstOrDefaultAsync(cancellationToken);
        
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
    public async Task<IActionResult> SendEmail(ResearcherEmailViewModel model, CancellationToken cancellationToken)
    {
        if (Enum.TryParse<ResearcherEmailOptions>(
                model.SelectedEmailId,
                out var selectedEmail) &&
            !model.IsEligibleForPrescreener &&
            selectedEmail == ResearcherEmailOptions.NextStepOfferPreScreener)
        {
            ModelState.AddModelError(nameof(model.SelectedEmailId), 
                "We’re sorry, this study is not eligible for a pre-screener. Update the qualification criteria and try again");
        }

        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }
        
        var emailAddress = await context.Studies
            .Where(s => s.Id == model.StudyId)
            .Select(s => s.EmailAddress)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (string.IsNullOrEmpty(emailAddress))
        {
            logger.LogWarning("[HttpPost]ResearcherEmail.SendEmail cannot find researcher email for study: {StudyId}", model.StudyId);
            return NotFound();
        }

        if (!int.TryParse(model.SelectedEmailId, out var selectedEmailId))
        {
            throw new ArgumentException("SelectedEmailId is not a valid integer.");
        }

        var studyResearcherEmail = new StudyResearcherEmail
        {
            StudyResearcherEmailAddress = emailAddress,
            StudyResearcherEmailOptionId = selectedEmailId,
            DeliveryStatusId = (int)DeliveryStatus.Pending,
            StudyId = model.StudyId
        };

        context.StudyResearcherEmails.Add(studyResearcherEmail);
        await context.SaveChangesAsync(cancellationToken);
        
        return RedirectToAction(
            "Details",
            "Study",
            new { id = model.StudyId });
    }
}