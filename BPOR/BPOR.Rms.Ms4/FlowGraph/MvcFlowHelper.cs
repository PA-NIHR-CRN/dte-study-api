using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BPOR.Rms.Ms4.FlowGraph;

public class MvcFlowHelper : IMvcFlowHelper
{
    private const string _controllerRouteKey = "controller";
    private const string _actionRouteKey = "action";

    private readonly Lazy<ActionContext> _actionContext;
    private readonly Lazy<IUrlHelper> _urlHelper;
    private readonly Lazy<MvcActionKey> _currentActionKey;

    public MvcFlowHelper(IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory)
    {
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

    public MvcActionKey CurrentActionKey => _currentActionKey.Value;
    public IUrlHelper UrlHelper => _urlHelper.Value;
}