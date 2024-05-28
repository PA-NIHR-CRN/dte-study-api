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
using Z.EntityFramework.Plus;

namespace BPOR.Rms.Controllers;

public class FilterController(ParticipantDbContext context, IFilterService filterService, IPaginationService paginationService, ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

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

        model.VolunteersContacted = GetTristateOptions();
        model.VolunteersCompletedRegistration = GetTristateOptions();
        model.VolunteersRecruited = GetTristateOptions();
        model.VolunteersRegisteredInterest = GetTristateOptions();

        model.HealthConditions = context.HealthConditions
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description })
                .OrderBy(x => x.Text)
                .Future();

        if (model.StudyId is not null)
        {
            var selectedStudy = await context.Studies
                .Where(x => x.Id == model.StudyId)
                .Select(x => new { x.StudyName, x.CpmsId, x.IsRecruitingIdentifiableParticipants })
                .DeferredFirst()
                .ExecuteAsync(cancellationToken);

            model.ShowStudyFilters = true;

            model.SelectedStudy = selectedStudy.StudyName;
            model.SelectedStudyCPMSId = selectedStudy.CpmsId;

            model.ShowRecruitedFilter = selectedStudy.IsRecruitingIdentifiableParticipants;
        }

        return View(model);
    }

    private VolunteerFilterViewModel ClearFilters(VolunteerFilterViewModel model)
    {
        TempData.AddSuccessNotification($"All previously applied filters have been removed.");

        ModelState.Clear();

        return new VolunteerFilterViewModel { StudyId = model.StudyId };
    }

    private static IEnumerable<SelectListItem> GetTristateOptions()
    {
        return [
                new SelectListItem { Value = string.Empty, Text = "No Preference" },
                new SelectListItem { Value = true.ToString(), Text = "Include" },
                new SelectListItem { Value = false.ToString(), Text = "Exclude" }
            ];
    }

    [HttpPost]
    public async Task<IActionResult> SetupEmailCampaign(VolunteerFilterViewModel model, CancellationToken cancellationToken = default)
    {
        DateTime? dateOfBirthFrom = model.AgeTo.HasValue ? DateTime.Today.AddYears(-model.AgeTo.Value) : null;
        DateTime? dateOfBirthTo = model.AgeFrom.HasValue ? DateTime.Today.AddYears(-model.AgeFrom.Value) : null;

        var filterCriteria = new FilterCriteria
        {
            IncludeContacted = model.SelectedVolunteersContacted,
            IncludeRegisteredInterest = model.SelectedVolunteersRegisteredInterest,
            IncludeCompletedRegistration = model.SelectedVolunteersCompletedRegistration,
            IncludeRecruited = model.SelectedVolunteersRecruited,
            // TODO: custom model binding for broken up date.
            RegistrationFromDate = ConstructDate(model.RegistrationFromDateYear, model.RegistrationFromDateMonth,
                model.RegistrationFromDateDay),
            RegistrationToDate = ConstructDate(model.RegistrationToDateYear, model.RegistrationToDateMonth,
                model.RegistrationToDateDay),
            DateOfBirthFrom = dateOfBirthFrom,
            DateOfBirthTo = dateOfBirthTo,
            FullPostcode = model.FullPostcode,
            SearchRadiusMiles = model.SearchRadiusMiles,
            StudyId = model.StudyId,
            FilterAreaOfInterest = model.SelectedHealthConditions.Select(x => new FilterAreaOfInterest
            {
                HealthConditionId = x
            }).ToList(),
            // TODO bind this directly into the model as a collection
            FilterPostcode = GetPostcodes(model),
            FilterGender = GetGenders(model),
            FilterSexSameAsRegisteredAtBirth = GetSexSameAsRegisteredAtBirths(model),
            FilterEthnicGroup = GetEthnicGroups(model),
            IncludeNoAreasOfInterest = model.IncludeNoHealthConditions
        };

        context.FilterCriterias.Add(filterCriteria);
        await context.SaveChangesAsync(cancellationToken);

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

    private static List<T> Map<T>(IEnumerable<bool> inputList, Func<int, T> getOutput) =>
        inputList.Select((x, i) => x ? i + 1 : 0).Where(x => x > 0).Select(getOutput).ToList();


    private static List<FilterEthnicGroup> GetEthnicGroups(VolunteerFilterViewModel model) => 
        Map([model.Ethnicity_Asian, model.Ethnicity_Black, model.Ethnicity_Mixed, model.Ethnicity_White, model.Ethnicity_Other],
            x => new FilterEthnicGroup { EthnicGroupId = x });

    private static List<FilterSexSameAsRegisteredAtBirth> GetSexSameAsRegisteredAtBirths(VolunteerFilterViewModel model) => 
        Map([model.IsGenderSameAsSexRegisteredAtBirth_Yes, model.IsGenderSameAsSexRegisteredAtBirth_No, model.IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay],
            x => new FilterSexSameAsRegisteredAtBirth { YesNoPreferNotToSay = x });

    private static List<FilterGender> GetGenders(VolunteerFilterViewModel model) =>
        Map([model.IsSexMale, model.IsSexFemale],
            x => new FilterGender { GenderId = x });

    private static List<FilterPostcode> GetPostcodes(VolunteerFilterViewModel model) =>
        model.PostcodeDistricts?.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => new FilterPostcode { PostcodeFragment = x })?.ToList() ?? [];

    protected async Task FilterVolunteersAsync(VolunteerFilterViewModel model, CancellationToken cancellationToken = default)
    {
        ValidateAreasOfResearch(model.SelectedHealthConditions, model.IncludeNoHealthConditions);
        ValidateRegistrationDates(model.RegistrationFromDateDay, model.RegistrationFromDateMonth,
            model.RegistrationFromDateYear,
            model.RegistrationToDateDay, model.RegistrationToDateMonth, model.RegistrationToDateYear);
        ValidatePostcodeDistricts(model.PostcodeDistricts, model.FullPostcode);
        ValidateFullPostcode(model.FullPostcode, model.SearchRadiusMiles);
        ValidateAge(model.AgeFrom, model.AgeTo);

        if (ModelState.IsValid)
        {
            try
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
            catch(Exception ex)
            {
                ModelState.AddModelError("","An error occurred while filtering volunteers.");
                _logger.LogError(ex, "An error occurred while filtering volunteers.");
            }

            
        }
    }

    private void ValidateAreasOfResearch(List<int> selectedHealthConditions, bool includeNoHealthConditions)
    {
        if (selectedHealthConditions.Any() && includeNoHealthConditions)
        {
            ModelState.AddModelError("IncludeNoHealthConditions", "Cannot select areas of research and include no areas of research at the same time");
        }
    }

    private void ValidateFullPostcode(string? fullPostcode, double? searchRadiusMiles)
    {
        if (!String.IsNullOrEmpty(fullPostcode) && searchRadiusMiles == null)
        {
            ModelState.AddModelError("SearchRadiusMiles",
                        "Enter a radius");
        }

        if (String.IsNullOrEmpty(fullPostcode) && searchRadiusMiles != null)
        {
            ModelState.AddModelError("FullPostcode",
                        "Enter a postcode");
        }

        if (!String.IsNullOrEmpty(fullPostcode) && searchRadiusMiles == 0)
        {
            ModelState.AddModelError("SearchRadiusMiles",
                        "Search radius must be greater than 0");
        }
    }

    private void ValidatePostcodeDistricts(string? postcodeDistricts, string? fullPostcode)
    {
        if (!String.IsNullOrEmpty(postcodeDistricts) && !String.IsNullOrEmpty(fullPostcode))
        {
            ModelState.AddModelError("PostcodeDistricts",
                        "Postcode district search and Full postcode search cannot be applied at the same time");
        }

        if (!string.IsNullOrEmpty(postcodeDistricts))
        {
            var postcodeDistrictsList = postcodeDistricts.Split(",");
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
            // TODO: I think this is too restrictive
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
            var fromDateValues = new List<DateValues>();

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
        catch (ArgumentOutOfRangeException) // TODO: Don't like this
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
