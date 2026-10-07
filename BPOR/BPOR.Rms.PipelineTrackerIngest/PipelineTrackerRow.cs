using System.Diagnostics;
using BPOR.Domain.Enums;

namespace BPOR.Rms.PipelineTrackerIngest;

public class PipelineTrackerRow
{
    public int VsId { get; set; }
    public string StudyShortName { get; set; }
    public int? CpmsId { get; set; }
    public StudyStatusType? StudyStatus { get; set; }
    public string MainContactName { get; set; }
    public string MainContactEmail { get; set; }
    public string ManagingSpeciality { get; set; }
    public string StudyDesign { get; set; }
    public string CommOrNonComm { get; set; }
    public DateTime? RecruitmentStartDate { get; set; }
    public DateTime? RecruitmentEndDate { get; set; }
    public string OverallRecruitmentTarget { get; set; }
    public DateTime AddedToRegistryPipelineAt { get; set; }
    public NihrFundingStatusType NihrFundingStatus { get; set; }
    public RejectedWithdrawnReasonType? RejectedWithdrawnReasonType { get; set; }
}

public class RejectedWithdrawnReasonType
{
    public RejectedWithdrawnReasonType(RejectedReasonType rejectedReason)
    {
        RejectedReason = rejectedReason;
    }

    public RejectedWithdrawnReasonType(WithdrawnReasonType withdrawnReason)
    {
        WithdrawnReason = withdrawnReason;
    }

    public WithdrawnReasonType? WithdrawnReason  { get; set; }
    public RejectedReasonType? RejectedReason { get; set; }
}