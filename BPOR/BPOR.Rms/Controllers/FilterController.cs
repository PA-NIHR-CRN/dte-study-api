using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using BPOR.Rms.Models.Email;
using Newtonsoft.Json;
using BPOR.Rms.Models;
using BPOR.Rms.Services;

namespace BPOR.Rms.Controllers;

public class FilterController(ParticipantDbContext context, IFilterService filterService) : Controller
{
    public IActionResult Index(VolunteerFilterViewModel model, string? studyId)
    {
        SetSelectedStudy(model, studyId);
        SetStudyExclusionFilters(model);
        SetHealthConditionSelectList(model);

        if (TempData["Notification"] != null)
        {
            model.Notification =
                JsonConvert.DeserializeObject<NotificationBannerModel>(TempData["Notification"]?.ToString());
        }

        model.ShowStudyFilters = String.IsNullOrEmpty(studyId) ? false : true;

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> HandleFormsAsync(VolunteerFilterViewModel model, string action, CancellationToken cancellationToken = default)
    {
        return action switch
        {
            "FilterVolunteers" => await FilterVolunteersAsync(model, cancellationToken),
            "ClearFilters" => ClearFilters(model),
            _ => RedirectToAction("Index")
        };
    }

    public IActionResult ClearFilters(VolunteerFilterViewModel model)
    {
        TempData["Notification"] = JsonConvert.SerializeObject(new NotificationBannerModel
        {
            IsSuccess = true,
            Heading = "",
            Body = $"All previously applied filters have been removed.",
        });

        return RedirectToAction("Index", "Filter", new { studyId = model.SelectedStudyId });
    }

    private void SetHealthConditionSelectList(VolunteerFilterViewModel model)
    {
        model.HealthConditions = context.HealthConditions
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description }).OrderBy(x => x.Text)
            .ToList();
    }

    private void SetStudyExclusionFilters(VolunteerFilterViewModel model)
    {
        // Contacted Select
        bool? includeContacted = null;

        if (model.ExcludeContacted)
        {
            includeContacted = false;
        }

        if (model.IncludeContacted)
        {
            includeContacted = true;
        }

        model.VolunteersContacted = new List<SelectListItem>
        {
            new SelectListItem
                { Value = "3", Text = "No Preference", Selected = includeContacted == null ? true : false },
            new SelectListItem { Value = "1", Text = "Include", Selected = includeContacted == true ? true : false },
            new SelectListItem { Value = "2", Text = "Exclude", Selected = includeContacted == false ? true : false }
        };

        // Completed Registration Select
        bool? includeCompletedRegistration = null;

        if (model.ExcludeCompletedRegistration)
        {
            includeCompletedRegistration = false;
        }

        if (model.IncludeCompletedRegistration)
        {
            includeCompletedRegistration = true;
        }

        model.VolunteersCompletedRegistration = new List<SelectListItem>
        {
            new SelectListItem
                { Value = "3", Text = "No Preference", Selected = includeCompletedRegistration == null ? true : false },
            new SelectListItem
                { Value = "1", Text = "Include", Selected = includeCompletedRegistration == true ? true : false },
            new SelectListItem
                { Value = "2", Text = "Exclude", Selected = includeCompletedRegistration == false ? true : false }
        };

        // Recruited Select
        bool? includeRecruited = null;

        if (model.ExcludeRecruited)
        {
            includeRecruited = false;
        }

        if (model.IncludeRecruited)
        {
            includeRecruited = true;
        }

        model.VolunteersRecruited = new List<SelectListItem>
        {
            new SelectListItem
                { Value = "3", Text = "No Preference", Selected = includeRecruited == null ? true : false },
            new SelectListItem { Value = "1", Text = "Include", Selected = includeRecruited == true ? true : false },
            new SelectListItem { Value = "2", Text = "Exclude", Selected = includeRecruited == false ? true : false }
        };

        // Registered Interest Select
        bool? includeRegisteredInterest = null;

        if (model.ExcludeRegisteredInterest)
        {
            includeRegisteredInterest = false;
        }

        if (model.IncludeRegisteredInterest)
        {
            includeRegisteredInterest = true;
        }

        model.VolunteersRegisteredInterest = new List<SelectListItem>
        {
            new SelectListItem
                { Value = "3", Text = "No Preference", Selected = includeRegisteredInterest == null ? true : false },
            new SelectListItem
                { Value = "1", Text = "Include", Selected = includeRegisteredInterest == true ? true : false },
            new SelectListItem
                { Value = "2", Text = "Exclude", Selected = includeRegisteredInterest == false ? true : false }
        };
    }

    private void SetSelectedStudy(VolunteerFilterViewModel model, string studyId)
    {
        if (!String.IsNullOrEmpty(studyId))
        {
            Study selectedStudy = context.Studies.Where(x => x.Id == Convert.ToInt32(studyId)).FirstOrDefault();

            model.SelectedStudy = selectedStudy?.StudyName ?? string.Empty;
            model.SelectedStudyId = selectedStudy?.Id.ToString();
            model.SelectedStudyCPMSId = selectedStudy?.CpmsId.ToString();
        }
    }

    [HttpPost]
    public IActionResult SetupEmailCampaign(VolunteerFilterViewModel model)
    {
        DateTime? DateOfBirthFrom = model.AgeTo.HasValue ? DateTime.Today.AddYears(-model.AgeTo.Value) : null;
        DateTime? DateOfBirthTo = model.AgeFrom.HasValue ? DateTime.Today.AddYears(-model.AgeFrom.Value) : null;

        var filterCriteria = new FilterCriteria
        {
            Contacted = model.IncludeContacted,
            RegisteredInterest = model.IncludeRegisteredInterest,
            CompletedRegistration = model.IncludeCompletedRegistration,
            Recruited = model.IncludeRecruited,
            PostcodeDistricts = model.PostcodeDistricts?.Split(",").ToList(),
            RegistrationFromDate = ConstructDate(model.RegistrationFromDateYear, model.RegistrationFromDateMonth,
                model.RegistrationFromDateDay),
            RegistrationToDate = ConstructDate(model.RegistrationToDateYear, model.RegistrationToDateMonth,
                model.RegistrationToDateDay),
            DateOfBirthFrom = DateOfBirthFrom,
            DateOfBirthTo = DateOfBirthTo,
            GenderId = model.IsSexMale ? 1 : model.IsSexFemale ? 2 : 3,
            GenderIsSameAsSexRegisteredAtBirth = model.IsGenderSameAsSexRegisteredAtBirth_Yes ? true :
                model.IsGenderSameAsSexRegisteredAtBirth_No ? false : null,
            EthnicGroup = GetEthnicGroup(model),
            SearchRadiusMiles = model.SearchRadiusMiles,
            StudyId = model.StudyId
        };

        context.FilterCriterias.Add(filterCriteria);
        context.SaveChanges();

        // TODO do we need studyID?
        var campaignDetails = new SetupCampaignViewModel
        {
            FilterCriteriaId = filterCriteria.Id,
            StudyId = model.StudyId,
            MaxNumbers = model.VolunteerCount,
            StudyName = model.SelectedStudy
        };
        return RedirectToAction("SetupCampaign", "Email", campaignDetails);
    }

    private static string? GetEthnicGroup(VolunteerFilterViewModel model)
    {
        if (model.Ethnicity_Asian) return "Asian";
        if (model.Ethnicity_Black) return "Black";
        if (model.Ethnicity_Mixed) return "Mixed";
        if (model.Ethnicity_Other) return "Other";
        if (model.Ethnicity_White) return "White";

        return null;
    }

    [HttpPost]
    [RequestFormSizeLimit(valueCountLimit: 50000)]
    public async Task<IActionResult> FilterVolunteersAsync(VolunteerFilterViewModel model, CancellationToken cancellationToken = default)
    {
        ValidateRegistrationDates(model.RegistrationFromDateDay, model.RegistrationFromDateMonth,
            model.RegistrationFromDateYear,
            model.RegistrationToDateDay, model.RegistrationToDateMonth, model.RegistrationToDateYear);
        ValidatePostcodeDistricts(model.PostcodeDistricts);
        ValidateAge(model.AgeFrom, model.AgeTo);

        if (ModelState.IsValid)
        {
            var query = await filterService.FilterVolunteersAsync(model, cancellationToken);
            model.VolunteerCount = query.Count();
        }

        SetHealthConditionSelectList(model);

        SetStudyExclusionFilters(model);

        if (!String.IsNullOrEmpty(model.SelectedStudy))
        {
            SetSelectedStudy(model, model.SelectedStudyId);
            model.ShowStudyFilters = true;
        }

        return View("Index", model);
    }


    private void ValidatePostcodeDistricts(string? postcodeDistricts)
    {
        if (!String.IsNullOrEmpty(postcodeDistricts))
        {
            List<string> PostcodeDistrictsList = postcodeDistricts.Split(",").ToList();
            string pattern = @"^[A-Za-z]{1,2}[0-9]{1,2}[A-Za-z]?$";


            foreach (var item in PostcodeDistrictsList)
            {
                if (!Regex.IsMatch(item.Trim(), pattern))
                {
                    ModelState.AddModelError("PostcodeDistricts",
                        "Enter a UK postcode district in the correct format, like PO15 or LS1");
                }
            }
        }
    }

    private void ValidateAge(int? ageFrom, int? ageTo)
    {
        if (ageFrom > ageTo)
        {
            ModelState.AddModelError("AgeFrom", "The minimum age must be lower than the maximum age");
        }
    }


    private void ValidateRegistrationDates(int? RegistrationFromDateDay, int? RegistrationFromDateMonth,
        int? RegistrationFromDateYear,
        int? RegistrationToDateDay, int? RegistrationToDateMonth, int? RegistrationToDateYear)
    {
        if ((RegistrationFromDateDay != null && RegistrationFromDateMonth != null &&
             RegistrationFromDateYear != null) ||
            (RegistrationToDateDay != null && RegistrationToDateMonth != null && RegistrationToDateYear != null))
        {
            DateTime? RegistrationFromDate = ConstructDate(RegistrationFromDateYear, RegistrationFromDateMonth,
                RegistrationFromDateDay);
            DateTime? RegistrationToDate =
                ConstructDate(RegistrationToDateYear, RegistrationToDateMonth, RegistrationToDateDay);

            if (RegistrationFromDate.HasValue && RegistrationFromDate.Value.Date >= DateTime.Today)
            {
                ModelState.AddModelError("RegistrationFromDateDay",
                    "The date of volunteer registration must be before today");
            }

            if (RegistrationToDate.HasValue && RegistrationToDate.Value.Date >= DateTime.Today)
            {
                ModelState.AddModelError("RegistrationToDateDay",
                    "The date of volunteer registration must be before today");
            }

            if (RegistrationFromDateYear.HasValue && RegistrationFromDateYear < 2022)
            {
                ModelState.AddModelError("RegistrationFromDateYear", "Year must be a number that is 2022 or later");
            }

            if (RegistrationToDateYear.HasValue && RegistrationToDateYear < 2022)
            {
                ModelState.AddModelError("RegistrationToDateYear", "Year must be a number that is 2022 or later");
            }

            if (RegistrationFromDate.HasValue && RegistrationToDate.HasValue)
            {
                if (RegistrationFromDate > RegistrationToDate)
                {
                    ModelState.AddModelError("RegistrationFromDateDay",
                        "Registration 'From' date must be before 'To' date");
                }
            }
        }
        else
        {
            List<DateValues> fromDateValues = new List<DateValues>();
            List<DateValues> toDateValues = new List<DateValues>();

            if (RegistrationFromDateDay != null || RegistrationFromDateMonth != null ||
                RegistrationFromDateYear != null)
            {
                fromDateValues.Add(new DateValues
                    { Key = "RegistrationFromDateDay", Unit = "day", Value = RegistrationFromDateDay });
                fromDateValues.Add(new DateValues
                    { Key = "RegistrationFromDateMonth", Unit = "month", Value = RegistrationFromDateMonth });
                fromDateValues.Add(new DateValues
                    { Key = "RegistrationFromDateYear", Unit = "year", Value = RegistrationFromDateYear });

                foreach (var val in fromDateValues)
                {
                    if (val.Value == null)
                    {
                        ModelState.AddModelError(val.Key,
                            "The date of volunteer registration must include a " + val.Unit);
                    }
                }
            }

            if (RegistrationToDateDay != null || RegistrationToDateMonth != null || RegistrationToDateYear != null)
            {
                fromDateValues.Add(new DateValues
                    { Key = "RegistrationToDateDay", Unit = "day", Value = RegistrationToDateDay });
                fromDateValues.Add(new DateValues
                    { Key = "RegistrationToDateMonth", Unit = "month", Value = RegistrationToDateMonth });
                fromDateValues.Add(new DateValues
                    { Key = "RegistrationToDateYear", Unit = "year", Value = RegistrationToDateYear });

                foreach (var val in fromDateValues)
                {
                    if (val.Value == null)
                    {
                        ModelState.AddModelError(val.Key,
                            "The date of volunteer registration must include a " + val.Unit);
                    }
                }
            }
        }
    }

    private static DateTime? ConstructDate(int? year, int? month, int? day)
    {
        if (!year.HasValue || !month.HasValue || !day.HasValue)
            return null;

        try
        {
            return new DateTime(year.Value, month.Value, day.Value);
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }

    public class DateValues
    {
        public string Key { get; set; }
        public string Unit { get; set; }
        public int? Value { get; set; }
    }
}
