using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using NIHR.Infrastructure.Models;

namespace BPOR.Rms.Services;

public interface IFilterService
{
    IQueryable<Participant> FilterVolunteers(VolunteerFilterViewModel model, CoordinatesModel? location);
}
