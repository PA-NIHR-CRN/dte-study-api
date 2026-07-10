using Amazon.Runtime.Internal;
using BPOR.Rms.VolunteerInformation.Models;
using BPOR.Rms.VolunteerInformation.Utility;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using NIHR.GovUk.AspNetCore.Mvc;

namespace BPOR.Rms.VolunteerInformation.Views.VolunteerInformationPage;

public abstract class ViewBase<T> : RazorPage<T>
{
    [RazorInject]
    public IUrlHelper UrlHelper { get; set; }

    protected void SetFlowBackAction([AspMvcAction] string action, [AspMvcController] string? controller = null)
    {
        ViewData.SetBackLinkOverride(UrlHelper.FlowBack(EditContext, action, controller ));
    }
    
    protected VsiEditContext EditContext => ViewBag.Context;
}