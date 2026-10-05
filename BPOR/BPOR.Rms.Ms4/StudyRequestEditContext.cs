using BPOR.Rms.Ms4.FlowGraph;

namespace BPOR.Rms.Ms4;

public record StudyRequestEditContext : MvcFlowContextBase
{
    /// <summary>
    /// The ID of the study being executed.
    /// </summary>
    public int StudyId { get; set; }
    
    /// <summary>
    /// The type of flow being executed.
    /// </summary>
    public StudyRequestEditFlowType FlowType { get; set; }
}