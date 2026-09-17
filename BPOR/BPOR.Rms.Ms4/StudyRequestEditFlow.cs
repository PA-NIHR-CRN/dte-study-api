using BPOR.Domain.Enums;
using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Models;

namespace BPOR.Rms.Ms4;

public class StudyRequestEditFlow : MvcFlowGraph<StudyRequestViewModel, StudyRequestEditContext>
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
    public static MvcActionKey SponsorOrganisation { get; } = new("StudyRequest", "SponsorOrganisation");
    public static MvcActionKey ResearchManager { get; } = new("StudyRequest", "ResearchManager");
    public static MvcActionKey ResearchLocations { get; } = new("StudyRequest", "ResearchLocations");
    public static MvcActionKey StudyDescription { get; } = new("StudyRequest", "StudyDescription");
    
    // TODO: Find a better way to handle a return-subflow-only edge.
    private static MvcActionKey NullPage { get; } = new("", "");

    public StudyRequestEditFlow()
    {
        AddTransition(EthicsApproval, InclusionInRdnPortfolio,
            SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit);
        AddTransition(InclusionInRdnPortfolio, FinishRecruiting,
            SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit,
            i => i.InclusionInRdnPortfolioStatus is SubmittedType.Yes);
        AddTransition(InclusionInRdnPortfolio, NihrFunding, SubflowOptions.None,
            i => i.InclusionInRdnPortfolioStatus is not SubmittedType.Yes);
        AddTransition(
            NihrFunding, NullPage, MvcFlowAction.Next,
            contextPredicate: context => context.FlowType == StudyRequestEditFlowType.Edit,
            isSubflowReturn: true);
        AddTransition(NihrFunding, FinishRecruiting, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit,
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
        AddTransition(ChiefInvestigatorContact, SponsorOrganisation, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit,
            model => model.IsChiefInvestigatorMainContact is true);
        AddTransition(ChiefInvestigatorContact, MainContact, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit,
            model => model.IsChiefInvestigatorMainContact is not true);
        AddTransition(MainContact, SponsorOrganisation, SubflowOptions.SubflowExit);
        AddTransition(SponsorOrganisation, ParticipantDetails, SubflowOptions.SubflowExit);
        AddTransition(ParticipantDetails, Summary, SubflowOptions.SubflowEntry | SubflowOptions.SubflowExit);
    }

    private void AddTransition(
        MvcActionKey from,
        MvcActionKey to,
        SubflowOptions flags = SubflowOptions.None,
        Predicate<StudyRequestViewModel>? modelPredicate = null)
    {
        AddTransition(
            from, to, MvcFlowAction.Next,
            modelPredicate: modelPredicate,
            isSubflowReturn: flags.HasFlag(SubflowOptions.SubflowExit));

        AddTransition(
            to, from, MvcFlowAction.Back,
            modelPredicate: modelPredicate,
            isSubflowReturn: flags.HasFlag(SubflowOptions.SubflowEntry));
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