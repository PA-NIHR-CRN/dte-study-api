namespace BPOR.Rms.Ms4.FlowGraph;

public class TransitionResult<TContext, TNodeKey> : ITransitionResult<TNodeKey>
{
    public TransitionResult(TContext context, TNodeKey nodeKey, bool returnFromSubflow)
    {
        Context = context;
        NodeKey = nodeKey;
        ReturnFromSubflow = returnFromSubflow;
    }

    public TContext Context { get; set; }
    public TNodeKey NodeKey { get; set; }
    public bool ReturnFromSubflow { get; }

    object? ITransitionResult<TNodeKey>.Context => Context;
}