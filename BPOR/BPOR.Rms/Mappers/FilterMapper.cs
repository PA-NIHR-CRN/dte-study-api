using BPOR.Domain.Entities;
using BPOR.Rms.Models.Filter;
using Rbec.Postcodes;

namespace BPOR.Rms.Mappers;

public class FilterMapper
{
    public static VolunteerFilterViewModel MapToFilterModel(FilterCriteria criteria)
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
                From = criteria.DateOfBirthTo.HasValue
                    ? DateTime.Today.Year - criteria.DateOfBirthTo.Value.Year -
                      (DateTime.Today.DayOfYear < criteria.DateOfBirthTo.Value.DayOfYear ? 1 : 0)
                    : null,
                To = criteria.DateOfBirthFrom.HasValue
                    ? DateTime.Today.Year - criteria.DateOfBirthFrom.Value.Year -
                      (DateTime.Today.DayOfYear < criteria.DateOfBirthFrom.Value.DayOfYear ? 1 : 0)
                    : null,
            },
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

            IncludeNoAreasOfInterest = criteria.IncludeNoAreasOfInterest
        };

        return viewModel;
    }
}
