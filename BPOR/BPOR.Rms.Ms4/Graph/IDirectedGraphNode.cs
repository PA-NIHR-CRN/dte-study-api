namespace BPOR.Rms.Ms4.Graph;

public interface IDirectedGraphNode<TNodeKey, TEdgeValue>
{
    IEnumerable<IDirectedGraphEdge<TNodeKey, TEdgeValue>> GetRelatedNodes(Predicate<TEdgeValue>? predicate = null);
    TNodeKey Key { get; }
}