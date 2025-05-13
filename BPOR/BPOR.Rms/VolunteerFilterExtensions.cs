using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.Models.Filter;

namespace BPOR.Rms;

public static class VolunteerFilterExtensions
{
    // TODO: too tightly coupled to view
    public static IQueryable<Participant> FilterVolunteers(this IQueryable<Participant> volunteers, TimeProvider timeProvider, VolunteerFilterViewModel model) => volunteers.WhereContacted(model.StudyId, model.SelectedVolunteersContacted)
        .WhereRegisteredInterest(model.StudyId, model.SelectedVolunteersRegisteredInterest)
        .WhereHasPreferredContactMethod(model.ShowPreferredContactFilter ? model.SelectedVolunteersPreferredContact : (int)ContactMethodId.Email)
        .WhereRecruited(model.StudyId, model.SelectedVolunteersRecruited)
        .WhereCompletedRegistration(model.SelectedVolunteersCompletedRegistration)
        .WhereHasAnyAreaOfResearch(model.SelectedAreasOfInterest, model.IncludeNoAreasOfInterest)
        .WhereHasRegistrationDateInRange(model.RegistrationFromDate.ToDateOnly(), model.RegistrationToDate.ToDateOnly())
        .WhereHasAgeInRange(timeProvider, model.AgeRange.From, model.AgeRange.To)
        .WhereHasSexRegisteredAtBirth(model.GetGenderOptions()) // TODO: sex rather than gender
        .WhereHasGenderSameAsSexRegisteredAtBirth(model.GetGenderSameAsSexRegisteredAtBirthOptions())
        .WhereHasEthnicity(model.GetEthnicityOptions())
        .WhereHasLocation(model.PostcodeSearch);
}
