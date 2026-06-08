using BPOR.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BPOR.Rms.VolunteerInformation.Utility;

public static class DisplayStringUtility
{
    public static string GetVerb(this VolunteerStudyInformationGroupCriteriaTypeId value) =>
        value switch
        {
            VolunteerStudyInformationGroupCriteriaTypeId.Include => Resources.CriteriaType_Include,
            VolunteerStudyInformationGroupCriteriaTypeId.Exclude => Resources.CriteriaType_Exclude,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    
    public static string GetNoun(this VolunteerStudyInformationGroupCriteriaTypeId value) =>
        value switch
        {
            VolunteerStudyInformationGroupCriteriaTypeId.Include => "Inclusion",
            VolunteerStudyInformationGroupCriteriaTypeId.Exclude => "Exclusion",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
}