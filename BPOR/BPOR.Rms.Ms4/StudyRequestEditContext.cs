namespace BPOR.Rms.Ms4;

public record StudyRequestEditContext
{
    /// <summary>
    /// The ID of the study being executed.
    /// </summary>
    public int StudyId { get; set; }
    
    /// <summary>
    /// The type of flow being executed.
    /// </summary>
    public StudyRequestEditFlowType FlowType { get; set; }
    
    // TODO: Move this to a FlowContext base class to support generic sub flows
    /// <summary>
    /// The action to return to when the current subflow exits.
    /// </summary>
    public string? SubflowRtnAct { get; set; }
}