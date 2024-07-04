using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Volunteer;
using LuhnNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

    public async Task<IActionResult> UpdateRecruited(UpdateRecruitedViewModel model)
    {
        ModelState.Remove("VolunteerReferenceNumbers");

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitVolunteerNumbers(UpdateRecruitedViewModel model)
    {
        if (String.IsNullOrEmpty(model.VolunteerReferenceNumbers))
        {
            ModelState.AddModelError("VolunteerReferenceNumbers",
                "Enter a Be Part of Research volunteer reference number");
            return View("UpdateRecruited", model);
        }

        // do not allow non-numeric characters, allow spaces and line breaks
        if (Regex.IsMatch(model.VolunteerReferenceNumbers, "[^0-9\\s\r\n]"))
        {
            ModelState.AddModelError("VolunteerReferenceNumbers",
                "Enter a valid volunteer reference number. Check that all volunteer reference numbers are in the valid format, for example 9703876601877339.");
        }

        if (!ModelState.IsValid)
        {
            return View("UpdateRecruited", model);
        }

        if (!String.IsNullOrEmpty(model.VolunteerReferenceNumbers))
        {
            // get each id from the string splitting by new line
            var volunteerRefs =
                model.VolunteerReferenceNumbers.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries);

            var totalVolunteers = volunteerRefs.Length;
            var totalEnrolled = 0;
            var totalPreviouslyEnrolled = 0;

            if (totalVolunteers > 0)
            {
                foreach (var reference in volunteerRefs)
                {
                    bool isValid = Luhn.IsValid(reference);

                    if (!isValid)
                    {
                        ModelState.AddModelError("VolunteerReferenceNumbers",
                            "Enter a valid volunteer reference number. Check that all volunteer reference numbers are in the valid format, for example 9703876601877339.");
                        return View("UpdateRecruited", model);
                    }
                }

                var studyParticipants = context.StudyParticipantEnrollment
                    .Where(p => volunteerRefs.Contains(p.Reference) && p.StudyId == model.StudyId).ToList();

                if (studyParticipants.Count < volunteerRefs.Length)
                {
                    ModelState.AddModelError("VolunteerReferenceNumbers",
                        "Enter a valid volunteer reference number. Check that all volunteer reference numbers are in the valid format, for example 9703876601877339.");
                    return View("UpdateRecruited", model);
                }

                foreach (var participant in studyParticipants)
                {
                    if (participant.EnrolledAt == null)
                    {
                        participant.EnrolledAt = DateTime.Now;
                        await context.SaveChangesAsync();
                        totalEnrolled++;
                    }
                    else
                    {
                        totalPreviouslyEnrolled++;
                    }
                }

                string bodyText = $" {totalEnrolled} of {totalVolunteers} volunteer(s) recorded as recruited.";
                string subBodyText = $" {totalPreviouslyEnrolled} already recorded as recruited.";

                if (totalEnrolled > 0)
                {
                    var manualEnrollment = new ManualEnrollment
                    {
                        StudyId = model.StudyId,
                        TotalEnrollments = totalEnrolled
                    };
                    context.ManualEnrollments.Add(manualEnrollment);
                    await context.SaveChangesAsync();

                    this.AddNotification(new NotificationBannerModel
                    {
                        IsSuccess = true,
                        Heading = "Success",
                        Body = bodyText,
                        SubBodyText = totalPreviouslyEnrolled > 0 ? subBodyText : null,
                        LinkText = "Return to the study details page",
                        LinkUrl = Url.ActionLink("Details", "Study", new { id = model.StudyId })
                    });
                }

                if (totalEnrolled == 0 && totalPreviouslyEnrolled > 0)
                {
                    this.AddNotification(new NotificationBannerModel
                    {
                        IsSuccess = true,
                        Heading = "Success",
                        Body = subBodyText,
                        LinkText = "Return to the study details page",
                        LinkUrl = Url.ActionLink("Details", "Study", new { id = model.StudyId })
                    });
                }
            }
        }

        model.VolunteerReferenceNumbers = string.Empty;
        return RedirectToAction("UpdateRecruited", model);
    }

    public async Task<IActionResult> UpdateAnonymousRecruited(UpdateAnonymousRecruitedViewModel model)
    {
        ModelState.Remove("RecruitmentTotal");

        if (model.StudyId != 0)
        {
            var study = await GetStudyDetails(model.StudyId);

            return View(study);
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRecruitmentTotal(UpdateAnonymousRecruitedViewModel model)
    {
        ModelState.Remove("StudyName");
        ModelState.Remove("StudyId");
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

        this.AddNotification(new NotificationBannerModel
        {
            IsSuccess = true,
            Heading = "Success",
            Body = $" {model.RecruitmentTotal} volunteer(s) recorded as recruited.",
            LinkText = "Return to study details page",
            LinkUrl = Url.ActionLink("Details", "Study", new { id = model.StudyId })
        });

        return RedirectToAction("UpdateAnonymousRecruited", model);
    }
}
