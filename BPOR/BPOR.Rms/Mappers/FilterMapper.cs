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

            IncludeContacted = criteria.IncludeContacted ?? false,
            IncludeRegisteredInterest = criteria.IncludeRegisteredInterest ?? false,
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

            IsSexMale = criteria.FilterGender.Any(fg => fg.GenderId == 1),
            IsSexFemale = criteria.FilterGender.Any(fg => fg.GenderId == 2),

            IsGenderSameAsSexRegisteredAtBirth_Yes = criteria.FilterSexSameAsRegisteredAtBirth.Any(f => f.YesNoPreferNotToSay == 1),
            IsGenderSameAsSexRegisteredAtBirth_No = criteria.FilterSexSameAsRegisteredAtBirth.Any(f => f.YesNoPreferNotToSay == 2),
            IsGenderSameAsSexRegisteredAtBirth_PreferNotToSay = criteria.FilterSexSameAsRegisteredAtBirth.Any(f => f.YesNoPreferNotToSay == 3),

            SelectedHealthConditions = criteria.FilterAreaOfInterest.Select(f => f.HealthConditionId.ToString()).ToList(),

            PostcodeDistricts = string.Join(", ", criteria.FilterPostcode.Select(f => f.PostcodeFragment)),

            Ethnicity_Asian = criteria.FilterEthnicGroup.Any(f => f.EthnicGroupId == 1),
            Ethnicity_Black = criteria.FilterEthnicGroup.Any(f => f.EthnicGroupId == 2),
            Ethnicity_Mixed = criteria.FilterEthnicGroup.Any(f => f.EthnicGroupId == 3),
            Ethnicity_White = criteria.FilterEthnicGroup.Any(f => f.EthnicGroupId == 4),
            Ethnicity_Other = criteria.FilterEthnicGroup.Any(f => f.EthnicGroupId == 5),


        };

        if (criteria.IncludeCompletedRegistration == true)
        {
            viewModel.SelectedVolunteersCompletedRegistration = "1";
        }
        else if (criteria.IncludeCompletedRegistration == false)
        {
            viewModel.SelectedVolunteersCompletedRegistration = "2";
        }

        if (criteria.IncludeContacted == true)
        {
            viewModel.SelectedVolunteersContacted = "1";
        }
        else if (criteria.IncludeContacted == false)
        {
            viewModel.SelectedVolunteersContacted = "2";
        }

        if (criteria.IncludeRecruited == true)
        {
            viewModel.SelectedVolunteersRecruited = "1";
        }
        else if (criteria.IncludeRecruited == false)
        {
            viewModel.SelectedVolunteersRecruited = "2";
        }

        if (criteria.IncludeRegisteredInterest == true)
        {
            viewModel.SelectedVolunteersRegisteredInterest = "1";
        }
        else if (criteria.IncludeRegisteredInterest == false)
        {
            viewModel.SelectedVolunteersRegisteredInterest = "2";
        }

        return viewModel;
    }
}
