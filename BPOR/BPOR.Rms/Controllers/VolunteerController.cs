using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Models.Volunteer;
using LuhnNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.Infrastructure.EntityFrameworkCore.Settings;
using NIHR.Infrastructure.Settings;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System;
using System.Text.RegularExpressions;

namespace BPOR.Rms.Controllers;

public class VolunteerController(ParticipantDbContext context,
    ILogger<VolunteerController> logger) : Controller
{
    private readonly IOptions<PostcodeLookupSettings> _postcodeLookupSettings;
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


    private bool isPostcodeValid(string postcode)
    {
        return Regex.IsMatch(postcode, "([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\\s?[0-9][A-Za-z]{2})");
    }
    // POST: Study/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("FirstName,LastName,DateOfBirth,PostCode,AddressLine1,AddressLine2,AddressLine3,AddressLine4,Town,PreferredContactMethod,EmailAddress,LandLine,Mobile" +
        ",ManualAdressEntry,SexRegisteredAtBirth,GenderIdentitySameAsBirth,EthnicGroup,EthnicBackground,EthnicBackgroundOptions,EthnicBackgroundOther,LongTermConditionOrIllness,AreasOfResearch,Addresses,SelectedAddress,SelectedAddressId")]
        VolunteerFormViewModel model, string action)
    {
        if (action == "AddressLookup")
        {
            ModelState.Clear();
            if (!model.PostCode.HasValue)
            {
                ModelState.AddModelError("PostCode", "Enter a postcode");
            }
            else
            if (isPostcodeValid(model.PostCode.ToString()))
            {
                model.Addresses = await GetAddresses(model.PostCode.Value.ToString());
                model.Addresses.Insert(0,new AddressDetails
                {
                    FullAddress = model.Addresses.Count() + " Addresses found"
                    
                });
            }
            if (model.ManualAdressEntry)
            {
                model.ManualAdressEntry = false;
            }
            return View(model);
        }
        if (action == "ManualAddress")
        {
            if (model.Addresses != null)
            {
                model.Addresses = null;
            }
            if (model.SelectedAddressId != null)
            {
                model.SelectedAddressId = null;

            }
            model.ManualAdressEntry = true;
           
            return View(model);
        }

        if (action == "DisplayEthnicBackgrounds")
        {
            
            if (model.EthnicGroup == null)
            { 
                ModelState.AddModelError("EthnicGroup", "Select a ethnic group");
                return View(model);
            }
            switch (model.EthnicGroup)
            {
                case "Asian or Asian British":
                    model.EthnicBackgroundOptions = model.EthnicbackgroundValuesAAB;
                    break;
                case "Black, African, Black British or Caribbean":
                    model.EthnicBackgroundOptions = model.EthnicbackgroundValuesBABBC;
                    break;
                case "Mixed or multiple ethnic groups":
                    model.EthnicBackgroundOptions = model.EthnicbackgroundValuesMM;
                    break;
                case "White":
                    model.EthnicBackgroundOptions = model.EthnicbackgroundValuesW;
                    break;
                case "Other ethnic group":
                    model.EthnicBackgroundOptions = model.EthnicbackgroundValuesO;
                    break;
            }
            return View(model);
        }


        if (action == "Save")
        {

            ValidateFields(model);
           
            


            if (!String.IsNullOrEmpty(model.EmailAddress))
            {
                await DoesUserEmailExistInDatabaseAsync(model.EmailAddress);
            }


            //validate and check email.

            //else
            //{
            //    if (model.Addresses == null)
            //    {
            //        model.Addresses = await GetAddresses(model.PostCode.Value.ToString());
            //    }
            //}

            //if (model.SelectedAddressId != null)
            //{
            //    var SelectedAddress = model.Addresses?.FirstOrDefault(a => a.FullAddress == model.SelectedAddressId);
            //    model.Town = SelectedAddress.Town;
            //    model.AddressLine1 = SelectedAddress.AddressLine1;
            //    model.AddressLine2 = SelectedAddress.AddressLine2;
            //    model.AddressLine3 = SelectedAddress.AddressLine3;
            //    model.AddressLine4 = SelectedAddress.AddressLine4;

            //}

            if (model.DateOfBirth.HasValue && model.PostCode.HasValue && !String.IsNullOrEmpty(model.LastName))
            {
                await DoesPostcodeSurnameDoBComboExistAsync(model.PostCode.Value.ToString(), model.LastName, model.DateOfBirth);
            }


            if (ModelState.IsValid)
            { 
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
                    GenderIsSameAsSexRegisteredAtBirth = model.GenderIdentitySameAsBirth == "Prefer" ? null: model.GenderIdentitySameAsBirth == "Yes",
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
                        Postcode = model.PostCode.ToString()
                    },
                    IsDeleted = false,
                    HealthConditions = model.AreasOfResearch.Select(x => new ParticipantHealthCondition
                    {
                        HealthConditionId = x
                    }).ToList()
                };
                context.Add(participant);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(VolunteerController.AccountSuccess));
            }
        }
        return View(model);
    }

    private void ValidateFields(VolunteerFormViewModel model)
    {

        // validate required fields
        if (String.IsNullOrEmpty(model.LandLine) && String.IsNullOrEmpty(model.Mobile))
        {
            ModelState.AddModelError("LandLine", "Enter either a UK landline number or UK mobile number");
        }
        if (model.PreferredContactMethod == "Email" && String.IsNullOrEmpty(model.EmailAddress))
        {
            ModelState.AddModelError("EmailAddress", "Email address cannot be blank");
        }

        if (!model.PostCode.HasValue)
        {
            ModelState.AddModelError("PostCode", "Enter a postcode");
        }
        if (!model.ManualAdressEntry)
        {
            //addressline1 &town needs verified
            if (String.IsNullOrEmpty(model.AddressLine1))
            {
                ModelState.AddModelError("AddressLine1", "Enter the first line of the address");
            }

            if (String.IsNullOrEmpty(model.Town))
            {
                ModelState.AddModelError("Town", "Enter the town of the address");
            }
        }
        if(model.SexRegisteredAtBirth == 0)
        {
            ModelState.AddModelError("SexRegisteredAtBirth", "Select if the sex registered at birth is female or male");
        }
        if (model.LongTermConditionOrIllness == 0)
        {
            ModelState.AddModelError("LongTermConditionOrIllness", "Select long-term conditions or illnesses and reduced ability to carry out daily activities");
        }




        // invalid values for fields
        if (!isPostcodeValid(model.PostCode.ToString()))
        {
            ModelState.AddModelError("PostCode", "Enter a full UK postcode");
        }
        string emailRegex = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
        if (model.EmailAddress != null && !Regex.IsMatch(model.EmailAddress, emailRegex))
        {
            ModelState.AddModelError("EmailAddress", "Enter an email address in the correct format, like name@example.com");
        }

        ValidateDateOfBirth(model.DateOfBirth);
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
            ModelState.AddModelError("EmailAddress", "Email address already exists and cannot be used");
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
            if (dateOfBirth.ToDateOnly() > today)
            {
                ModelState.AddModelError("DateOfBirth.Day", "Date of birth must be in the past");
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

    public async Task<List<AddressDetails>?> GetAddresses(string postcode)
    {
        List<AddressDetails> addresses = new List<AddressDetails>();
        string baseUrl = "https://szye9d9gqf.execute-api.eu-west-2.amazonaws.com/dev/api/address/postcode/";
        string username = "nihr-dte-study-api";
        string password = "0qvNuUymNt6o";

        using (HttpClient client = new HttpClient())
        {
            var authToken = Encoding.ASCII.GetBytes($"{username}:{password}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

            try
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl + postcode);

                if (response.IsSuccessStatusCode)
                {
                    var responseConent = response.Content.ReadAsStringAsync();
                    return MapResponseToAddressDetails(responseConent.Result.ToString());
                }
                else
                {
                    logger.LogError("An unsuccessful response was received during the postcode address lookup: " + response.ToString());
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving addresses via postcode lookup");
                throw new InvalidOperationException("Error retrieving addresses via postcode lookup");
            }
        }

        return addresses;
    }

    public List<AddressDetails> MapResponseToAddressDetails(string jsonResponse)
    {
        List<AddressDetails> addressDetailsList = JsonSerializer.Deserialize<List<AddressDetails>>(jsonResponse);
        return addressDetailsList;
    }
}
