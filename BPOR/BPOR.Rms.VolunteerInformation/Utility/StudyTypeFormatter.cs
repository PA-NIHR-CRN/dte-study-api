using BPOR.Rms.Abstractions.Enums;
using NIHR.Infrastructure.AspNetCore;

namespace BPOR.Rms.VolunteerInformation.Utility;

public class StudyTypeFormatter : IDisplayStringFormatter
{
    public string ToDisplayString(object? value)
        => value switch
        {
            VsiStudyType.Hybrid => Resources.VsiStudyTypeHybrid,
            VsiStudyType.InPerson => Resources.VsiStudyTypeInPerson,
            VsiStudyType.Remote => Resources.VsiStudyTypeRemote,
            _ => string.Empty
        };
}