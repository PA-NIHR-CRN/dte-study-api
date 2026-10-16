using BPOR.Domain.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace BPOR.Rms.PipelineTrackerIngest.TypeConvertors;

public class RejectedWithdrawnReasonTypeConvertor : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return text switch
        {
            "N/A" => null,
            "Unknown" => null,
            "R - Not possible to recruit target population" => new RejectedWithdrawnReasonType(RejectedReasonType.NotPossibleToRecruitTargetPopulation),
            "R - Study already listed here" => new RejectedWithdrawnReasonType(RejectedReasonType.StudyAlreadyListedHere),
            "R - Recruitment window too short" => new RejectedWithdrawnReasonType(RejectedReasonType.RecruitmentWindowTooShort),
            "R - Not NIHR-affiliated" => new RejectedWithdrawnReasonType(RejectedReasonType.NotNihrAffiliated),
            "R - PPIE opportunity" => new RejectedWithdrawnReasonType(RejectedReasonType.PpieOpportunity),
            "W - Non-response" => new RejectedWithdrawnReasonType(WithdrawnReasonType.NoResponseFromStudyTeam),
            "W - Limited admin capacity" => new RejectedWithdrawnReasonType(WithdrawnReasonType.StudyTeamHasLimitedCapacity),
            "W - Problems with Study (eg. Suspension)" => new RejectedWithdrawnReasonType(WithdrawnReasonType.ProblemsWithStudy),
            "W - Contact dropped by BPoR team" => new RejectedWithdrawnReasonType(WithdrawnReasonType.ContactDroppedByBPorTeam),
            "W - Study is recruiting fine already" => new RejectedWithdrawnReasonType(WithdrawnReasonType.StudyDoesNotNeedAdditionalSupport),
            "W - Misc" => new RejectedWithdrawnReasonType(WithdrawnReasonType.Other),
            _ => throw new ArgumentException($"Unsupported rejected reason {text}", nameof(text))
        };
    }


}