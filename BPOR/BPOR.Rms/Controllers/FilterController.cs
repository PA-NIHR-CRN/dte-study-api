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
using BPOR.Rms.Services;

namespace BPOR.Rms.Controllers;

public class FilterController(ParticipantDbContext context,
                              IPaginationService paginationService,
                              IHostEnvironment hostEnvironment,
                              TimeProvider timeProvider,
                              ICurrentUserProvider<User> currentUserProvider,
                              IVolunteerFilterService volunteerFilterService) : Controller
{
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

        if (ModelState.IsValid)
        {
            model.VolunteerCount =
                await volunteerFilterService.GetFilteredVolunteerCountAsync(model, cancellationToken);
            if (model.Testing.ShowResults)
            {
                model.Testing.VolunteerResults =
                    await volunteerFilterService.GetFilteredVolunteersForTestingAsync(model, paginationService,
                        cancellationToken);
                
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
}