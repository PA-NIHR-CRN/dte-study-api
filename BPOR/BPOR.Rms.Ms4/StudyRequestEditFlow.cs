using BPOR.Domain.Enums;
using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Models;

namespace BPOR.Rms.Ms4;

public static class StudyRequestEditFlow
{
    public static MvcActionKey EthicsApproval { get; } = new("StudyRequest", "EthicsApproval");
    public static MvcActionKey InclusionInRdnPortfolio { get; } = new("StudyRequest", "InclusionInRdnPortfolio");
    public static MvcActionKey FinishRecruiting { get; } = new("StudyRequest", "FinishRecruiting");
    public static MvcActionKey NihrFunding { get; } = new("StudyRequest", "NihrFunding");
    public static MvcActionKey MoreInformationRequired { get; } = new("StudyRequest", "MoreInformationRequired");
    public static MvcActionKey Summary { get; } = new("StudyRequest", "Summary");
    public static MvcActionKey ParticipantDetails { get; } = new("StudyRequest", "ParticipantDetails");
    public static MvcActionKey ChiefInvestigator { get; } = new("StudyRequest", "ChiefInvestigator");
    public static MvcActionKey ChiefInvestigatorContact { get; } = new("StudyRequest", "ChiefInvestigatorContact");
    public static MvcActionKey MainContact { get; } = new("StudyRequest", "MainContact");
    public static MvcActionKey Section2Check { get; } = new("StudyRequest", "Section2Check");
    public static MvcActionKey SponsorOrganisation { get; } = new("StudyRequest", "SponsorOrganisation");
    public static MvcActionKey Section3Check { get; } = new("StudyRequest", "Section3Check");
    public static MvcActionKey ResearchManager { get; } = new("StudyRequest", "ResearchManager");
    public static MvcActionKey ResearchLocations { get; } = new("StudyRequest", "ResearchLocations");
    public static MvcActionKey StudyDescription { get; } = new("StudyRequest", "StudyDescription");

    public static MvcFlowGraph<StudyRequestViewModel, StudyRequestEditContext, MvcFlowAction> Graph { get; }

    static StudyRequestEditFlow()
    {
        Graph = new MvcFlowGraph<StudyRequestViewModel, StudyRequestEditContext, MvcFlowAction>();

        AddTransition(EthicsApproval, InclusionInRdnPortfolio,
            SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit);
        AddTransition(InclusionInRdnPortfolio, FinishRecruiting,
            SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit,
            i => i.InclusionInRdnPortfolioStatus is SubmittedType.Yes);
        AddTransition(InclusionInRdnPortfolio, NihrFunding, SubflowOptions.None,
            i => i.InclusionInRdnPortfolioStatus is not SubmittedType.Yes);
        AddTransition(NihrFunding, FinishRecruiting, SubflowOptions.SubflowEntry,
            i => i.InclusionInRdnPortfolioStatus is not SubmittedType.Yes &&
                 i.NihrFundingStatus is not NihrFundingStatusType.No);
        AddTransition(NihrFunding, MoreInformationRequired, SubflowOptions.None,
            i => i.NihrFundingStatus is NihrFundingStatusType.No);
        AddTransition(FinishRecruiting, StudyDescription, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit);
        AddTransition(StudyDescription, ResearchLocations, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit);
        AddTransition(ResearchLocations, ResearchManager, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit);
        AddTransition(ResearchManager, ChiefInvestigator, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit);
        AddTransition(ChiefInvestigator, ChiefInvestigatorContact,
            SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit);
        AddTransition(ChiefInvestigatorContact, Section2Check, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit,
            model => model.IsChiefInvestigatorMainContact is true);
        AddTransition(ChiefInvestigatorContact, MainContact, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit,
            model => model.IsChiefInvestigatorMainContact is not true);
        AddTransition(MainContact, Section2Check, SubflowOptions.SubflowExit);
        AddTransition(Section2Check, SponsorOrganisation, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit);
        AddTransition(SponsorOrganisation, Section3Check, SubflowOptions.SubflowExit);
        AddTransition(Section3Check, ParticipantDetails, SubflowOptions.SubflowEntry);
        AddTransition(ParticipantDetails, Summary, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit);
    }

    private static void AddTransition(
        MvcActionKey from,
        MvcActionKey to,
        SubflowOptions flags = SubflowOptions.None,
        Predicate<StudyRequestViewModel>? modelPredicate = null)
    {
        Graph.AddTransition(
            from, to, MvcFlowAction.Next,
            modelPredicate: modelPredicate,
            destinationTransform: flags.HasFlag(SubflowOptions.SubflowExit) ? HandleSubflow : null);

        Graph.AddTransition(
            to, from, MvcFlowAction.Back,
            modelPredicate: modelPredicate,
            destinationTransform: flags.HasFlag(SubflowOptions.SubflowEntry) ? HandleSubflow : null);
    }

    private static TransitionResult<StudyRequestEditContext, MvcActionKey> HandleSubflow(
        StudyRequestEditContext context, TransitionResult<StudyRequestEditContext, MvcActionKey> transitionResult)
    {
        // TODO: Implement subflows generically using a stack serialised to the URL query.
        // Each stack frame needs to consist of the context and mvc action of the calling action - this needs to be
        // really compact, so positional serialisation, enums as numeric values, short-forms for action names etc.
        if (!string.IsNullOrWhiteSpace(context.SubflowRtnAct))
        {
            transitionResult.NodeKey = MvcActionKey.Parse(context.SubflowRtnAct);
            transitionResult.Context.SubflowRtnAct = null;
        }

        return transitionResult;
    }

    [Flags]
    private enum SubflowOptions
    {
        None = 0x0,
        /// <summary>
        /// The transition additionally acts as a sub flow entry point.
        /// </summary>
        SubflowEntry = 0x1,
        /// <summary>
        /// The transition additionally acts as a sub flow exit point.
        /// </summary>
        SubflowExit = 0x2,
    }
}