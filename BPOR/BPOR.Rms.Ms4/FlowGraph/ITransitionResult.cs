namespace BPOR.Rms.Ms4.FlowGraph;

public interface ITransitionResult<out TNodeKey>
{
    TNodeKey NodeKey { get; }
    
    object? Context { get; }
}