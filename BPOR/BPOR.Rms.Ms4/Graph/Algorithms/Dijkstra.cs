using System.Numerics;

namespace BPOR.Rms.Ms4.Graph.Algorithms;

public static class Extensions
{
    public static Dijkstra<TNodeKey, TEdgeValue> Dijkstra<TNodeKey, TEdgeValue>(this DirectedGraph<TNodeKey, TEdgeValue> graph) 
        where TNodeKey : notnull 
        => new(graph);
}

public class Dijkstra<TNodeKey, TEdgeValue>(DirectedGraph<TNodeKey, TEdgeValue> graph)
    where TNodeKey : notnull
{
    public Dictionary<TNodeKey, double> Execute(
        TNodeKey start,
        Func<TEdgeValue, double>? getCost = null,
        Predicate<TEdgeValue>? edgeFilter = null)
    {
        // Track the shortest distance from the start node to all other nodes
        var distances = new Dictionary<TNodeKey, double>();
        
        // Priority queue stores tuples of (NodeName, Distance), ordered by Distance
        var priorityQueue = new PriorityQueue<IDirectedGraphNode<TNodeKey, TEdgeValue>, double>();

        // Initialise all nodes with a distance of infinity
        foreach (var node in graph.NodeKeys)
        {
            distances[node] = double.MaxValue;
        }

        // Set the starting node distance to 0 and enqueue it
        distances[start] = 0;
        priorityQueue.Enqueue(graph.GetNode(start), 0);

        while (priorityQueue.Count > 0)
        {
            var currentNode = priorityQueue.Dequeue();

            foreach (var edge in currentNode.GetRelatedNodes(edgeFilter))
            {
                double edgeCost = getCost?.Invoke(edge.Value) ?? 1;
                double newDistance = distances[currentNode.Key] + edgeCost;

                // If a shorter path to the target neighbour is found, update it
                if (!distances.TryGetValue(edge.RelatedNode.Key, out var existingDistance) ||  newDistance < existingDistance)
                {
                    distances[edge.RelatedNode.Key] = newDistance;
                    priorityQueue.Enqueue(edge.RelatedNode, newDistance);
                }
            }
        }

        return distances;
    }
}