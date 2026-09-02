using BPOR.Rms.Utilities.FlowGraph;

namespace BPOR.Rms.Tests;

public class FlowGraphTests
{
    public record Context(FlowRoute Route, bool IsEditing);
    
    public enum FlowRoute
    {
        ResearcherCreate,
        AdminCreate,
        Edit,
    }
    
    public class Model
    {}
    
    [Fact]
    public void Test1()
    {
        Graph<Model, MvcActionKey, Context, SimpleTransition> graph = new();

        
        var AgreeToTAndCsNode = graph.AddNode( new MvcActionKey("MyController", "AgreeToTAndCs"));
        var TypeOfPetNode = graph.AddNode(new MvcActionKey("MyController", "TypeOfPet"));
        var NameOfPetNode = graph.AddNode(new MvcActionKey("MyController", "NameOfPet"));

        var editCompleteNode = graph.AddRedirectNode();

        graph.AddEntryPoint(i => i is { Route: FlowRoute.ResearcherCreate }, AgreeToTAndCsNode);
        
       //  graph.AddAutoRedirect(FlowRoute.ResearcherCreate, AgreeToTAndCsNode);

        graph.AddTransition(AgreeToTAndCsNode, TypeOfPetNode, i => i is FlowRoute.ResearcherCreate, i => i is SimpleTransition.Next or SimpleTransition.Skip);
        graph.AddTransition(TypeOfPetNode, AgreeToTAndCsNode, i => i is FlowRoute.ResearcherCreate, i => i is SimpleTransition.Back);
        
        graph.AddTransition(TypeOfPetNode, NameOfPetNode, i => i is FlowRoute.ResearcherCreate, i => i is SimpleTransition.Next or SimpleTransition.Skip);
        graph.AddTransition(NameOfPetNode, TypeOfPetNode, i => i is FlowRoute.ResearcherCreate, i => i is SimpleTransition.Back);
    }
}