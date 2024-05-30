using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Models;
using BPOR.Rms.Services;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.Paging;
using Z.EntityFramework.Plus;
using Rbec.Postcodes;

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

        if (model.StudyId is not null)
        {
            var selectedStudy = await context.Studies
                .Where(x => x.Id == model.StudyId)
                .Select(x => new { x.StudyName, x.CpmsId, x.IsRecruitingIdentifiableParticipants })
                .DeferredFirst()
                .ExecuteAsync(cancellationToken);

            model.StudyName = selectedStudy.StudyName;
            model.StudyCpmsId = selectedStudy.CpmsId;

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
            RegistrationFromDate = model.RegistrationFromDate.ToDateOnly()?.ToDateTime(TimeOnly.MinValue),
            RegistrationToDate = model.RegistrationToDate.ToDateOnly()?.ToDateTime(TimeOnly.MaxValue),
            DateOfBirthFrom = dateOfBirthFrom,
            DateOfBirthTo = dateOfBirthTo,
            FullPostcode = model.FullPostcode,
            SearchRadiusMiles = model.SearchRadiusMiles,
            StudyId = model.StudyId,
            FilterAreaOfInterest = model.SelectedAreasOfInterest.Select(x => new FilterAreaOfInterest
            {
                HealthConditionId = x
            }).ToList(),
            // TODO bind this directly into the model as a collection
            FilterPostcode = GetPostcodes(model),
            FilterGender = GetGenders(model),
            FilterSexSameAsRegisteredAtBirth = GetSexSameAsRegisteredAtBirths(model),
            FilterEthnicGroup = GetEthnicGroups(model),
            IncludeNoAreasOfInterest = model.IncludeNoAreasOfInterest
        };

        context.FilterCriterias.Add(filterCriteria);
        await context.SaveChangesAsync(cancellationToken);

        // TODO do we need studyID?
        var campaignDetails = new SetupCampaignViewModel
        {
            FilterCriteriaId = filterCriteria.Id,
            StudyId = model.StudyId,
            MaxNumbers = model.VolunteerCount == null ? 0 : model.VolunteerCount.Value,
            StudyName = model.StudyName
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
        ValidateRegistrationDates(model.RegistrationFromDate, model.RegistrationToDate);
        ValidatePostcodeDistricts(model.PostcodeDistricts, model.FullPostcode);
        ValidateFullPostcode(model.FullPostcode, model.SearchRadiusMiles);
        ValidateAge(model.AgeFrom, model.AgeTo);

        if (ModelState.IsValid)
        {
            try
            {
                var query = await filterService.FilterVolunteersAsync(model, cancellationToken);
                model.VolunteerCount = await query.CountAsync(cancellationToken);

                if (model.Testing.ShowResults)
                {
                    model.Testing.VolunteerResults = await query.Select(x => new VolunteerResult
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while filtering volunteers.");
                _logger.LogError(ex, "An error occurred while filtering volunteers.");
            }


        }
    }

    private void ValidateFullPostcode(string? fullPostcode, double? searchRadiusMiles)
    {

        if (!String.IsNullOrEmpty(fullPostcode))
        {
            if (!Postcode.TryParse(fullPostcode, out Postcode postcode))
            {
                ModelState.AddModelError("FullPostcode",
                        "Enter a full UK postcode");
            }
        }

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

    private void ValidateRegistrationDates(GovUkDate registrationFromDate, GovUkDate registrationToDate)
    {
        if ((registrationFromDate.Day != null && registrationFromDate.Month != null &&
             registrationFromDate.Year != null) ||
            (registrationToDate.Day != null && registrationToDate.Month != null && registrationToDate.Year != null))
        {

            if (registrationFromDate.ToDateOnly() > DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("RegistrationFromDateDay",
                    "The date of volunteer registration must be on or before today");
            }

            if (registrationToDate.ToDateOnly() > DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("RegistrationToDateDay",
                    "The date of volunteer registration must be on or before today");
            }

            if (registrationFromDate.Year.HasValue && registrationFromDate.Year < 2022)
            {
                ModelState.AddModelError("RegistrationFromDateYear", "Year must be a number that is 2022 or later");
            }

            if (registrationToDate.Year.HasValue && registrationToDate.Year < 2022)
            {
                ModelState.AddModelError("RegistrationToDateYear", "Year must be a number that is 2022 or later");
            }

            if (registrationFromDate.ToDateOnly().HasValue && registrationToDate.ToDateOnly().HasValue)
            {
                if (registrationFromDate.ToDateOnly() > registrationToDate.ToDateOnly())
                {
                    ModelState.AddModelError("RegistrationFromDateDay",
                        "Registration 'From' date must be on or before 'To' date");
                }
            }
        }
        else
        {
            var fromDateValues = new List<DateValues>();

            if (registrationFromDate.Day != null || registrationFromDate.Month != null ||
                registrationFromDate.Year != null)
            {
                fromDateValues.Add(new DateValues
                { Key = "RegistrationFromDate.Day", Unit = "day", Value = registrationFromDate.Day });
                fromDateValues.Add(new DateValues
                { Key = "RegistrationFromDate.Month", Unit = "month", Value = registrationFromDate.Month });
                fromDateValues.Add(new DateValues
                { Key = "RegistrationFromDate.Year", Unit = "year", Value = registrationFromDate.Year });

                foreach (var val in fromDateValues)
                {
                    if (val.Value == null)
                    {
                        ModelState.AddModelError(val.Key,
                            "The date of volunteer registration must include a " + val.Unit);
                    }
                }
            }

            if (registrationToDate.Day != null || registrationToDate.Month != null || registrationToDate.Year != null)
            {
                fromDateValues.Add(new DateValues
                { Key = "RegistrationToDate.Day", Unit = "day", Value = registrationToDate.Day });
                fromDateValues.Add(new DateValues
                { Key = "RegistrationToDate.Month", Unit = "month", Value = registrationToDate.Month });
                fromDateValues.Add(new DateValues
                { Key = "RegistrationToDate.Year", Unit = "year", Value = registrationToDate.Year });

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

    public class DateValues
    {
        public string Key { get; set; }
        public string Unit { get; set; }
        public int? Value { get; set; }
    }
}
