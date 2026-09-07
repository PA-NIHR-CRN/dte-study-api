using System.Text.Json.Serialization;
using BPOR.Domain.Enums;
using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Models;

namespace BPOR.Rms.Ms4;

public class StudyRequestFlow
{
    public static MvcActionKey EthicsApproval { get; } = new("StudyRequest", "EthicsApproval");
    public static MvcActionKey InclusionInRdnPortfolio { get; } = new("StudyRequest", "InclusionInRdnPortfolio");
    public static MvcActionKey FinishRecruiting { get; } = new("StudyRequest", "FinishRecruiting");
    public static MvcActionKey NihrFunding { get; } = new("StudyRequest", "NihrFunding");
    public static MvcActionKey MoreInformationRequired { get; } = new("StudyRequest", "MoreInformationRequired");
    public static MvcActionKey Summary { get; } = new("StudyRequest", "Summary");
    public static MvcActionKey ParticipantDetails { get; set; } = new("StudyRequest", "ParticipantDetails");
    public static MvcActionKey ChiefInvestigator { get; set; } = new("StudyRequest", "ChiefInvestigator");
    public static MvcActionKey ChiefInvestigatorContact { get; set; } = new("StudyRequest", "ChiefInvestigatorContact");
    public static MvcActionKey MainContact { get; set; } = new("StudyRequest", "MainContact");
    public static MvcActionKey Section2Check { get; set; } = new("StudyRequest", "Section2Check");
    public static MvcActionKey SponsorOrganisation { get; set; } = new("StudyRequest", "SponsorOrganisation");
    public static MvcActionKey Section3Check { get; set; } = new("StudyRequest", "Section3Check");
    public static MvcActionKey ResearchManager { get; set; } = new("StudyRequest", "ResearchManager");
    public static MvcActionKey ResearchLocations { get; set; } = new("StudyRequest", "ResearchLocations");
    public static MvcActionKey StudyDescription { get; set; } = new("StudyRequest", "StudyDescription");


    public static MvcFlowGraph<StudyRequestViewModel, StudyEditContext, FlowAction> Graph { get; }

    static StudyRequestFlow()
    {
        Graph = new MvcFlowGraph<StudyRequestViewModel, StudyEditContext, FlowAction>();

        AddTransition(EthicsApproval, InclusionInRdnPortfolio, TransitionFlags.BackToCheckScreen | TransitionFlags.NextToCheckScreen);
        AddTransition(InclusionInRdnPortfolio, FinishRecruiting,
            TransitionFlags.BackToCheckScreen | TransitionFlags.NextToCheckScreen,
            i => i.InclusionInRdnPortfolioStatus is SubmittedType.Yes);
        AddTransition(InclusionInRdnPortfolio, NihrFunding, TransitionFlags.None,
            i => i.InclusionInRdnPortfolioStatus is not SubmittedType.Yes);
        AddTransition(NihrFunding, FinishRecruiting, TransitionFlags.BackToCheckScreen,
            i => i.InclusionInRdnPortfolioStatus is not SubmittedType.Yes && i.NihrFundingStatus is not NihrFundingStatusType.No);
        AddTransition(NihrFunding, MoreInformationRequired, TransitionFlags.None,
            i => i.NihrFundingStatus is NihrFundingStatusType.No);
        AddTransition(FinishRecruiting, StudyDescription, TransitionFlags.BackToCheckScreen | TransitionFlags.NextToCheckScreen);
        AddTransition(StudyDescription, ResearchLocations, TransitionFlags.BackToCheckScreen | TransitionFlags.NextToCheckScreen);
        AddTransition(ResearchLocations, ResearchManager, TransitionFlags.BackToCheckScreen | TransitionFlags.NextToCheckScreen);
        AddTransition(ResearchManager, ChiefInvestigator, TransitionFlags.BackToCheckScreen | TransitionFlags.NextToCheckScreen);
        AddTransition(ChiefInvestigator, ChiefInvestigatorContact, TransitionFlags.BackToCheckScreen | TransitionFlags.NextToCheckScreen);
        AddTransition(ChiefInvestigatorContact, Section2Check, TransitionFlags.BackToCheckScreen | TransitionFlags.NextToCheckScreen,
            model => model.IsChiefInvestigatorMainContact is true);
        AddTransition(ChiefInvestigatorContact, MainContact, TransitionFlags.BackToCheckScreen | TransitionFlags.NextToCheckScreen, 
            model => model.IsChiefInvestigatorMainContact is not true); 
        AddTransition(MainContact, Section2Check, TransitionFlags.NextToCheckScreen);
        AddTransition(Section2Check, SponsorOrganisation, TransitionFlags.BackToCheckScreen | TransitionFlags.NextToCheckScreen);
        AddTransition(SponsorOrganisation, Section3Check, TransitionFlags.NextToCheckScreen);
        AddTransition(Section3Check, ParticipantDetails, TransitionFlags.BackToCheckScreen);
        AddTransition(ParticipantDetails, Summary, TransitionFlags.BackToCheckScreen | TransitionFlags.NextToCheckScreen);
    }


    static void AddTransition(
        MvcActionKey from,
        MvcActionKey to,
        TransitionFlags flags = TransitionFlags.None,
        Predicate<StudyRequestViewModel>? modelPredicate = null)
    {
        Graph.AddTransition(
            from, to, i => i is FlowAction.Next, 
            modelPredicate: modelPredicate,
            destinationTransform: flags.HasFlag(TransitionFlags.NextToCheckScreen) ? CheckScreenTransform : null);
        
        Graph.AddTransition(
            to, from, i => i is FlowAction.Back, 
            modelPredicate: modelPredicate, 
            destinationTransform: flags.HasFlag(TransitionFlags.BackToCheckScreen) ? CheckScreenTransform : null);
    }

    private static TransitionResult<StudyEditContext, MvcActionKey> CheckScreenTransform(
        StudyEditContext context, TransitionResult<StudyEditContext, MvcActionKey> transitionResult)
    {
        if (context.HasCheckAction)
        {
            transitionResult.NodeKey = MvcActionKey.Parse(context.CheckAction!);
            transitionResult.Context.CheckAction = null;
        }
        return transitionResult;
    }

    [Flags]
    enum TransitionFlags
    {
        None = 0x0,
        BackToCheckScreen = 0x1,
        NextToCheckScreen = 0x2,
    }
}


public record StudyEditContext
{
    public int StudyId { get; set; }
    public StudyEditFlowType FlowType { get; set; }
    public string? CheckAction { get; set; }
    
    [JsonIgnore]
    public bool HasCheckAction => !string.IsNullOrEmpty(CheckAction);
}

public enum StudyEditFlowType
{
    AdminCreate,
    ResearcherCreate,
}