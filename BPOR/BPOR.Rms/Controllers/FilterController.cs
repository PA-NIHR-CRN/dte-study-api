using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Models;
using BPOR.Rms.Services;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.Paging;

namespace BPOR.Rms.Controllers;

public class FilterController(ParticipantDbContext context, IFilterService filterService, IPaginationService paginationService) : Controller
{
    public async Task<IActionResult> Index(VolunteerFilterViewModel model, string? activity = null, CancellationToken cancellationToken = default)
    {

        if (activity == "FilterVolunteers")
        {
            await FilterVolunteersAsync(model, cancellationToken);
        }
        else if (activity == "ClearFilters")
        {
            model = ClearFilters(model);
        }

        SetSelectedStudy(model);
        SetStudyExclusionFilters(model);
        SetHealthConditionSelectList(model);

        return View(model);
    }


    protected VolunteerFilterViewModel ClearFilters(VolunteerFilterViewModel model)
    {
        TempData.AddSuccessNotification($"All previously applied filters have been removed.");

        ModelState.Clear();

        return new VolunteerFilterViewModel { StudyId = model.StudyId };
    }

    private void SetHealthConditionSelectList(VolunteerFilterViewModel model)
    {
        model.HealthConditions = context.HealthConditions
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description }).OrderBy(x => x.Text)
            .ToList();
    }

    private static void SetStudyExclusionFilters(VolunteerFilterViewModel model)
    {
        model.VolunteersContacted = GetTristateOptions();

        model.VolunteersCompletedRegistration = GetTristateOptions();

        model.VolunteersRecruited = GetTristateOptions();

        model.VolunteersRegisteredInterest = GetTristateOptions();
    }

    private static IEnumerable<SelectListItem> GetTristateOptions()
    {
        return [
                new SelectListItem { Value = string.Empty, Text = "No Preference" },
                new SelectListItem { Value = true.ToString(), Text = "Include" },
                new SelectListItem { Value = false.ToString(), Text = "Exclude" }
            ];
    }

    private void SetSelectedStudy(VolunteerFilterViewModel model)
    {
        if (model.StudyId is not null)
        {
            model.ShowStudyFilters = true;

            var selectedStudy = context.Studies.Where(x => x.Id == model.StudyId).Select(x => new { x.StudyName, x.CpmsId, x.IsRecruitingIdentifiableParticipants }).First();

            model.SelectedStudy = selectedStudy!.StudyName;
            model.SelectedStudyCPMSId = selectedStudy!.CpmsId;

            model.ShowRecruitedFilter = selectedStudy!.IsRecruitingIdentifiableParticipants;
        }
    }

    public IActionResult SetupEmailCampaign(VolunteerFilterViewModel model)
    {
        DateTime? dateOfBirthFrom = model.AgeTo.HasValue ? DateTime.Today.AddYears(-model.AgeTo.Value) : null;
        DateTime? dateOfBirthTo = model.AgeFrom.HasValue ? DateTime.Today.AddYears(-model.AgeFrom.Value) : null;

        var filterCriteria = new FilterCriteria
        {
            IncludeContacted = model.SelectedVolunteersContacted,
            IncludeRegisteredInterest = model.SelectedVolunteersRegisteredInterest,
            IncludeCompletedRegistration = model.SelectedVolunteersRegisteredInterest,
            IncludeRecruited = model.SelectedVolunteersRecruited,
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
            MaxNumbers = model.VolunteerCount == null ? 0 : model.VolunteerCount.Value,
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

    protected async Task FilterVolunteersAsync(VolunteerFilterViewModel model, CancellationToken cancellationToken = default)
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

            if (model.ShowResults)
            {
                model.VolunteerResults = await query.Select(x => new VolunteerResult
                {
                    Id = x.Id,
                    Email = x.Email,
                    Postcode = x.Address == null ? null : x.Address.Postcode,
                    AreasOfResearch = x.HealthConditions.Select(y => y.HealthCondition.Code).OrderBy(y => y).AsEnumerable(),
                    DateOfBirth = x.DateOfBirth,
                    Age = x.DateOfBirth.YearsTo(DateTime.Today),
                    Gender = x.Gender.Code,
                    Location = x.ParticipantLocation == null ? null : x.ParticipantLocation.Location,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    HasCompletedRegistration = x.Stage2CompleteUtc.HasValue,
                    HasRegistered = x.RegistrationConsentAtUtc,
                    EthnicGroup = x.EthnicGroup,
                    GenderIsSameAsSexRegisteredAtBirth = x.GenderIsSameAsSexRegisteredAtBirth,
                })
                .OrderBy(x => x.Id)
                .PageAsync(paginationService, cancellationToken);
            }
        }
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
