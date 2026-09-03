using JetBrains.Annotations;

namespace BPOR.Rms.Ms4.FlowGraph;

public record MvcActionKey([AspMvcController]string Controller, [AspMvcAction]string Action);

// public class RouteBuilder<TModel, TRoute, TTransition>
//     where TTransition : IEquatable<TTransition>
//     where TRoute : IEquatable<TRoute>
// {
//     private readonly TRoute _route;
//     private Node<TModel, TRoute, TTransition> _currentNode;
//
//     public RouteBuilder(TRoute route, Node<TModel, TRoute, TTransition> currentNode)
//     {
//         _route = route;
//         _currentNode = currentNode;
//     }
//     
//     public 
// }