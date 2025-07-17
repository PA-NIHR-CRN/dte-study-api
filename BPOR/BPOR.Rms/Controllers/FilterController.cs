using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using Microsoft.AspNetCore.Mvc;
using BPOR.Rms.Models.Email;
using NIHR.Infrastructure.Paging;
using Z.EntityFramework.Plus;
using Rbec.Postcodes;
using BPOR.Rms.Startup;
using NIHR.GovUk.AspNetCore.Mvc;
using BPOR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using BPOR.Rms.Services;

namespace BPOR.Rms.Controllers;

public class FilterController(ParticipantDbContext context,
                              IPaginationService paginationService,
                              IHostEnvironment hostEnvironment,
                              TimeProvider timeProvider,
                              ICurrentUserProvider<User> currentUserProvider,
                              IVolunteerFilterService volunteerFilterService) : Controller
{
    private readonly DateOnly _today = DateOnly.FromDateTime(timeProvider.GetLocalNow().Date);

    private async Task<IActionResult> ViewIndex(VolunteerFilterViewModel model, CancellationToken cancellationToken)
    {
        bool isResearcher = currentUserProvider.User.HasRole(Domain.Enums.UserRole.Researcher);
        if (isResearcher)
        {
            return View("Unauthorised");
        }
        return View("index", model);
    }

    [HttpPost]
    public async Task<IActionResult> ClearFilters(VolunteerFilterViewModel model, CancellationToken cancellationToken)
    {
        this.AddSuccessNotification($"All previously applied filters have been removed.");
        ModelState.Clear();
        model = new VolunteerFilterViewModel { StudyId = model.StudyId };
        await PopulateStudyDetails(model, cancellationToken);
        return await ViewIndex(model, cancellationToken);
    }
    
    [HttpPost]
    public async Task<IActionResult> FilterVolunteers(VolunteerFilterViewModel model, CancellationToken cancellationToken)
    {
        await PopulateStudyDetails(model, cancellationToken);
        
        if (!(User.IsInRole("Tester") && User.IsInRole("Admin")))
        {
            model.Testing = new();
        }
        
        var results = await FilterVolunteersAsync(model, cancellationToken);
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

        return await ViewIndex(model, CancellationToken.None);
    }
    
    public async Task<IActionResult> Index(VolunteerFilterViewModel model, CancellationToken cancellationToken = default)
    {
        await PopulateStudyDetails(model, cancellationToken);
        return await ViewIndex(model, cancellationToken);
    }

    private async Task PopulateStudyDetails(VolunteerFilterViewModel model, CancellationToken cancellationToken)
    {
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
            model.ShowPreferredContactFilter = selectedStudy.IsRecruitingIdentifiableParticipants;

            if (!model.ShowPreferredContactFilter) {
                model.SelectedVolunteersPreferredContact = (int)ContactMethodId.Email;
            }
        }

        model.VolunteerCount = 0;
        model.Testing.VolunteerResults = Page<VolunteerResult>.Empty();

        if (activity == "FilterVolunteers")
        {
            if (ModelState.IsValid)
            {
                model.VolunteerCount = await volunteerFilterService.GetFilteredVolunteerCountAsync(model, cancellationToken);
                if (model.Testing.ShowResults)
                {
                    model.Testing.VolunteerResults = await volunteerFilterService.GetFilteredVolunteersForTestingAsync(model, paginationService, cancellationToken);
                }
            }
        }
        else if (activity == "ClearFilters")
        {
            model = ClearFilters(model);
        }

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
        this.AddSuccessNotification($"All previously applied filters have been removed.");

        ModelState.Clear();

        return new VolunteerFilterViewModel { StudyId = model.StudyId };
    }

    private async Task PopulateFilterIndexDataAsync(VolunteerFilterViewModel model, CancellationToken cancellationToken)
    {
        if (model.StudyId.HasValue)
        {
            var study = await context.Studies
                .Where(s => s.Id == model.StudyId)
                .Select(s => new { s.StudyName, s.CpmsId, s.IsRecruitingIdentifiableParticipants })
                .FirstOrDefaultAsync(cancellationToken);

            if (study != null)
            {
                model.StudyName = study.StudyName;
                model.StudyCpmsId = study.CpmsId;
                model.ShowRecruitedFilter = study.IsRecruitingIdentifiableParticipants;
                model.ShowPreferredContactFilter = study.IsRecruitingIdentifiableParticipants;

                if (!model.ShowPreferredContactFilter)
                {
                    model.SelectedVolunteersPreferredContact = (int)ContactMethodId.Email;
                }
            }
        }

        if (model.VolunteersPreferredContactItems == null || !model.VolunteersPreferredContactItems.Any())
        {
            model.VolunteersPreferredContactItems = VolunteerFilterViewModel.SetVolunteersPreferredContactItems();
        }

        if (model.VolunteersContactedItems == null || !model.VolunteersContactedItems.Any())
        {
            model.VolunteersContactedItems = VolunteerFilterViewModel.SetVolunteersContactedItems();
        }

        if (model.VolunteersRecruitedItems == null || !model.VolunteersRecruitedItems.Any())
        {
            model.VolunteersRecruitedItems = VolunteerFilterViewModel.SetVolunteersRecruitedItems();
        }

        if (model.VolunteersCompletedRegistrationItems == null || !model.VolunteersCompletedRegistrationItems.Any())
        {
            model.VolunteersCompletedRegistrationItems = VolunteerFilterViewModel.SetVolunteersCompletedRegistrationItems();
        }

        if (model.VolunteersRegisteredInterestItems == null || !model.VolunteersRegisteredInterestItems.Any())
        {
            model.VolunteersRegisteredInterestItems = VolunteerFilterViewModel.SetVolunteersRegisteredInterestItems();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> SetupCampaign(VolunteerFilterViewModel model, CancellationToken cancellationToken = default)
    {
        await PopulateStudyDetails(model, cancellationToken);

        if (!model.SelectedVolunteersPreferredContact.Equals((int)ContactMethodId.Email) && 
            !model.SelectedVolunteersPreferredContact.Equals((int)ContactMethodId.Letter) && 
            model.ShowPreferredContactFilter)
        {
            ModelState.AddModelError(nameof(model.SelectedVolunteersPreferredContact), "Select if the volunteers preferred contact method is email or letter");
            return await ViewIndex(model, cancellationToken);
        }

        var filterCriteria = new FilterCriteria
        {
            IncludeContacted = model.SelectedVolunteersContacted,
            IncludeRegisteredInterest = model.SelectedVolunteersRegisteredInterest,
            IncludeCompletedRegistration = model.SelectedVolunteersCompletedRegistration,
            IncludeRecruited = model.SelectedVolunteersRecruited,
            RegistrationFromDate = model.RegistrationFromDate.ToDateOnly()?.ToDateTime(TimeOnly.MinValue),
            RegistrationToDate = model.RegistrationToDate.ToDateOnly()?.ToDateTime(TimeOnly.MaxValue),
            AgeFrom = model.AgeRange.From,
            AgeTo = model.AgeRange.To,
            ContactMethodId = model.SelectedVolunteersPreferredContact,
            FullPostcode = model.PostcodeSearch.PostcodeRadiusSearch.FullPostcode?.ToString(),
            SearchRadiusMiles = model.PostcodeSearch.PostcodeRadiusSearch.SearchRadiusMiles,
            StudyId = model.StudyId,
            IncludeNoAreasOfInterest = model.IncludeNoAreasOfInterest,
            FilterAreaOfInterest = model.SelectedAreasOfInterest.Select(x => new FilterAreaOfInterest
            {
                HealthConditionId = x
            }).ToList(),
            // TODO bind this directly into the model as a collection
            FilterPostcode = model.PostcodeSearch.GetPostcodeDistricts().Select(x => new FilterPostcode { PostcodeFragment = x }).ToList(),
            FilterGender = model.GetGenderOptions().Select(x => new FilterGender { GenderId = (int)x }).ToList(), // TODO: support null gender
            FilterSexSameAsRegisteredAtBirth = GetSexSameAsRegisteredAtBirths(model),
            FilterEthnicGroup = GetEthnicGroups(model),
            FilterHasLongTermCondition = GetFilterHasLongTermCondition(model)
        };

        context.FilterCriterias.Add(filterCriteria);
        await context.SaveChangesAsync(cancellationToken);

        var campaignDetails = new SetupCampaignViewModel
        {
            FilterCriteriaId = filterCriteria.Id,
            StudyId = model.StudyId,
            MaxNumbers = model.VolunteerCount == null ? 0 : model.VolunteerCount.Value,
            StudyName = model.StudyName,
            ContactMethod = (ContactMethodId)model.SelectedVolunteersPreferredContact,
        };

        return RedirectToAction("Setup", "Campaign", campaignDetails);
    }

    private static List<T> Map<T>(IEnumerable<bool> inputList, Func<int, T> getOutput) =>
        inputList.Select((x, i) => x ? i + 1 : 0).Where(x => x > 0).Select(getOutput).ToList();


    private static List<FilterEthnicGroup> GetEthnicGroups(VolunteerFilterViewModel model) =>
        Map([model.Ethnicity_Asian, model.Ethnicity_Black, model.Ethnicity_Mixed, model.Ethnicity_White, model.Ethnicity_Other],
            x => new FilterEthnicGroup { EthnicGroupId = x });

    private static List<FilterSexSameAsRegisteredAtBirth> GetSexSameAsRegisteredAtBirths(VolunteerFilterViewModel model) =>
        Map([model.IsGenderSameAsSexRegisteredAtBirth_Yes, model.IsGenderSameAsSexRegisteredAtBirth_No, model.IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay],
            x => new FilterSexSameAsRegisteredAtBirth { YesNoPreferNotToSay = x });

    private static List<FilterHasLongTermCondition> GetFilterHasLongTermCondition(VolunteerFilterViewModel model) =>
    Map([model.HasLongTermCondition_Yes, model.HasLongTermCondition_No, model.HasLongTermCondition_PreferNotToSay],
        x => new FilterHasLongTermCondition { YesNoPreferNotToSay = x });

    protected async Task<FilterResults> FilterVolunteersAsync(VolunteerFilterViewModel model, CancellationToken token = default)
    {
        FilterResults results = new();

        if (ModelState.IsValid)
        {
            var query = context.Participants.FilterVolunteers(timeProvider, model);

            results.Count = query.DeferredCount().FutureValue();

            if (model.Testing.ShowResults)
            {
                var location = model.PostcodeSearch.PostcodeRadiusSearch.Location;
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
                    DistanceInMiles = location != null && x.ParticipantLocation != null ? x.ParticipantLocation.Location.Distance(location) / 1609.344 : null,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    HasCompletedRegistration = x.Stage2CompleteUtc.HasValue,
                    HasRegistered = x.RegistrationConsentAtUtc,
                    EthnicGroup = x.EthnicGroup,
                    GenderIsSameAsSexRegisteredAtBirth = x.GenderIsSameAsSexRegisteredAtBirth,
                    ContactMethod = x.ContactMethodId.FirstOrDefault().ContactMethodId
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
    public int Count { get; internal set; }
}