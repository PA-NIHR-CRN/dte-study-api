using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using BPOR.Rms.Models.Volunteer;
using NIHR.Infrastructure.Paging;

namespace BPOR.Rms.Services
{
    public interface IVolunteerFilterService
    {
        Task<int> GetFilteredVolunteerCountAsync(VolunteerFilterViewModel model, CancellationToken token = default);
        Task<PageDeferred<VolunteerResult>> GetFilteredVolunteersForTestingAsync(VolunteerFilterViewModel model, IPaginationService paginationService, CancellationToken token = default);
        Task<List<CampaignParticipantDetails>> GetFilteredVolunteersAsync(FilterCriteria dbFilter, int? targetGroupSize, CancellationToken cancellationToken);
        Task<VolunteerFilterViewModel> MapToFilterModelAsync(FilterCriteria criteria, CancellationToken cancellationToken);
    }
}