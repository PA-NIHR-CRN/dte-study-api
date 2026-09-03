using BPOR.Domain.Enums;
using BPOR.Rms.Ms4.Controllers;
using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Models;
using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Ms4;

public static class StudyRequestFlow
{
        public static IGraphNode<MvcActionKey> EthicsApproval { get; } 
        public static IGraphNode<MvcActionKey> InclusionInRdnPortfolio { get; }
        public static IGraphNode<MvcActionKey> FinishRecruiting { get; }
        public static IGraphNode<MvcActionKey> NihrFunding { get; }
        public static IGraphNode<MvcActionKey> ReturnToSummary { get; }
        public static IGraphNode<MvcActionKey> Summary { get; }
        public static IGraphNode<MvcActionKey> StudyDetails { get; }

        

        public static Graph<StudyRequestViewModel, MvcActionKey, StudyEditContext, FlowAction> Graph { get; }

        static StudyRequestFlow()
        {
            Graph = new Graph<StudyRequestViewModel, MvcActionKey, StudyEditContext, FlowAction>();
            
            Summary = Graph.AddNode(new MvcActionKey("StudyRequest", "Summary")); 
            StudyDetails = Graph.AddNode(new MvcActionKey("Study", "Details")); 
            ReturnToSummary = Graph.AddRedirectNode();
            Graph.AddTransition(ReturnToSummary, Summary,
                i => i.FlowType is StudyEditFlowType.ResearcherCreate or StudyEditFlowType.AdminCreate, 
                i => i is FlowAction.Next);
            Graph.AddTransition(ReturnToSummary, StudyDetails, i => i.FlowType is StudyEditFlowType.AdminEdit, i => i is FlowAction.Next);

            EthicsApproval = Graph.AddNode(new MvcActionKey("StudyRequest", "EthicsApproval"));
            InclusionInRdnPortfolio = Graph.AddNode(new MvcActionKey("StudyRequest", "InclusionInRdnPortfolio"));
            FinishRecruiting = Graph.AddNode(new MvcActionKey("StudyRequest", "FinishRecruiting"));
            NihrFunding = Graph.AddNode(new MvcActionKey("StudyRequest", "NihrFunding"));

            Summary = Graph.AddNode(new MvcActionKey("StudyRequest", "Summary"));

            void AddSimpleActions(IGraphNode<MvcActionKey> from, IGraphNode<MvcActionKey> to, Predicate<StudyRequestViewModel>? modelPredicate = null)
            {
                Graph.AddTransition(from, to, i => !i.IndividualEdit, i => i is FlowAction.Next, modelPredicate);
                Graph.AddTransition(to, from, i => !i.IndividualEdit, i => i is FlowAction.Back, modelPredicate);
            }
            
            void AddSimpleActionsWithReturnToSummary(IGraphNode<MvcActionKey> from, IGraphNode<MvcActionKey> to, Predicate<StudyRequestViewModel>? modelPredicate = null)
            {
                AddSimpleActions(from, to, modelPredicate);
                Graph.AddTransition(from, ReturnToSummary, i => i.IndividualEdit, i => i is FlowAction.Next, modelPredicate);
            }

            AddSimpleActionsWithReturnToSummary(EthicsApproval, InclusionInRdnPortfolio);
            AddSimpleActionsWithReturnToSummary(InclusionInRdnPortfolio, FinishRecruiting, i=> i.InclusionInRdnPortfolioStatus is SubmittedType.Yes);
            AddSimpleActions(InclusionInRdnPortfolio, NihrFunding, i => i.InclusionInRdnPortfolioStatus is not SubmittedType.Yes);
            AddSimpleActionsWithReturnToSummary(NihrFunding, FinishRecruiting, i=> i.NihrFundingStatus is not NihrFundingStatusType.No);
        }

        public static string? GetUri(this IUrlHelper urlHelper, MvcActionKey target, StudyEditContext context)
        {
            return urlHelper.Action(target.Action, target.Controller, context);
        }
    }

public class StudyEditContext
{
    public int StudyId {get; set;}
    public StudyEditFlowType FlowType {get; set;}
    public bool IndividualEdit { get; set; }
}

public enum StudyEditFlowType
{
    AdminCreate,
    ResearcherCreate,
    AdminEdit,
}