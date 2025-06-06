using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Volunteer;
using LuhnNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIHR.GovUk.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using NIHR.Infrastructure;
using NIHR.Infrastructure.Models;
using Rbec.Postcodes;
using System.Text.Json;

namespace BPOR.Rms.Controllers;

public class VolunteerController(ParticipantDbContext context,
    ILogger<VolunteerController> logger,
   IPostcodeMapper locationApiClient) : Controller
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
        return View(new VolunteerFormViewModel());
    }

    private void ClearAllErrorsExcept(string errorToNotClear)
    {
        foreach (var key in ModelState.Keys)
        {
            if (key != errorToNotClear)
                ModelState.Remove(key);
            
        }
    }

    // POST: Study/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VolunteerFormViewModel model, string action, CancellationToken cancellationToken)
    {

        if (action == "AddressLookup")
        {

            ClearAllErrorsExcept("PostCode");

            if (model.ManualAddressEntry)
            {
                model.ManualAddressEntry = false;
            }
            model.SelectedAddress = null;
        }

        if (action == "ManualAddress")
        {
            ModelState.Clear();

            model.SelectedAddress = null;
            model.PostCode = null;
            model.ManualAddressEntry = true;
        }

        if (action == "DisplayEthnicBackgrounds")
        {
            ModelState.Clear();
            if (model.EthnicGroup == null)
            {
                ModelState.AddModelError("EthnicGroup", "Select an ethnic group");
            }
        }

        if (action == "Save")
        {
            if (model.DateOfBirth.HasValue && model.PostCode.HasValue && !String.IsNullOrEmpty(model.LastName))
            {
                await DoesPostcodeSurnameDoBComboExistAsync(model.PostCode.ToString(), model.LastName, model.DateOfBirth, cancellationToken);
            }

            if (!String.IsNullOrEmpty(model.EmailAddress))
            {
                await DoesUserEmailExistInDatabaseAsync(model.EmailAddress);
            }

            string canonicalTown = null;
            if (ModelState.IsValid) { 

                if (!model.ManualAddressEntry)
                { 
                    if (model.SelectedAddress != null)
                    {
                        var participantAddress =  JsonSerializer.Deserialize<PostcodeAddressModel>(model.SelectedAddress);
                        var TempPostcode = new Postcode();
                        model.Town = participantAddress.Town;
                        model.AddressLine1 = participantAddress.AddressLine1;
                        model.AddressLine2 = participantAddress.AddressLine2;
                        model.AddressLine3 = participantAddress.AddressLine3;
                        model.AddressLine4 = participantAddress.AddressLine4;
                        if (Postcode.TryParse(participantAddress.Postcode, out TempPostcode))
                        {
                            model.PostCode = TempPostcode;
                        };
                        canonicalTown = model.Town;
                    }
                }
                else
                {
                    List<PostcodeAddressModel> possibleAddresses = await GetAddresses(model.PostCode.ToString());
                    if (possibleAddresses.Count > 0) {
                        canonicalTown = possibleAddresses.First().Town;
                    }
                }

                bool? hasLongTermIllness = null;
                int? dailyLifeImpact= null;
                switch (model.LongTermConditionOrIllness)
                {
                    case 1:
                        hasLongTermIllness = null;
                        dailyLifeImpact = null;
                        break;
                    case 2:
                        hasLongTermIllness = false;
                        dailyLifeImpact = null;
                        break;
                    case 3:
                        hasLongTermIllness = true;
                        dailyLifeImpact = 3;
                        break;
                    case 4:
                        hasLongTermIllness = true;
                        dailyLifeImpact = 2;
                        break;
                    case 5:
                        hasLongTermIllness = true;
                        dailyLifeImpact = 1;
                        break;
                }

                var participant = new Participant
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    RegistrationConsent = true,
                    RegistrationConsentAtUtc = DateTime.Now,
                    Stage2CompleteUtc = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Email = model.EmailAddress == null ? "" : model.EmailAddress,
                    EthnicGroup = model.EthnicGroup,
                    EthnicBackground = model.EthnicBackground,
                    DateOfBirth = model.DateOfBirth.ToDateOnly()?.ToDateTime(TimeOnly.MinValue),
                    HasLongTermCondition = hasLongTermIllness,
                    DailyLifeImpactId = dailyLifeImpact,
                    GenderId = model.SexRegisteredAtBirth,
                    GenderIsSameAsSexRegisteredAtBirth = model.GenderIdentitySameAsBirth == "Prefer" ? null : model.GenderIdentitySameAsBirth == "Yes",
                    MobileNumber = model.Mobile,
                    LandlineNumber = model.LandLine,
                    CommunicationLanguageId = 1,
                    Address = new ParticipantAddress
                    {
                        AddressLine1 = model.AddressLine1,
                        AddressLine2 = model.AddressLine2,
                        AddressLine3 = model.AddressLine3,
                        AddressLine4 = model.AddressLine4,
                        Town = model.Town,
                        Postcode = model.PostCode.ToString(),
                        CanonicalTown = canonicalTown
                    },
                    ContactMethodId = new List<ParticipantContactMethod>()
                    {
                        new ParticipantContactMethod
                        {
                            ContactMethodId = (int) model.PreferredContactMethod
                        }
                    },
                    //may need to save participant and update GUID
                    ParticipantIdentifiers = new List<ParticipantIdentifier> {
                        new ParticipantIdentifier() {
                            IdentifierTypeId = (int)IdentifierTypes.Offline,
                            Value = Guid.NewGuid()
                        }
                    },
                    IsDeleted = false,
                    HealthConditions = model.AreasOfResearch == null ? null : model.AreasOfResearch.Select(x => new ParticipantHealthCondition
                    {
                        HealthConditionId = x
                    }).ToList()
                };

                context.Add(participant);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(VolunteerController.AccountSuccess));
            }
        }


        if (model.SelectedAddress != null)
        {
            TempData.Add("selectedAddress",JsonSerializer.Deserialize<PostcodeAddressModel>(model.SelectedAddress).FullAddress);
        }
        model.lastAction = action;
        return View(model);
    }


    private async Task DoesPostcodeSurnameDoBComboExistAsync(string postCode, string lastName, GovUkDate dateOfBirth, CancellationToken cancellationToken)
    {
        DateTime? DoB = dateOfBirth.ToDateOnly()?.ToDateTime(TimeOnly.MinValue);

        var user = await context.Participants
            .Where(p => p.LastName == lastName &&
                        p.DateOfBirth.HasValue &&
                        p.DateOfBirth.Value.Date == DoB.Value.Date &&
                        p.Address != null &&
                        p.Address.Postcode == postCode)
            .AnyAsync(cancellationToken);

        if (user)
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
            ModelState.AddModelError("EmailAddress", "Email address already exists and cannot be used");
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

    public async Task<List<PostcodeAddressModel>?> GetAddresses(string postcode)
    {
        IEnumerable<PostcodeAddressModel> addressModels;
        addressModels = await locationApiClient.GetAddressesByPostcodeAsync(postcode, new CancellationToken());

        if (addressModels.Count() > 0)
        {
            return addressModels.ToList();
        }
        else
        {
            return new List<PostcodeAddressModel>();
        }
    }

}
