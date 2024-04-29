using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;

namespace BPOR.Rms.Mappers;

public class FilterMapper
{
    public static VolunteerFilterViewModel MapToFilterModel(FilterCriteria criteria)
    {
        var viewModel = new VolunteerFilterViewModel
        {
            StudyId = criteria.StudyId ?? 0,
            SelectedStudyId = criteria.StudyId?.ToString(),

            IncludeContacted = criteria.IncludeContacted ?? false,
            IncludeRegisteredInterest = criteria.IncludeRegisteredInterest ?? false,
            IncludeCompletedRegistration = criteria.IncludeCompletedRegistration ?? false,
            IncludeRecruited = criteria.IncludeRecruited ?? false,

            FullPostcode = criteria.FullPostcode,
            SearchRadiusMiles = criteria.SearchRadiusMiles,

            
            RegistrationFromDateDay = criteria.RegistrationFromDate?.Day,
            RegistrationFromDateMonth = criteria.RegistrationFromDate?.Month,
            RegistrationFromDateYear = criteria.RegistrationFromDate?.Year,

            RegistrationToDateDay = criteria.RegistrationToDate?.Day,
            RegistrationToDateMonth = criteria.RegistrationToDate?.Month,
            RegistrationToDateYear = criteria.RegistrationToDate?.Year,

            AgeFrom = criteria.DateOfBirthTo.HasValue
                ? DateTime.Today.Year - criteria.DateOfBirthTo.Value.Year -
                  (DateTime.Today.DayOfYear < criteria.DateOfBirthTo.Value.DayOfYear ? 1 : 0)
                : null,
            AgeTo = criteria.DateOfBirthFrom.HasValue
                ? DateTime.Today.Year - criteria.DateOfBirthFrom.Value.Year -
                  (DateTime.Today.DayOfYear < criteria.DateOfBirthFrom.Value.DayOfYear ? 1 : 0)
                : null,
        };

        return viewModel;
    }
}
