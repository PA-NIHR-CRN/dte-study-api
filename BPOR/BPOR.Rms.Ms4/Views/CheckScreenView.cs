using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Routing;
using NIHR.Infrastructure.AspNetCore.Authentication.AccessToken;

namespace BPOR.Rms.Ms4.Views;

public abstract class CheckScreenView : RazorPage<StudyRequestViewModel>
{
    [RazorInject]
    public IUrlAccessTokenService UrlAccessTokenService { get; set; }
    
    [RazorInject]
    public IMvcFlowHelper mvcFlowHelper {get; set; }
    
    protected string GetChangeUrl(MvcActionKey node)
    {
        var studyEditContext = (StudyEditContext)ViewData["StudyEditContext"];
        var actionContext =  new UrlActionContext {Action = node.Action, Controller = node.Controller, Values = studyEditContext with
            {
                CheckAction = mvcFlowHelper.CurrentActionKey.ToString()
            }};
        string changeUrl = mvcFlowHelper.UrlHelper.Action(actionContext);
        changeUrl = UrlAccessTokenService.AddCurrentAccessToken(changeUrl);
        return changeUrl;
    }
}