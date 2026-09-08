using BPOR.Rms.Ms4.Graph;
using BPOR.Rms.Ms4.Graph.Algorithms;
using NetTopologySuite.Planargraph;

namespace BPOR.Rms.Ms4.FlowGraph;

public class MvcFlowGraph<TModel, TContext, TAction>
    where TContext : notnull
{
    private readonly DirectedGraph<MvcActionKey, Transition> _graph = new();

    /// <summary>
    /// Calculates the best case progress for a given context between a start node and end node via the current node.
    /// </summary>
    /// <returns> The best case progress between 0 and 1, or null if there is no valid path. </returns>
    public double? CalculateBestCaseProgress(TContext context, MvcActionKey start, MvcActionKey end,
        MvcActionKey current)
    {
        var dijkstra = _graph.Dijkstra();

        // Calculate minimum paths from both the start node and the current node
        var costsFromStart = dijkstra.Execute(start, edgeFilter: i => i.ContextFilter?.Invoke(context) ?? true);
        var costsFromCurrent = dijkstra.Execute(current, edgeFilter: i => i.ContextFilter?.Invoke(context) ?? true);

        if (costsFromStart.TryGetValue(current, out double costStartToCurrent) &&
            costsFromCurrent.TryGetValue(end, out double costCurrentToEnd))
        {
            return costStartToCurrent / (costStartToCurrent + costCurrentToEnd);
        }

        return null;
    }

    private record Transition(
        Predicate<TContext>? ContextFilter,
        TAction Action,
        Predicate<TModel>? ModelFilter,
        Func<TContext, TContext>? ContextTransform,
        Func<TContext, TransitionResult<TContext, MvcActionKey>, TransitionResult<TContext, MvcActionKey>>? ResultTransform);

    public void AddTransition(MvcActionKey origin,
        MvcActionKey destination,
        TAction action,
        Predicate<TContext>? contextPredicate = null,
        Predicate<TModel>? modelPredicate = null,
        Func<TContext, TContext>? contextTransform = null,
        Func<TContext, TransitionResult<TContext, MvcActionKey>, TransitionResult<TContext, MvcActionKey>>? destinationTransform = null)
        => _graph.AddEdge(origin, destination,
            new Transition(contextPredicate, action, modelPredicate, contextTransform, destinationTransform));

    public TransitionResult<TContext, MvcActionKey>? ApplyTransition(MvcActionKey origin, TContext context,
        TModel model, TAction action)
    {
        var currentNode = _graph.GetNode(origin);
        if (currentNode == null)
        {
            throw new ArgumentException(nameof(origin));
        }

        var relatedNodes = currentNode.GetRelatedNodes(i => IsValidTransition(i, context, model, action)).ToArray();
        switch (relatedNodes.Length)
        {
            case 0:
                return null;
            case 1:
                var contextTransform = relatedNodes[0].Value.ContextTransform;
                TContext newContext = contextTransform == null
                    ? context
                    : contextTransform(context);
                var result = new TransitionResult<TContext, MvcActionKey>(newContext, relatedNodes[0].RelatedNode.Key);
                var resultTransform = relatedNodes[0].Value.ResultTransform;
                result = resultTransform != null ? resultTransform(context, result) : result;
                return result;
            default:
                throw new InvalidOperationException($"Multiple {action} transitions found for the current state");
        }
    }

    private bool IsValidTransition(Transition transition, TContext context, TModel model, TAction action)
        => Equals(action, transition.Action) &&
           (transition.ContextFilter?.Invoke(context) ?? true) &&
           (transition.ModelFilter?.Invoke(model) ?? true);
}