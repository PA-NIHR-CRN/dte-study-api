using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;

namespace BPOR.Rms.Services;

public interface IFilterService
{
    Task<IQueryable<Participant>> FilterVolunteersAsync(VolunteerFilterViewModel model,
        CancellationToken cancellationToken = default);
}
