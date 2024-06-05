using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using NIHR.Infrastructure.Models;

namespace BPOR.Rms;

public static class VolunteerFilterExtensions
{
    // TODO: too tightly coupled to view
    public static IQueryable<Participant> FilterVolunteers(this IQueryable<Participant> volunteers, TimeProvider timeProvider, VolunteerFilterViewModel model, CoordinatesModel? location) => volunteers.WhereContacted(model.StudyId, model.SelectedVolunteersContacted)
        .WhereRegisteredInterest(model.StudyId, model.SelectedVolunteersRegisteredInterest)
        .WhereRecruited(model.StudyId, model.SelectedVolunteersRecruited)
        .WhereCompletedRegistration(model.SelectedVolunteersCompletedRegistration)
        .WhereHasAnyAreaOfResearch(model.SelectedAreasOfInterest, model.IncludeNoAreasOfInterest)
        .WhereHasRegistrationDateInRange(model.RegistrationFromDate.ToDateOnly(), model.RegistrationToDate.ToDateOnly())
        .WhereHasAgeInRange(timeProvider, model.AgeFrom, model.AgeTo)
        .WhereHasSexRegisteredAtBirth(model.GetGenderOptions()) // TODO: sex rather than gender
        .WhereHasGenderSameAsSexRegisteredAtBirth(model.GetGenderSameAsSexRegisteredAtBirthOptions())
        .WhereHasEthnicity(model.GetEthnicityOptions())
        .WhereWithinRadiusOfLocation(location, model.SearchRadiusMiles)
        .WhereHasAnyPostcodeDistrict(model.GetPostcodeDistricts());
}
