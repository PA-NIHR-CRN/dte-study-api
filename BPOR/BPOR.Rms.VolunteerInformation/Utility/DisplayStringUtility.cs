using BPOR.Domain.Enums;
using BPOR.Rms.Abstractions.Enums;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BPOR.Rms.VolunteerInformation.Utility;

public static class DisplayStringUtility
{
    public static string GetVerb(this VsiGroupCriteronType value) =>
        value switch
        {
            VsiGroupCriteronType.Include => Resources.CriteriaType_Include,
            VsiGroupCriteronType.Exclude => Resources.CriteriaType_Exclude,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    
    public static string GetNoun(this VsiGroupCriteronType value) =>
        value switch
        {
            VsiGroupCriteronType.Include => "Inclusion",
            VsiGroupCriteronType.Exclude => "Exclusion",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
}