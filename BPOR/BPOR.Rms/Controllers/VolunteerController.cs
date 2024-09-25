using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Models.Volunteer;
using LuhnNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIHR.GovUk.AspNetCore.Mvc;
using System;
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
            ModelState.AddModelError("AgreedToContactConsent", "Confirm that the Privacy and Data Sharing Policy has been read and understood before giving consent");
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
            ModelState.AddModelError("LandLine", "Enter either a UK landline number or UK mobile number");
        }
        if (model.PreferredContactMethod == "Email" && String.IsNullOrEmpty(model.Email))
        {
            ModelState.AddModelError("Email", "Email address cannot be blank");
        }

        ValidateDateOfBirth(model.DateOfBirth);

        if (!String.IsNullOrEmpty(model.Email))
        {
            await DoesUserEmailExistInDatabaseAsync(model.Email);
        }

        if (!model.PostCode.HasValue)
        {
            ModelState.AddModelError("PostCode", "Enter a postcode");
        }

        if (model.DateOfBirth.HasValue && model.PostCode.HasValue && !String.IsNullOrEmpty(model.LastName))
        {
            await DoesPostcodeSurnameDoBComboExistAsync(model.PostCode.Value.ToString(), model.LastName, model.DateOfBirth);
        }

        if (ModelState.IsValid)
        {
            return RedirectToAction(nameof(VolunteerController.AccountSuccess));
        }

        return View(model);
    }

    private async Task DoesPostcodeSurnameDoBComboExistAsync(string postCode, string lastName, GovUkDate dateOfBirth)
    {
        DateTime DoB = new DateTime(dateOfBirth.Year.Value, dateOfBirth.Month.Value, dateOfBirth.Day.Value);

        var user = await context.Participants
            .Where(p => p.LastName == lastName &&
                        p.DateOfBirth.HasValue &&
                        p.DateOfBirth.Value.Date == DoB.Date &&
                        p.Address != null &&
                        p.Address.Postcode == postCode)
            .FirstOrDefaultAsync();

        if (user != null)
        {
            ModelState.AddModelError("LastName", "Combination of surname, date of birth and postcode already exists and cannot be used");
        }
    }

    private async Task DoesUserEmailExistInDatabaseAsync(string email)
    {
        var user = await context.Participants
            .Where(p => p.Email == email)
            .FirstOrDefaultAsync();

        if (user != null)
        {
            ModelState.AddModelError("Email", "Email address already exists and cannot be used");
        }
    }

    private void ValidateDateOfBirth(GovUkDate dateOfBirth)
    {
        
        // lot of dupelication here with recruitment start and end dates, can these be consolidated or largly consolidated?

        if (dateOfBirth.Day == null)
        {
            ModelState.AddModelError("DateOfBirth.Day", "Date of birth must include a day");
        }

        if (dateOfBirth.Month == null)
        {
            ModelState.AddModelError("DateOfBirth.Month", "Date of birth must include a month");
        }

        if (dateOfBirth.Year == null)
        {
            ModelState.AddModelError("DateOfBirth.Year", "Date of birth must include a year");
        }

        if (dateOfBirth.Day != null && dateOfBirth.Month == null && dateOfBirth.Year == null)
        {
            CleardateOfBirthErrorStates();
            ModelState.AddModelError("DateOfBirth.Day", "Date of birth must include a month and year");
        }

        if (dateOfBirth.Day == null && dateOfBirth.Month != null && dateOfBirth.Year == null)
        {
            CleardateOfBirthErrorStates();
            ModelState.AddModelError("DateOfBirth.Day", "Date of birth must include a day and year");
        }

        if (dateOfBirth.Day == null && dateOfBirth.Month == null && dateOfBirth.Year != null)
        {
            CleardateOfBirthErrorStates();
            ModelState.AddModelError("DateOfBirth.Day", "Date of birth must include a day and month");
        }

        if (!dateOfBirth.Day.HasValue && !dateOfBirth.Month.HasValue && !dateOfBirth.Year.HasValue)
        {
            CleardateOfBirthErrorStates();
            ModelState.AddModelError("DateOfBirth", "Enter a date of birth");
        }

        if (dateOfBirth.HasValue)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            DateOnly eighteenYearsAgo = today.AddYears(-18);

            if (dateOfBirth.ToDateOnly() > eighteenYearsAgo)
            {
                ModelState.AddModelError("DateOfBirth.Day", "Volunteer must be aged 18 or older");
            }
        }
    }

    private void CleardateOfBirthErrorStates()
    {
        ModelState["DateOfBirth.Day"].Errors.Clear();
        ModelState["DateOfBirth.Month"].Errors.Clear();
        ModelState["DateOfBirth.Year"].Errors.Clear();
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
