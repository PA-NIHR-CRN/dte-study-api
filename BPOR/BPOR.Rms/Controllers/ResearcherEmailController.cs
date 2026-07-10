using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.Models;
using BPOR.Rms.Models.ResearcherEmail;
using BPOR.Rms.Startup;
using BPOR.Rms.VolunteerInformation;
using BPOR.Rms.VolunteerInformation.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.NotificationService;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

namespace BPOR.Rms.Controllers;

public class ResearcherEmailController(ParticipantDbContext context,
    ICurrentUserProvider<User> currentUserProvider,
    ILogger<ResearcherEmailController> logger,
    IOptions<VipSettings> vipOptions,
    IOptions<RmsSettings> rmsOptions) : Controller
{
    // GET
    public async Task<IActionResult> Index(int studyId, CancellationToken cancellationToken)
    {
        bool isAdmin = currentUserProvider.User.HasRole(Domain.Enums.UserRole.Admin);
        if (!isAdmin)
        {
            return View("Unauthorised");
        }
        
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
    public async Task<IActionResult> SendEmail(
        [FromServices] INotificationService<ResearcherEmailNotificationDeliveryHandler> notificationService,
        ResearcherEmailViewModel model,
        CancellationToken cancellationToken)
    {
        bool isAdmin = currentUserProvider.User.HasRole(Domain.Enums.UserRole.Admin);
        if (!isAdmin)
        {
            return View("Unauthorised");
        }
        
        if (model.SelectedEmailId == 0)
        {
            ModelState.AddModelError(
                nameof(model.SelectedEmailId),
                "Please select an email to send to the researcher.");
        }
        
        var study = await context.Studies
            .Where(s => s.Id == model.StudyId)
            .Select(s => new
            {
                Id = s.Id,
                StudyName = s.StudyName,
                FullName = s.FullName,
                EmailAddress = s.EmailAddress,
                HasMultipleResearchLocations = s.HasMultipleResearchLocations,
                SinglePersonResponsibleForRecruiting = s.SinglePersonResponsibleForRecruiting
            })
            .SingleOrDefaultAsync(cancellationToken);
        
        if (study == null)
        {
            logger.LogWarning("[HttpPost]ResearcherEmail.SendEmail cannot find study: {StudyId}", model.StudyId);
            return NotFound();
        }
        
        var isEligibilityCriteriaComplete =
            study.HasMultipleResearchLocations.HasValue &&
            study.SinglePersonResponsibleForRecruiting.HasValue;

        var isEligibleForPrescreener =
            isEligibilityCriteriaComplete &&
            !(study.HasMultipleResearchLocations!.Value &&
              study.SinglePersonResponsibleForRecruiting!.Value);
        
        if (!isEligibleForPrescreener  &&
            (ResearcherEmailOptions)model.SelectedEmailId == ResearcherEmailOptions.NextStepOfferPreScreener)
        {
            ModelState.AddModelError(
                nameof(model.SelectedEmailId),
                "We’re sorry, this study is not eligible for a pre-screener. Update the qualification criteria and try again");
        }

        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        var studyResearcherEmail = new StudyResearcherEmail
        {
            StudyResearcherEmailAddress = study.EmailAddress,
            StudyResearcherEmailOptionId = model.SelectedEmailId,
            DeliveryStatusId = (int)DeliveryStatus.Pending,
            StudyId = model.StudyId
        };

        context.StudyResearcherEmails.Add(studyResearcherEmail);
        await context.SaveChangesAsync(cancellationToken);

        string templateId = model.SelectedEmailId switch
        {
            1 => rmsOptions.Value.ReasearchIntroductoryTemplateId,
            2 => rmsOptions.Value.ReasearchNextStepsWithPrescreenerTemplateId,
            3 => rmsOptions.Value.ReasearchNextStepsWithoutPrescreenerTemplateId,
            _ => throw new ArgumentOutOfRangeException(nameof(model.SelectedEmailId), model.SelectedEmailId, null)
        };

        await notificationService.SendNotification(
            new UnkeyedSendNotificationRequest()
            {
                ContactMethod = NotificationContactMethod.Email,
                Personalisation = new Dictionary<string, string>()
                                              {
                                                  [PersonalisationKeys.Email] = study.EmailAddress,
                                                  ["RmsStudyId"] = study.Id.ToString(),
                                                  ["StudyName"] = study.StudyName,
                                                  ["SenderName"] = currentUserProvider?.User?.ContactFullName ?? "BPOR Team",
                                                  ["RecipientName"] = study.FullName ?? "Researcher",
                                                  ["VipGoogleDocUrl"] = vipOptions.Value.VipGoogleDocUrl
                                              },
                Reference = studyResearcherEmail.Id.ToString(),
                TemplateId = templateId
            },
            cancellationToken);
        
        return RedirectToAction(
            "Details",
            "Study",
            new { id = model.StudyId });
    }
}