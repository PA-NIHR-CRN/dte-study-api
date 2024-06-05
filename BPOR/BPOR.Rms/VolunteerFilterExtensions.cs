using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using NIHR.Infrastructure.Models;

namespace BPOR.Rms;

public static class VolunteerFilterExtensions
{
    // too tightly coupled to view
    public static IQueryable<Participant> FilterVolunteers(this IQueryable<Participant> query, TimeProvider timeProvider, VolunteerFilterViewModel model, CoordinatesModel? location)
    {
        query = query.WhereVolunteersContacted(model.StudyId, model.SelectedVolunteersContacted);
        query = query.WhereVolunteersRegisteredInterest(model.StudyId, model.SelectedVolunteersRegisteredInterest);
        query = query.WhereVolunteersRecruited(model.StudyId, model.SelectedVolunteersRecruited);
        query = query.WhereVolunteersCompletedRegistration(model.SelectedVolunteersCompletedRegistration);
        query = query.WhereHasAnyAreaOfResearch(model.SelectedAreasOfInterest, model.IncludeNoAreasOfInterest);
        query = query.WhereHasRegistrationDateInRange(model.RegistrationFromDate.ToDateOnly(), model.RegistrationToDate.ToDateOnly());
        query = query.WhereHasAgeInRange(timeProvider, model.AgeFrom, model.AgeTo);

        query = query.WhereHasSexRegisteredAtBirth(model.IsSexMale, model.IsSexFemale);
        query = query.WhereHasGenderSameAsSexRegisteredAtBirth(model.IsGenderSameAsSexRegisteredAtBirth_Yes, model.IsGenderSameAsSexRegisteredAtBirth_No,
            model.IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay);

        query = query.WhereHasEthnicity(model.Ethnicity_Asian, model.Ethnicity_Black, model.Ethnicity_Mixed,
            model.Ethnicity_Other, model.Ethnicity_White);

        query = query.WhereWithinRadiusOfLocation(location, model.SearchRadiusMiles);

        if (!string.IsNullOrWhiteSpace(model.PostcodeDistricts))
        {
            var postcodeList = model.PostcodeDistricts.Split(',', StringSplitOptions.RemoveEmptyEntries).Where(p => p is not null).Select(p => p.Trim());

            query = query.WhereStartsWithAnyPostcodeDistrict(postcodeList);
        }

        return query;
    }
}
