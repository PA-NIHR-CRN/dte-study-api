using BPOR.Domain.Entities;
using BPOR.Domain.Entities.Configuration;
using BPOR.Rms.Mappers;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Filter;
using BPOR.Rms.Models.Volunteer;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure;
using NIHR.Infrastructure.Paging;
using BPOR.Domain.Extensions;
using Microsoft.Extensions.Options;
using NIHR.GovUk.AspNetCore.Mvc;
using Rbec.Postcodes;
using System.Threading;

namespace BPOR.Rms.Services
{
    /// <summary>
    /// Responsible for filtering volunteers and returning both random samples, counts and tester results.
    /// </summary>
    public class VolunteerFilterService (
        ILogger<VolunteerFilterService> logger, 
        ParticipantDbContext context, 
        TimeProvider timeProvider, 
        IPostcodeMapper locationApiClient,
        IOptions<VolunteerFilterServiceOptions> options) : IVolunteerFilterService
    {

        public async Task<VolunteerFilterViewModel> MapToFilterModelAsync(FilterCriteria criteria, CancellationToken cancellationToken)
        {
            var viewModel = new VolunteerFilterViewModel
            {
                StudyId = criteria.StudyId ?? 0,
                PostcodeSearch = new PostcodeSearchModel
                {
                    PostcodeRadiusSearch = new PostcodeRadiusSearchModel
                    {
                        FullPostcode = string.IsNullOrEmpty(criteria.FullPostcode)
                            ? null
                            : Postcode.Parse(criteria.FullPostcode),
                        SearchRadiusMiles = criteria.SearchRadiusMiles,
                    },
                    PostcodeDistricts = string.Join(", ", criteria.FilterPostcode.Select(f => f.PostcodeFragment))
                },
                SelectedVolunteersCompletedRegistration = criteria.IncludeCompletedRegistration,
                SelectedVolunteersContacted = criteria.IncludeContacted,
                SelectedVolunteersRecruited = criteria.IncludeRecruited,
                SelectedVolunteersRegisteredInterest = criteria.IncludeRegisteredInterest,
                RegistrationFromDate = GovUkDate.FromDateTime(criteria.RegistrationFromDate),
                RegistrationToDate = GovUkDate.FromDateTime(criteria.RegistrationToDate),

                // TODO: ShowRecruitedFilter, StudyCpmsId, StudyName need to be initialised. Might be a job of a component / tag helper.
                AgeRange = new AgeRange
                {
                    From = criteria.AgeFrom,
                    To = criteria.AgeTo,
                },
                SelectedVolunteersPreferredContact = criteria.ContactMethodId,
                IsSexMale = criteria.FilterGender.Any(fg => fg.GenderId == 1),
                IsSexFemale = criteria.FilterGender.Any(fg => fg.GenderId == 2),

                IsGenderSameAsSexRegisteredAtBirth_Yes =
                    criteria.FilterSexSameAsRegisteredAtBirth.Any(f => f.YesNoPreferNotToSay == 1),
                IsGenderSameAsSexRegisteredAtBirth_No =
                    criteria.FilterSexSameAsRegisteredAtBirth.Any(f => f.YesNoPreferNotToSay == 2),
                IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay =
                    criteria.FilterSexSameAsRegisteredAtBirth.Any(f => f.YesNoPreferNotToSay == 3),

                SelectedAreasOfInterest = criteria.FilterAreaOfInterest.Select(f => f.HealthConditionId).ToList(),

                Ethnicity_Asian = criteria.FilterEthnicGroup.Any(f => f.EthnicGroupId == 1),
                Ethnicity_Black = criteria.FilterEthnicGroup.Any(f => f.EthnicGroupId == 2),
                Ethnicity_Mixed = criteria.FilterEthnicGroup.Any(f => f.EthnicGroupId == 3),
                Ethnicity_White = criteria.FilterEthnicGroup.Any(f => f.EthnicGroupId == 4),
                Ethnicity_Other = criteria.FilterEthnicGroup.Any(f => f.EthnicGroupId == 5),
                
                HasLongTermCondition_Yes =
                    criteria.FilterHasLongTermCondition.Any(f => f.YesNoPreferNotToSay == 1),
                HasLongTermCondition_No =
                    criteria.FilterHasLongTermCondition.Any(f => f.YesNoPreferNotToSay == 2),
                HasLongTermCondition_PreferNotToSay =
                    criteria.FilterHasLongTermCondition.Any(f => f.YesNoPreferNotToSay == 3),


                IncludeNoAreasOfInterest = criteria.IncludeNoAreasOfInterest
            };

            if (criteria.FullPostcode is not null && criteria.SearchRadiusMiles is not null && criteria.SearchRadiusMiles > 0)
            {
                var location = await locationApiClient.GetCoordinatesFromPostcodeAsync(criteria.FullPostcode, cancellationToken);
                viewModel.PostcodeSearch.PostcodeRadiusSearch.Location = new Point(location.Longitude, location.Latitude) { SRID = ParticipantLocationConfiguration.LocationSrid };
            }

            return viewModel;
        }

        public async Task<List<CampaignParticipantDetails>> GetFilteredVolunteersAsync(FilterCriteria dbFilter,
            int? targetGroupSize, CancellationToken cancellationToken)
        {
            var filter = await MapToFilterModelAsync(dbFilter, cancellationToken);

            // TODO: save the original search location co-ordinates in the FilterCriteria

            var result = new List<CampaignParticipantDetails>();

            int seed = Random.Shared.Next();

            double pageMin = 0.0;
            double pageSize = options.Value.InitialPageSize;

            while (pageMin < 1.0 && (!targetGroupSize.HasValue || targetGroupSize.Value > result.Count))
            {

                double pageMax = pageMin + pageSize;
                var volunteerQuery = context
                    .GetRandomSampleOfParticipants(seed, pageMin, pageMax)
                    .FilterVolunteers(timeProvider, filter)
                    .Where(v => !string.IsNullOrEmpty(v.Email)); // TODO: This is suspect? Why are we always excluding participants with no email address?

                if (targetGroupSize.HasValue)
                {
                    volunteerQuery = volunteerQuery.Take(targetGroupSize.Value - result.Count);
                }

                try
                {
                    result.AddRange(await volunteerQuery.AsCampaignParticipant().ToArrayAsync(cancellationToken));
                    pageMin = pageMax;
                }
                catch (TimeoutException)
                {
                    if (pageSize < options.Value.MinPageSize)
                    {
                        logger.LogError("Volunteer Filter query timed out for filter {filterId} and page size {pageSize}.", dbFilter.Id, pageSize);
                        throw;
                    }
                    else
                    {
                        logger.LogWarning("Volunteer Filter query timed out for filter {filterId} and page size {pageSize} - this operation will be retried with a smaller window", dbFilter.Id, pageSize);
                        pageSize *= options.Value.TimeoutPageSizeFactor;
                    }
                }

            }

            return result;
        }

        public async Task<int> GetFilteredVolunteerCountAsync(VolunteerFilterViewModel model, CancellationToken token = default)
        {
            int filteredParticipantCount = 0;
            int batchSize = options.Value.FilterCountBatchSize;

            var maxParticipantId = await context.Participants.Select(i => i.Id).MaxAsync();

            for (int startId = 0; startId <= maxParticipantId; startId += batchSize)
            {
                filteredParticipantCount += await context.Participants.AsNoTracking().Where(p => p.Id >= startId && p.Id < startId + batchSize).FilterVolunteers(timeProvider, model).CountAsync();
            }

            return filteredParticipantCount;
        }

        public async Task<PageDeferred<VolunteerResult>> GetFilteredVolunteersForTestingAsync(
            VolunteerFilterViewModel model, IPaginationService paginationService, CancellationToken token = default)
        {
            int batchSize = options.Value.FilterCountBatchSize;

            DateOnly today = DateOnly.FromDateTime(timeProvider.GetLocalNow().Date);

            var location = model.PostcodeSearch.PostcodeRadiusSearch.Location;
            var result = context.Participants.FilterVolunteers(timeProvider, model).Select(x => new VolunteerResult
            {
                Id = x.Id,
                Email = x.Email,
                Postcode = x.Address == null ? null : x.Address.Postcode,
                AreasOfResearch = x.HealthConditions.Select(y => y.HealthCondition.Code).OrderBy(y => y).AsEnumerable(),
                DateOfBirth = x.DateOfBirth,
                Age = x.DateOfBirth.YearsTo(today),
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
            
            return result;
        }
    }
}
