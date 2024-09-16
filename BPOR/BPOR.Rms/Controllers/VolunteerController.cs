using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Models.Volunteer;
using LuhnNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIHR.GovUk.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BPOR.Rms.Controllers;

public class VolunteerController(ParticipantDbContext context) : Controller
{
    public IActionResult Consent()
    {
        return View(new VolunteerContactConsentViewModel());
    }

    [HttpPost]
    public IActionResult Consent(VolunteerContactConsentViewModel model)
    {
        if (!model.AgreedToContactConsent)
        {
            ModelState.AddModelError("AgreedToContactConsent", "Confirm you have read and understood the privacy policy");
        }

        if (ModelState.IsValid)
        {
            return RedirectToAction("Create");
        }

        return View(model);
    }

    public IActionResult Create()
    {
        var model = new VolunteerFormViewModel();
        return View(model);
    }

    // POST: Study/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("FirstName,LastName,DateOfBirth,PostCode,AddressLine1,AddressLine2,AddressLine3,AddressLine4,Town,PreferredContactMethod,Email,LandLine,Mobile" +
        ",SexRegisteredAtBirth,GenderIdentitySameAsBirth,EthnicGroup,EthnicBackground,LongTermConditionOrIllness,AreasOfResearch")]
        VolunteerFormViewModel model, string action)
    {

        if (String.IsNullOrEmpty(model.LandLine) && String.IsNullOrEmpty(model.Mobile))
        {
            ModelState.AddModelError("LandLine", "At least one of either a Landline or Mobile number must be provided");
        }
        if (model.PreferredContactMethod == "Email" && String.IsNullOrEmpty(model.Email))
        {
            ModelState.AddModelError("Email", "Email must be provided when preferred contact method is email");
        }

        ValidateDateOfBirth(model);

        if (ModelState.IsValid)
        {
            return RedirectToAction(nameof(VolunteerController.AccountSuccess));
        }

        return View(model);
    }

    private void ValidateDateOfBirth(VolunteerFormViewModel model)
    {
        if (!model.DateOfBirth.HasValue)
        {
            ModelState.AddModelError("DateOfBirth_Day", "Date of birth is required");
        }
    }

    public IActionResult AccountSuccess()
    {
        return View();
    }

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
                model.VolunteerReferenceNumbers.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Distinct().ToArray();

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
                        Title = "Success",
                        Heading = bodyText,
                        Body = totalPreviouslyEnrolled > 0 ? subBodyText : null,
                        LinkText = "Return to the study details page",
                        LinkUrl = Url.ActionLink("Details", "Study", new { id = model.StudyId })
                    });
                }

                if (totalEnrolled == 0 && totalPreviouslyEnrolled > 0)
                {
                    this.AddNotification(new NotificationBannerModel
                    {
                        IsSuccess = true,
                        Title = "Success",
                        Heading = subBodyText,
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
            Title = "Success",
            Heading = $"{model.RecruitmentTotal} volunteer(s) recorded as recruited.",
            LinkText = "Return to study details page",
            LinkUrl = Url.ActionLink("Details", "Study", new { id = model.StudyId })
        });

        return RedirectToAction("UpdateAnonymousRecruited", model);
    }
}
