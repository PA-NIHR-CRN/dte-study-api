namespace BPOR.Rms.Ms4.Graph;

public class DirectedGraph<TNodeKey, TEdgeValue> 
    where TNodeKey : notnull
{
    private readonly Dictionary<TNodeKey, Node> _nodes = new();
    public IEnumerable<TNodeKey> NodeKeys => _nodes.Keys;

    public IDirectedGraphNode<TNodeKey, TEdgeValue> AddNode(TNodeKey nodeKey)
    {
        var result = new Node(nodeKey);
        _nodes.Add(nodeKey, result);
        return result;
    }
    
    public IDirectedGraphNode<TNodeKey, TEdgeValue> GetOrAddNode(TNodeKey nodeKey) => GetOrAddNodeInternal(nodeKey);

    private Node GetOrAddNodeInternal(TNodeKey nodeKey)
    {
        if (!_nodes.TryGetValue(nodeKey, out var result))
        {
            result = new Node(nodeKey);
            _nodes.Add(nodeKey, result);
        }
        return result;
    }

    public IDirectedGraphEdge<TNodeKey, TEdgeValue> AddEdge(TNodeKey fromKey, TNodeKey toKey, TEdgeValue value)
    {
        var from = GetOrAddNodeInternal(fromKey);
        var to = GetOrAddNodeInternal(toKey);
        var edge = new Edge(value, to);
        from.DepartingEdges.Add(edge);
        return edge;
    }
    
    private class Node : IDirectedGraphNode<TNodeKey, TEdgeValue>
    {
        public Node(TNodeKey key)
        {
            Key = key;
        }
        
        public IEnumerable<IDirectedGraphEdge<TNodeKey, TEdgeValue>> GetRelatedNodes(Predicate<TEdgeValue>? predicate = null)
        {
            return predicate == null ? DepartingEdges : DepartingEdges.Where(edge => predicate(edge.Value));
        } 

        public TNodeKey Key { get;}
        public readonly List<Edge> DepartingEdges = new();
    }

    private class Edge : IDirectedGraphEdge<TNodeKey, TEdgeValue>
    {
        public Edge(TEdgeValue value, Node relatedNode)
        {
            Value = value;
            RelatedNode = relatedNode;
        }

        public TEdgeValue Value { get; }
        IDirectedGraphNode<TNodeKey, TEdgeValue> IDirectedGraphEdge<TNodeKey, TEdgeValue>.RelatedNode => RelatedNode;
        public readonly Node RelatedNode;
    }

    public IDirectedGraphNode<TNodeKey, TEdgeValue> GetNode(TNodeKey key)
    {
        return _nodes[key];
    }
}