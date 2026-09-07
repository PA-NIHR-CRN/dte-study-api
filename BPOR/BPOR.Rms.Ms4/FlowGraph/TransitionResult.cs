namespace BPOR.Rms.Ms4.FlowGraph;

public class TransitionResult<TContext, TNodeKey> : ITransitionResult<TNodeKey>
{
    public TransitionResult(TContext context, TNodeKey nodeKey)
    {
        Context = context;
        NodeKey = nodeKey;
    }

    public TContext Context { get; set; }
    public TNodeKey NodeKey { get; set; }

    object? ITransitionResult<TNodeKey>.Context => Context;
}