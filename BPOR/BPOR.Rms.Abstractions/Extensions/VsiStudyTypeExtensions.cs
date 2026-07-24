using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.Abstractions.Extensions;

public static class VsiStudyTypeExtensions
{
    public static bool CanBeInPerson(this VsiStudyType? vsiStudyType) => 
        vsiStudyType is VsiStudyType.InPerson or VsiStudyType.Hybrid;
}