using System.Diagnostics;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using BPOR.Rms.Models.Email;
using Newtonsoft.Json;
using BPOR.Rms.Models;
using BPOR.Rms.Services;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.Controllers;

public class FilterController(ParticipantDbContext context, IFilterService filterService) : Controller
{
    public IActionResult Index(VolunteerFilterViewModel model, int? studyId)
    {
        SetSelectedStudy(model, studyId);
        SetStudyExclusionFilters(model);
        SetHealthConditionSelectList(model);

        if (TempData["Notification"] != null)
        {
            model.Notification =
                JsonConvert.DeserializeObject<NotificationBannerModel>(TempData["Notification"]?.ToString());
        }

        model.ShowStudyFilters = model.StudyId is not null;

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(VolunteerFilterViewModel model, int? studyId, string action, CancellationToken cancellationToken = default)
    {
        return action switch
        {
            "FilterVolunteers" => await FilterVolunteersAsync(model, cancellationToken),
            "ClearFilters" => ClearFilters(model),
            _ => RedirectToAction("Index", new { studyId })
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

        return RedirectToAction("Index", "Filter", new { studyId = model.StudyId });
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

    private void SetSelectedStudy(VolunteerFilterViewModel model, int? studyId)
    {
        if (studyId is not null)
        {
            Study selectedStudy = context.Studies.Where(x => x.Id == studyId).FirstOrDefault();

            model.StudyId = selectedStudy?.Id;
            model.SelectedStudy = selectedStudy?.StudyName ?? string.Empty;
            model.SelectedStudyCPMSId = selectedStudy?.CpmsId;
        }
    }

    [HttpPost]
    public IActionResult SetupEmailCampaign(VolunteerFilterViewModel model)
    {
        DateTime? dateOfBirthFrom = model.AgeTo.HasValue ? DateTime.Today.AddYears(-model.AgeTo.Value) : null;
        DateTime? dateOfBirthTo = model.AgeFrom.HasValue ? DateTime.Today.AddYears(-model.AgeFrom.Value) : null;

        bool? volunteersContacted = null;
        bool? volunteersRegisteredInterest = null;
        bool? volunteersRecruited = null;
        bool? volunteersCompletedRegistration = null;

        if (model.SelectedVolunteersContacted == "1") { volunteersContacted = true; }
        else if (model.SelectedVolunteersContacted == "2") { volunteersContacted = false; }

        if (model.SelectedVolunteersRegisteredInterest == "1") { volunteersRegisteredInterest = true; }
        else if (model.SelectedVolunteersRegisteredInterest == "2") { volunteersRegisteredInterest = false; }

        if (model.SelectedVolunteersRecruited == "1") { volunteersRecruited = true; }
        else if (model.SelectedVolunteersRecruited == "2") { volunteersRecruited = false; }

        if (model.SelectedVolunteersCompletedRegistration == "1") { volunteersCompletedRegistration = true; }
        else if (model.SelectedVolunteersCompletedRegistration == "2") { volunteersCompletedRegistration = false; }

        var filterCriteria = new FilterCriteria
        {
            IncludeContacted = volunteersContacted,
            IncludeRegisteredInterest = volunteersRegisteredInterest,
            IncludeCompletedRegistration = volunteersCompletedRegistration,
            IncludeRecruited = volunteersRecruited,
            RegistrationFromDate = ConstructDate(model.RegistrationFromDateYear, model.RegistrationFromDateMonth,
                model.RegistrationFromDateDay),
            RegistrationToDate = ConstructDate(model.RegistrationToDateYear, model.RegistrationToDateMonth,
                model.RegistrationToDateDay),
            DateOfBirthFrom = dateOfBirthFrom,
            DateOfBirthTo = dateOfBirthTo,
            FullPostcode = model.FullPostcode,
            SearchRadiusMiles = model.SearchRadiusMiles,
            StudyId = model.StudyId
        };

        context.FilterCriterias.Add(filterCriteria);
        context.SaveChanges();

        SaveAreasOfResearchFilters(filterCriteria, model.SelectedHealthConditions);
        SavePostcodeDistrictFilters(filterCriteria, model.PostcodeDistricts);
        SaveGenderFilter(model, filterCriteria);
        SaveSexSameAsRegisteredAtBirthFilters(model, filterCriteria);
        SaveEthnicityFilters(model, filterCriteria);

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

    private void SaveAreasOfResearchFilters(FilterCriteria filterCriteria, List<string>? selectedHealthConditions)
    {
        if (selectedHealthConditions?.Count > 0)
        {
            foreach (var item in selectedHealthConditions)
            {
                var areaOfInterest = new FilterAreaOfInterest
                {
                    FilterCriteriaId = filterCriteria.Id,
                    HealthConditionId = Convert.ToInt32(item)
                };

                context.FilterAreaOfInterest.AddRange(areaOfInterest);
            }
            context.SaveChanges();
        }
    }

    private void SavePostcodeDistrictFilters(FilterCriteria filterCriteria, string? postcodeDistricts = null)
    {
        if (!String.IsNullOrEmpty(postcodeDistricts))
        {
            var postcodeFragments = postcodeDistricts.Split(",").ToList();
            var filterPostcodeFragments = new List<FilterPostcode>();

            foreach (var frag in postcodeFragments)
            {
                var item = new FilterPostcode
                {
                    FilterCriteriaId = filterCriteria.Id,
                    PostcodeFragment = frag
                };
                filterPostcodeFragments.Add(item);
            }
            context.FilterPostcode.AddRange(filterPostcodeFragments);
            context.SaveChanges();
        }
    }

    private void SaveGenderFilter(VolunteerFilterViewModel model, FilterCriteria filterCriteria)
    {
        if (model.IsSexMale || model.IsSexFemale)
        {
            var genderList = new List<FilterGender>();

            if (model.IsSexMale)
            {
                var gender = new FilterGender
                {
                    FilterCriteriaId = filterCriteria.Id,
                    GenderId = 1
                };
                genderList.Add(gender);
            }

            if (model.IsSexFemale)
            {
                var gender = new FilterGender
                {
                    FilterCriteriaId = filterCriteria.Id,
                    GenderId = 2
                };
                genderList.Add(gender);
            }

            context.FilterGender.AddRange(genderList);
            context.SaveChanges();
        }
    }

    private void SaveSexSameAsRegisteredAtBirthFilters(VolunteerFilterViewModel model, FilterCriteria filterCriteria)
    {
        if (model.IsGenderSameAsSexRegisteredAtBirth_Yes || model.IsGenderSameAsSexRegisteredAtBirth_No || model.IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay)
        {
            var sexRegisteredAtBirthList = new List<FilterSexSameAsRegisteredAtBirth>();

            if (model.IsGenderSameAsSexRegisteredAtBirth_Yes)
            {
                var item = new FilterSexSameAsRegisteredAtBirth
                {
                    FilterCriteriaId = filterCriteria.Id,
                    YesNoPreferNotToSay = 1
                };
                sexRegisteredAtBirthList.Add(item);
            }

            if (model.IsGenderSameAsSexRegisteredAtBirth_No)
            {
                var item = new FilterSexSameAsRegisteredAtBirth
                {
                    FilterCriteriaId = filterCriteria.Id,
                    YesNoPreferNotToSay = 2
                };
                sexRegisteredAtBirthList.Add(item);
            }

            if (model.IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay)
            {
                var item = new FilterSexSameAsRegisteredAtBirth
                {
                    FilterCriteriaId = filterCriteria.Id,
                    YesNoPreferNotToSay = 3
                };
                sexRegisteredAtBirthList.Add(item);
            }

            context.FilterSexSameAsRegisteredAtBirth.AddRange(sexRegisteredAtBirthList);
            context.SaveChanges();
        }
    }

    private void SaveEthnicityFilters(VolunteerFilterViewModel model, FilterCriteria filterCriteria)
    {
        if (model.Ethnicity_Asian || model.Ethnicity_Black || model.Ethnicity_Mixed || model.Ethnicity_Other || model.Ethnicity_White)
        {
            var ethnicGroups = new List<FilterEthnicGroup>();

            if (model.Ethnicity_Asian)
            {
                var ethnicity = new FilterEthnicGroup
                {
                    EthnicGroupId = 1,
                    FilterCriteriaId = filterCriteria.Id
                };
                ethnicGroups.Add(ethnicity);
            }

            if (model.Ethnicity_Black)
            {
                var ethnicity = new FilterEthnicGroup
                {
                    EthnicGroupId = 2,
                    FilterCriteriaId = filterCriteria.Id
                };
                ethnicGroups.Add(ethnicity);
            }

            if (model.Ethnicity_Mixed)
            {
                var ethnicity = new FilterEthnicGroup
                {
                    EthnicGroupId = 3,
                    FilterCriteriaId = filterCriteria.Id
                };
                ethnicGroups.Add(ethnicity);
            }

            if (model.Ethnicity_White)
            {
                var ethnicity = new FilterEthnicGroup
                {
                    EthnicGroupId = 4,
                    FilterCriteriaId = filterCriteria.Id
                };
                ethnicGroups.Add(ethnicity);
            }

            if (model.Ethnicity_Other)
            {
                var ethnicity = new FilterEthnicGroup
                {
                    EthnicGroupId = 5,
                    FilterCriteriaId = filterCriteria.Id
                };
                ethnicGroups.Add(ethnicity);
            }

            context.FilterEthnicGroup.AddRange(ethnicGroups);
            context.SaveChanges();
        }
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
            model.VolunteerCount = await query.CountAsync(cancellationToken);
        }

        SetHealthConditionSelectList(model);

        SetStudyExclusionFilters(model);

        if (!string.IsNullOrEmpty(model.SelectedStudy))
        {
            SetSelectedStudy(model, model.StudyId);
            model.ShowStudyFilters = true;
        }

        return View("Index", model);
    }


    private void ValidatePostcodeDistricts(string? postcodeDistricts)
    {
        if (!string.IsNullOrEmpty(postcodeDistricts))
        {
            List<string> postcodeDistrictsList = postcodeDistricts.Split(",").ToList();
            string pattern = @"^[A-Za-z]{1,2}[0-9]{1,2}[A-Za-z]?$";


            foreach (var item in postcodeDistrictsList)
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


    private void ValidateRegistrationDates(int? registrationFromDateDay, int? registrationFromDateMonth,
        int? registrationFromDateYear,
        int? registrationToDateDay, int? registrationToDateMonth, int? registrationToDateYear)
    {
        if ((registrationFromDateDay != null && registrationFromDateMonth != null &&
             registrationFromDateYear != null) ||
            (registrationToDateDay != null && registrationToDateMonth != null && registrationToDateYear != null))
        {
            DateTime? registrationFromDate = ConstructDate(registrationFromDateYear, registrationFromDateMonth,
                registrationFromDateDay);
            DateTime? registrationToDate =
                ConstructDate(registrationToDateYear, registrationToDateMonth, registrationToDateDay);

            if (registrationFromDate.HasValue && registrationFromDate.Value.Date >= DateTime.Today)
            {
                ModelState.AddModelError("RegistrationFromDateDay",
                    "The date of volunteer registration must be before today");
            }

            if (registrationToDate.HasValue && registrationToDate.Value.Date >= DateTime.Today)
            {
                ModelState.AddModelError("RegistrationToDateDay",
                    "The date of volunteer registration must be before today");
            }

            if (registrationFromDateYear.HasValue && registrationFromDateYear < 2022)
            {
                ModelState.AddModelError("RegistrationFromDateYear", "Year must be a number that is 2022 or later");
            }

            if (registrationToDateYear.HasValue && registrationToDateYear < 2022)
            {
                ModelState.AddModelError("RegistrationToDateYear", "Year must be a number that is 2022 or later");
            }

            if (registrationFromDate.HasValue && registrationToDate.HasValue)
            {
                if (registrationFromDate > registrationToDate)
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

            if (registrationFromDateDay != null || registrationFromDateMonth != null ||
                registrationFromDateYear != null)
            {
                fromDateValues.Add(new DateValues
                { Key = "RegistrationFromDateDay", Unit = "day", Value = registrationFromDateDay });
                fromDateValues.Add(new DateValues
                { Key = "RegistrationFromDateMonth", Unit = "month", Value = registrationFromDateMonth });
                fromDateValues.Add(new DateValues
                { Key = "RegistrationFromDateYear", Unit = "year", Value = registrationFromDateYear });

                foreach (var val in fromDateValues)
                {
                    if (val.Value == null)
                    {
                        ModelState.AddModelError(val.Key,
                            "The date of volunteer registration must include a " + val.Unit);
                    }
                }
            }

            if (registrationToDateDay != null || registrationToDateMonth != null || registrationToDateYear != null)
            {
                fromDateValues.Add(new DateValues
                { Key = "RegistrationToDateDay", Unit = "day", Value = registrationToDateDay });
                fromDateValues.Add(new DateValues
                { Key = "RegistrationToDateMonth", Unit = "month", Value = registrationToDateMonth });
                fromDateValues.Add(new DateValues
                { Key = "RegistrationToDateYear", Unit = "year", Value = registrationToDateYear });

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
