using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using Microsoft.AspNetCore.Mvc;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Models;
using NIHR.Infrastructure.Paging;
using Z.EntityFramework.Plus;
using Rbec.Postcodes;
using NIHR.Infrastructure.Models;
using NIHR.Infrastructure;
using NetTopologySuite.Geometries;
using BPOR.Domain.Entities.Configuration;
using BPOR.Rms.Startup;

namespace BPOR.Rms.Controllers;

public class FilterController(ParticipantDbContext context, IPaginationService paginationService, IHostEnvironment hostEnvironment, TimeProvider timeProvider, IPostcodeMapper locationApiClient, ICurrentUserProvider<User> currentUserProvider) : Controller
{
    private readonly DateOnly _today = DateOnly.FromDateTime(timeProvider.GetLocalNow().Date);

    public async Task<IActionResult> Index(VolunteerFilterViewModel model, string? activity = null, CancellationToken cancellationToken = default)
    {
        bool isResearcher = currentUserProvider?.User?.UserRoles.Any(r => r.RoleId == (int)Domain.Enums.UserRole.Researcher) ?? false;
        if (isResearcher)
        {
            ViewData["ShowBackLink"] = true;
            return View("Unauthorised");
        }
        FilterResults results = new();

        if (!(User.IsInRole("Tester") && User.IsInRole("Admin")))
        {
            model.Testing = new();
        }

        if (activity == "FilterVolunteers")
        {
            results = await FilterVolunteersAsync(model, cancellationToken);
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

        model.VolunteerCount = results.Count?.Value;
        model.Testing.VolunteerResults = results.Items?.Value ?? Page<VolunteerResult>.Empty();

        if (hostEnvironment.IsProduction())
        {
            foreach (var x in model.Testing.VolunteerResults)
            {
                if (Postcode.TryParse(x.Postcode, out var postcode))
                {
                    x.Postcode = postcode.ToString().Split(' ').First();
                }

                x.FirstName = string.Empty;
                x.LastName = string.Empty;
                x.Email = string.Empty;
                x.DateOfBirth = null;
            }
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
        var dobRange = _today.GetDatesWithinYearRange(model.AgeFrom, model.AgeTo);

        var filterCriteria = new FilterCriteria
        {
            IncludeContacted = model.SelectedVolunteersContacted,
            IncludeRegisteredInterest = model.SelectedVolunteersRegisteredInterest,
            IncludeCompletedRegistration = model.SelectedVolunteersCompletedRegistration,
            IncludeRecruited = model.SelectedVolunteersRecruited,
            RegistrationFromDate = model.RegistrationFromDate.ToDateOnly()?.ToDateTime(TimeOnly.MinValue),
            RegistrationToDate = model.RegistrationToDate.ToDateOnly()?.ToDateTime(TimeOnly.MaxValue),
            DateOfBirthFrom = dobRange.From?.ToDateTime(TimeOnly.MinValue),
            DateOfBirthTo = dobRange.To?.ToDateTime(TimeOnly.MaxValue),
            FullPostcode = model.FullPostcode,
            SearchRadiusMiles = model.SearchRadiusMiles,
            StudyId = model.StudyId,
            IncludeNoAreasOfInterest = model.IncludeNoAreasOfInterest,
            FilterAreaOfInterest = model.SelectedAreasOfInterest.Select(x => new FilterAreaOfInterest
            {
                HealthConditionId = x
            }).ToList(),
            // TODO bind this directly into the model as a collection
            FilterPostcode = model.GetPostcodeDistricts().Select(x => new FilterPostcode { PostcodeFragment = x }).ToList(),
            FilterGender = model.GetGenderOptions().Select(x => new FilterGender { GenderId = (int)x }).ToList(), // TODO: support null gender
            FilterSexSameAsRegisteredAtBirth = GetSexSameAsRegisteredAtBirths(model),
            FilterEthnicGroup = GetEthnicGroups(model),
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

    protected async Task<FilterResults> FilterVolunteersAsync(VolunteerFilterViewModel model, CancellationToken token = default)
    {
        FilterResults results = new();

        if (ModelState.IsValid)
        {
            // TODO: provide more consistent location lookup
            // possibly in middleware.
            CoordinatesModel? location = null;
            if (model.FullPostcode is not null && model.SearchRadiusMiles is not null && model.SearchRadiusMiles > 0)
            {
                location = await locationApiClient.GetCoordinatesFromPostcodeAsync(model.FullPostcode, token);

                if (location is null)
                {
                    ModelState.AddModelError(nameof(model.FullPostcode), $"Unable to determine the location of the postcode '{model.FullPostcode}'.");

                    return results;
                }
            }

            var query = context.Participants.FilterVolunteers(timeProvider, model, location);

            results.Count = query.DeferredCount().FutureValue();

            if (model.Testing.ShowResults)
            {
                var point = location is not null ? new Point(location.Longitude, location.Latitude) { SRID = ParticipantLocationConfiguration.LocationSrid } : null;

                results.Items = query.Select(x => new VolunteerResult
                {
                    Id = x.Id,
                    Email = x.Email,
                    Postcode = x.Address == null ? null : x.Address.Postcode,
                    AreasOfResearch = x.HealthConditions.Select(y => y.HealthCondition.Code).OrderBy(y => y).AsEnumerable(),
                    DateOfBirth = x.DateOfBirth,
                    Age = x.DateOfBirth.YearsTo(_today),
                    Gender = x.Gender.Code,
                    Location = x.ParticipantLocation == null ? null : x.ParticipantLocation.Location,
                    DistanceInMiles = point != null && x.ParticipantLocation != null ? x.ParticipantLocation.Location.Distance(point) / 1609.344 : null,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    HasCompletedRegistration = x.Stage2CompleteUtc.HasValue,
                    HasRegistered = x.RegistrationConsentAtUtc,
                    EthnicGroup = x.EthnicGroup,
                    GenderIsSameAsSexRegisteredAtBirth = x.GenderIsSameAsSexRegisteredAtBirth,
                })
                .OrderBy(x => x.Id)
                .DeferredPage(paginationService);
            }
        }

        return results;
    }
}

public class FilterResults
{
    public PageDeferred<VolunteerResult>? Items { get; internal set; }
    public QueryFutureValue<int>? Count { get; internal set; }
}