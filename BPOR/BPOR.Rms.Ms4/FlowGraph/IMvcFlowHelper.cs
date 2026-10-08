using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Ms4.FlowGraph;

public interface IMvcFlowHelper<TModel, TContext> : IMvcFlowHelper<TContext>
    where TContext : MvcFlowContextBase
{
    MvcActionKey CurrentActionKey { get; }
    IUrlHelper UrlHelper { get; }
    string? GetRelatedUrl(TModel model, TContext context, MvcFlowAction action);
}

public interface IMvcFlowHelper<TContext>
    where TContext : MvcFlowContextBase
{
    double? CalculateBestCaseProgress(TContext context, MvcActionKey start, MvcActionKey end);
    string GetSubflowUrl(MvcActionKey node, TContext context, string? returnPathOverride = null);
}