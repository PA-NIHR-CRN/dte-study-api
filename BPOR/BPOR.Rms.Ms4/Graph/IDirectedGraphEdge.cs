namespace BPOR.Rms.Ms4.Graph;

public interface IDirectedGraphEdge<TNodeKey, TEdgeValue>
{
    TEdgeValue Value { get; }
    IDirectedGraphNode<TNodeKey, TEdgeValue> RelatedNode { get; }
}