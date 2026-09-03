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
        public static IGraphNode<MvcActionKey> ParticipantDetails { get; set; }
        public static IGraphNode<MvcActionKey> SponsorOrganisation { get; set; }
        public static IGraphNode<MvcActionKey> MainContact { get; set; }
        public static IGraphNode<MvcActionKey> ChiefInvestigatorContact { get; set; }
        public static IGraphNode<MvcActionKey> ChiefInvestigator { get; set; }
        public static IGraphNode<MvcActionKey> ResearchManager { get; set; }
        public static IGraphNode<MvcActionKey> ResearchLocations { get; set; }
        public static IGraphNode<MvcActionKey> StudyDescription { get; set; }

        

        public static Graph<StudyRequestViewModel, MvcActionKey, StudyEditContext, FlowAction> Graph { get; }

        static StudyRequestFlow()
        {
            Graph = new Graph<StudyRequestViewModel, MvcActionKey, StudyEditContext, FlowAction>();
            
            Summary = Graph.AddNode(new MvcActionKey("StudyRequest", "Summary")); 
            StudyDetails = Graph.AddNode(new MvcActionKey("Study", "Details")); 
            ReturnToSummary = Graph.AddNonTerminalNode();
            Graph.AddTransition(ReturnToSummary, Summary,
                i => i.FlowType is StudyEditFlowType.ResearcherCreate or StudyEditFlowType.AdminCreate, 
                i => i is FlowAction.Next);
            Graph.AddTransition(ReturnToSummary, StudyDetails, i => i.FlowType is StudyEditFlowType.AdminEdit, i => i is FlowAction.Next);

            EthicsApproval = Graph.AddNode(new MvcActionKey("StudyRequest", "EthicsApproval"));
            InclusionInRdnPortfolio = Graph.AddNode(new MvcActionKey("StudyRequest", "InclusionInRdnPortfolio"));
            NihrFunding = Graph.AddNode(new MvcActionKey("StudyRequest", "NihrFunding"));
            FinishRecruiting = Graph.AddNode(new MvcActionKey("StudyRequest", "FinishRecruiting"));
            StudyDescription = Graph.AddNode(new MvcActionKey("StudyRequest", "StudyDescription"));
            ResearchLocations = Graph.AddNode(new MvcActionKey("StudyRequest", "ResearchLocations"));
            ResearchManager = Graph.AddNode(new MvcActionKey("StudyRequest", "ResearchManager"));
            ChiefInvestigator = Graph.AddNode(new MvcActionKey("StudyRequest", "ChiefInvestigator"));
            ChiefInvestigatorContact = Graph.AddNode(new MvcActionKey("StudyRequest", "ChiefInvestigatorContact"));
            MainContact = Graph.AddNode(new MvcActionKey("StudyRequest", "MainContact"));
            SponsorOrganisation = Graph.AddNode(new MvcActionKey("StudyRequest", "SponsorOrganisation"));
            ParticipantDetails = Graph.AddNode(new MvcActionKey("StudyRequest", "ParticipantDetails")); 

            AddSimpleActionsWithReturnToSummary(EthicsApproval, InclusionInRdnPortfolio);
            AddSimpleActionsWithReturnToSummary(InclusionInRdnPortfolio, FinishRecruiting, i=> i.InclusionInRdnPortfolioStatus is SubmittedType.Yes);
            AddSimpleActions(InclusionInRdnPortfolio, NihrFunding, i => i.InclusionInRdnPortfolioStatus is not SubmittedType.Yes);
            AddSimpleActionsWithReturnToSummary(NihrFunding, FinishRecruiting, i=> i.NihrFundingStatus is not NihrFundingStatusType.No);
            AddSimpleActionsWithReturnToSummary(FinishRecruiting, StudyDescription);
            AddSimpleActionsWithReturnToSummary(StudyDescription, ResearchLocations);
            AddSimpleActionsWithReturnToSummary(ResearchLocations, ResearchManager );
            AddSimpleActionsWithReturnToSummary(ResearchManager, ChiefInvestigator);
            AddSimpleActionsWithReturnToSummary(ChiefInvestigator, ChiefInvestigatorContact);
            AddSimpleActionsWithReturnToSummary(ChiefInvestigatorContact, SponsorOrganisation, model => model.IsChiefInvestigatorMainContact is true);
            AddSimpleActions(ChiefInvestigatorContact, MainContact, model => model.IsChiefInvestigatorMainContact is false);
            AddSimpleActionsWithReturnToSummary(MainContact, SponsorOrganisation, model => model.IsChiefInvestigatorMainContact is false);
            AddSimpleActionsWithReturnToSummary(SponsorOrganisation, ParticipantDetails);
            AddSimpleActionsWithReturnToSummary(ParticipantDetails, Summary);
        }
        
        static void AddSimpleActions(IGraphNode<MvcActionKey> from, IGraphNode<MvcActionKey> to, Predicate<StudyRequestViewModel>? modelPredicate = null)
        {
            Graph.AddTransition(from, to, i => !i.IndividualEdit, i => i is FlowAction.Next, modelPredicate);
            Graph.AddTransition(to, from, i => !i.IndividualEdit, i => i is FlowAction.Back, modelPredicate);
        }
            
        static void AddSimpleActionsWithReturnToSummary(IGraphNode<MvcActionKey> from, IGraphNode<MvcActionKey> to, Predicate<StudyRequestViewModel>? modelPredicate = null)
        {
            AddSimpleActions(from, to, modelPredicate);
            Graph.AddTransition(from, ReturnToSummary, i => i.IndividualEdit, i => i is FlowAction.Next, modelPredicate);
        }

        public static string? GetUri(this IUrlHelper urlHelper, MvcActionKey target, StudyEditContext context)
        {
            return urlHelper.Action(target.Action, target.Controller, context);
        }
    }

public record StudyEditContext
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