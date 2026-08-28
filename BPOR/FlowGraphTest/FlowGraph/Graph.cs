namespace FlowGraphTest.FlowGraph;

public class Graph<TModel, TNodeKey, TContext, TAction>
    where TContext : notnull
    where TNodeKey : IEquatable<TNodeKey>
{
    private const int MaxHops = 20;
    
    private readonly List<EntryPoint> _entryPoints = new();

    public Graph()
    {
    }

    private record EntryPoint(Predicate<TContext> contextPredicate, Node node);
    
    public void AddEntryPoint(Predicate<TContext> contextPredicate, Node node) => _entryPoints.Add(new EntryPoint(contextPredicate, node));
    
    public Node AddRedirectNode() => new ContextualNode();

    private readonly List<Node> _nodes = new();
    private readonly List<Transition> _transitions = new();

    public class Node
    {
        
    }

    private class ContextualNode : Node
    {
    }

    public class LeafNode : Node
    {
        public LeafNode(TNodeKey value)
        {
            Value = value;
        }

        public TNodeKey Value { get; }
    }
    
    private record Transition(Node Origin, Node Destination, 
        Predicate<TContext> ContextPredicate, Predicate<TAction> TransitionPredicate, Func<TContext, TContext>? TransformContext);
    
    public void AddTransition(Node origin, Node destination, 
        Predicate<TContext> contextPredicate, Predicate<TAction> actionPredicate, Func<TContext, TContext>? transformContext = null)
        => _transitions.Add(new Transition(origin, destination, contextPredicate, actionPredicate, transformContext));

    public bool CanTransition(Node origin, TContext context,  TAction action)
        => GetTransition(origin, context, action) != null;

    public (TContext newContext, TNodeKey newNode) ApplyTransition(TNodeKey origin, TContext context, TAction action)
    {
        Node? currentNode = GetLeafNode(origin);
        if (currentNode == null)
        {
            throw new ArgumentException(nameof(origin));
        }
        
        int hopCount = 0;
        while (hopCount < MaxHops)
        {
            var transition = GetTransition(currentNode, context, action);
            if (transition == null)
            {
                throw new InvalidOperationException($"Transition {hopCount} had no transition");
            }

            TContext newContext = transition.TransformContext == null
                ? context
                : transition.TransformContext(context);
            currentNode = transition.Destination;

            switch (currentNode)
            {
                case LeafNode leafNode:
                    return (newContext, leafNode.Value);
                case ContextualNode contextualNode:
                    hopCount++;
                    break;
                default:
                    throw new InvalidOperationException("Unsupported node type");
            }
        }

        throw new InvalidOperationException("Too many redirects");
    }

    private LeafNode? GetLeafNode(TNodeKey nodeKey)
    {
        return _nodes.OfType<LeafNode>().SingleOrDefault(i => i.Value.Equals(nodeKey));
    }

    private Transition? GetTransition(Node origin, TContext context, TAction transition)
        => _transitions.FirstOrDefault(t =>
            Equals(t.Origin, origin) && t.ContextPredicate(context) && t.TransitionPredicate(transition));


    public LeafNode AddNode(TNodeKey mvcActionNode)
    {
        return new LeafNode(mvcActionNode);
    }
}