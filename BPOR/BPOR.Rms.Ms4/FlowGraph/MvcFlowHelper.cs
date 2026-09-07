using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BPOR.Rms.Ms4.FlowGraph;

public class MvcFlowHelper : IMvcFlowHelper
{
    private Lazy<IUrlHelper> _urlHelper;
    private readonly IActionContextAccessor _actionContextAccessor;
    private readonly Lazy<MvcActionKey> _currentActionKey;

    public MvcFlowHelper(IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory)
    {
        _urlHelper = new Lazy<IUrlHelper>(() => urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext));
        _currentActionKey = new Lazy<MvcActionKey>(() => GetCurrentActionKeyInternal());
        _actionContextAccessor = actionContextAccessor;
    }
    
    private MvcActionKey GetCurrentActionKeyInternal()
    {
        var controllerName = _actionContextAccessor.ActionContext.RouteData.Values["controller"]!.ToString()!;
        var actionName = _actionContextAccessor.ActionContext.RouteData.Values["action"]!.ToString()!;
        return new MvcActionKey(controllerName, actionName);
    }

    public MvcActionKey CurrentActionKey => _currentActionKey.Value;
    public IUrlHelper UrlHelper => _urlHelper.Value;
}