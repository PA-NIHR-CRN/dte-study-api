using BPOR.Rms.Ms4.Models.Enums;

namespace BPOR.Rms.Ms4.Models;

public class OverviewViewModel
{
    public bool? HasEthicsApproval { get; set; }
    public InclusionInRdnPortfolioStatus? InclusionInRdnPortfolioStatus { get; set; }
    public NihrFundingStatus? NihrFundingStatus { get; set; }
    public long? CpmsId { get; set; }
    public string? FinishRecruitingDay { get; set; }
    public string? FinishRecruitingMonth { get; set; }
    public string? FinishRecruitingYear { get; set; }
}