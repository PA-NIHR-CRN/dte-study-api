using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Volunteer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BPOR.Rms.Controllers;

public class VolunteerController(ParticipantDbContext context) : Controller
{
    private async Task<UpdateAnonymousRecruitedViewModel?> GetStudyDetails(int studyId)
    {
        return await context.Studies
            .Where(s => s.Id == studyId)
            .Select(Projections.StudyAsUpdateAnonymousRecruitedViewModel())
            .FirstOrDefaultAsync();
    }

    public IActionResult UpdateRecruited(UpdateRecruitedViewModel model)
    {
        ModelState.Remove("VolunteerReferenceNumbers");


        return View(model);
    }

    [HttpPost]
    public IActionResult SubmitVolunteerNumbers(UpdateRecruitedViewModel model)
    {
        // get each id from the string splitting by new line
        var volunteerIds =
            model.VolunteerReferenceNumbers.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries);

        return RedirectToAction("UpdateRecruited", model);
    }

    public async Task<IActionResult> UpdateAnonymousRecruited(UpdateAnonymousRecruitedViewModel model)
    {
        ModelState.Remove("RecruitmentTotal");

        if (model.StudyId != 0)
        {
            var study = await GetStudyDetails(model.StudyId);

            if (TempData["Notification"] != null)
            {
                study.Notification =
                    JsonConvert.DeserializeObject<NotificationBannerModel>(TempData["Notification"]?.ToString());
            }

            return View(study);
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRecruitmentTotal(UpdateAnonymousRecruitedViewModel model)
    {
        ModelState.Remove("StudyName");
        ModelState.Remove("StudyId");
        ModelState.Remove("Notification");
        ModelState.Remove("EnrollmentDetails");

        if (!ModelState.IsValid)
        {
            var study = await GetStudyDetails(model.StudyId);
            model.EnrollmentDetails = study?.EnrollmentDetails;
            return View("UpdateAnonymousRecruited", model);
        }

        var manualEnrollment = new ManualEnrollment
        {
            StudyId = model.StudyId,
            TotalEnrollments = model.RecruitmentTotal.Value
        };
        context.ManualEnrollments.Add(manualEnrollment);
        await context.SaveChangesAsync();

        TempData["Notification"] = JsonConvert.SerializeObject(new NotificationBannerModel
        {
            IsSuccess = true,
            Heading = "Success",
            Body = $" {model.RecruitmentTotal} volunteer(s) recorded as recruited.",
            LinkText = "Return to study",
            LinkUrl = Url.ActionLink("Details", "Study", new { id = model.StudyId })
        });

        return RedirectToAction("UpdateAnonymousRecruited", model);
    }
}
