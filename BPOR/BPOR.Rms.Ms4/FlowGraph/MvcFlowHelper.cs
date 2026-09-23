using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

namespace BPOR.Rms.Ms4.FlowGraph;

public class MvcFlowHelper<TModel, TContext> : MvcFlowHelper<TContext>, IMvcFlowHelper<TModel, TContext> 
    where TContext : MvcFlowContextBase
{
    private readonly IMvcFlowGraph<TModel, TContext> _graph;
    
    public MvcFlowHelper(IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory, IMvcFlowGraph<TModel, TContext> graph)
        : base(actionContextAccessor, urlHelperFactory, graph)
    {
        _graph = graph;
    }
    
    public string? GetRelatedUrl(TModel model, TContext context, MvcFlowAction action)
    {
        var nextAction = _graph.ApplyTransition(CurrentActionKey, context, model, action);

        if (nextAction == null)
        {
            return null;
        }

        if (nextAction.ReturnFromSubflow && !string.IsNullOrEmpty(nextAction.Context.ReturnUrl))
        {
            return nextAction.Context.ReturnUrl;
        }

        string? result = UrlHelper.GetUrl(nextAction);
        if (result == null)
        {
            throw new Exception($"{nextAction.NodeKey} could not be mapped to a URL");
        }
        
        return result;
    }
}

public class MvcFlowHelper<TContext> : IMvcFlowHelper<TContext> 
    where TContext : MvcFlowContextBase
{
    private readonly IMvcFlowGraph<TContext> _graph;
    private const string _controllerRouteKey = "controller";
    private const string _actionRouteKey = "action";

    protected readonly Lazy<ActionContext> _actionContext;
    protected readonly Lazy<IUrlHelper> _urlHelper;
    protected readonly Lazy<MvcActionKey> _currentActionKey;

    public MvcFlowHelper(IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory, IMvcFlowGraph<TContext> graph)
    {
        _graph = graph;
        _actionContext = new Lazy<ActionContext>(() => 
            actionContextAccessor.ActionContext ?? throw new InvalidOperationException("ActionContext undefined"));
        _urlHelper = new Lazy<IUrlHelper>(() => urlHelperFactory.GetUrlHelper(_actionContext.Value));
        _currentActionKey = new Lazy<MvcActionKey>(GetCurrentActionKey);
    }
    
    private MvcActionKey GetCurrentActionKey()
    {
        var controllerName = _actionContext.Value.RouteData.Values[_controllerRouteKey]!.ToString()!;
        var actionName = _actionContext.Value.RouteData.Values[_actionRouteKey]!.ToString()!;
        return new MvcActionKey(controllerName, actionName);
    }

    public double? CalculateBestCaseProgress(TContext context, MvcActionKey start, MvcActionKey end)
    {
        return _graph.CalculateBestCaseProgress(context, start, end, CurrentActionKey);
    }

    public MvcActionKey CurrentActionKey => _currentActionKey.Value;
    public IUrlHelper UrlHelper => _urlHelper.Value;

    public string GetSubflowUrl(MvcActionKey node, TContext context, string? returnPathOverride = null)
    {
        var actionContext =  new UrlActionContext {Action = node.Action, Controller = node.Controller, Values = context with
        {
            ReturnUrl = returnPathOverride ?? _actionContext.Value.HttpContext.Request.GetEncodedPathAndQuery()
        }};
        string result = UrlHelper.Action(actionContext);
        if (result == null)
        {
            throw new Exception($"{node} could not be mapped to a URL");
        }
        return result;
    }
}