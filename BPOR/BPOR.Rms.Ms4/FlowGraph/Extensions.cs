using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Ms4.FlowGraph;

public static class Extensions
{
    public static string? GetUrl(this IUrlHelper helper, ITransitionResult<MvcActionKey> transitionResult)
        => helper.GetUrl(transitionResult.NodeKey, transitionResult.Context);
    
    public static string? GetUrl(this IUrlHelper helper, MvcActionKey mvcAction, object? parameters)
        => helper.Action(mvcAction.Action, mvcAction.Controller, parameters);
}