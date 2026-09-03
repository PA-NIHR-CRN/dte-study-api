namespace BPOR.Rms.Ms4.FlowGraph;

public class Graph<TModel, TNodeKey, TContext, TAction>
    where TContext : notnull
    where TNodeKey : IEquatable<TNodeKey>
{
    private const int _maxHops = 20;
    
    private readonly List<EntryPoint> _entryPoints = new();

    public Graph()
    {
    }

    private record EntryPoint(Predicate<TContext> contextPredicate, IGraphNode<TNodeKey> node);
    
    public void AddEntryPoint(Predicate<TContext> contextPredicate, IGraphNode<TNodeKey> node) => _entryPoints.Add(new EntryPoint(contextPredicate, node));
    
    public IGraphNode<TNodeKey> AddNonTerminalNode() => new ContextualNode();

    private readonly List<IGraphNode<TNodeKey>> _nodes = new();
    private readonly List<Transition> _transitions = new();

    private class ContextualNode : IGraphNode<TNodeKey>
    {
        public bool IsTransitionEnd => false;
        public TNodeKey? Value => default;
    }

    private class LeafNode : IGraphNode<TNodeKey>
    {
        public LeafNode(TNodeKey value)
        {
            Value = value;
        }

        public bool IsTransitionEnd => true;

        public TNodeKey Value { get; }
    }
    
    private record Transition(IGraphNode<TNodeKey> Origin, IGraphNode<TNodeKey> Destination, 
        Predicate<TContext> ContextPredicate, Predicate<TAction> TransitionPredicate, Predicate<TModel>? ModelPredicate,
        Func<TContext, TContext>? TransformContext);
    
    public void AddTransition(IGraphNode<TNodeKey> origin, IGraphNode<TNodeKey> destination, 
        Predicate<TContext> contextPredicate, Predicate<TAction> actionPredicate, Predicate<TModel>? modelPredicate = null, Func<TContext, TContext>? transformContext = null)
        => _transitions.Add(new Transition(origin, destination, contextPredicate, actionPredicate, modelPredicate, transformContext));

    public bool CanTransition(IGraphNode<TNodeKey> origin, TContext context, TModel model, TAction action)
        => GetTransition(origin, context, model, action) != null;


    public TransitionResult<TContext, TNodeKey>? ApplyTransition(TNodeKey origin, TContext context, TModel model, TAction action)
    {
        IGraphNode<TNodeKey>? currentNode = GetNode(origin);
        if (currentNode == null)
        {
            throw new ArgumentException(nameof(origin));
        }
        
        int hopCount = 0;
        while (hopCount < _maxHops)
        {
            var transition = GetTransition(currentNode, context, model, action);
            if (transition == null)
            {
                return null;
            }

            TContext newContext = transition.TransformContext == null
                ? context
                : transition.TransformContext(context);
            currentNode = transition.Destination;

            if (currentNode.IsTransitionEnd)
            {
                return new(newContext, currentNode.Value!);
            }

            hopCount++;
        }

        throw new InvalidOperationException("Too many redirects");
    }

    private LeafNode? GetNode(TNodeKey nodeKey)
    {
        return _nodes.OfType<LeafNode>().SingleOrDefault(i => i.Value.Equals(nodeKey));
    }

    private Transition? GetTransition(IGraphNode<TNodeKey> origin, TContext context, TModel model, TAction transition)
        => _transitions.FirstOrDefault(t =>
            Equals(t.Origin, origin) &&
            t.ContextPredicate(context) &&
            t.TransitionPredicate(transition) &&
            (t.ModelPredicate?.Invoke(model) ?? true));


    public IGraphNode<TNodeKey> AddNode(TNodeKey mvcActionNode)
    {
        var result = new LeafNode(mvcActionNode);
        this._nodes.Add(result);
        return result;
    }
}

public interface IGraphNode<TValue>
{
    public bool IsTransitionEnd { get; }
    public TValue? Value { get; }
}

public class TransitionResult<TContext, TNodeKey>
{
    public TransitionResult(TContext context, TNodeKey nodeKey)
    {
        Context = context;
        NodeKey = nodeKey;
    }

    public TContext Context { get; }
    public TNodeKey NodeKey { get; }
}

