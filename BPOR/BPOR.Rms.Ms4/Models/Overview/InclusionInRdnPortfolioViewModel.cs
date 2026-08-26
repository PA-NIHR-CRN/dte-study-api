namespace BPOR.Rms.Ms4.Models.Overview;

public class InclusionInRdnPortfolioViewModel : StudyRequestPageViewModel
{
    public InclusionInRdnPortfolioStatus? InclusionInRdnPortfolioStatus { get; set; }
    
    public long? CpmsId { get; set; }
}