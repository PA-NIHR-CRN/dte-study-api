using BPOR.Rms.Ms4.FlowGraph;
using BPOR.Rms.Ms4.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using BPOR.Rms.Infrastructure.AspNetCore.Authentication.AccessToken;

namespace BPOR.Rms.Ms4.Views;

public abstract class CheckScreenView : RazorPage<StudyRequestViewModel>
{
    [RazorInject]
    public IUrlAccessTokenService UrlAccessTokenService { get; set; }
    
    [RazorInject]
    public IMvcFlowHelper<StudyRequestViewModel, StudyRequestEditContext> mvcFlowHelper {get; set; }
    
    protected string GetChangeUrl(MvcActionKey node)
    {
        var studyEditContext = (StudyRequestEditContext)ViewData["StudyEditContext"];
        string changeUrl = mvcFlowHelper.GetSubflowUrl(node, studyEditContext);
        changeUrl = UrlAccessTokenService.AddCurrentAccessToken(changeUrl);
        return changeUrl;
    }
}