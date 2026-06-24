using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Models;
using BPOR.Rms.VolunteerInformation.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[AllowAnonymous]
public abstract class VipControllerBase<TContext> : Controller
    where TContext : VsiEditContext, new()
{
    protected IVipRepository VipRepository { get; }
    
    protected FlowDirection Direction { get; set; }

    protected VipControllerBase(IVipRepository vipRepository)
    {
        VipRepository = vipRepository;
    }

    protected TContext EditContext
    {
        get => ViewBag.Context;
        set => ViewBag.Context = value;
    }
    
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var editContext = new TContext();
        context.Result = await InitialiseEditContext(context, editContext, context.HttpContext.RequestAborted);
        EditContext = editContext;

        await base.OnActionExecutionAsync(context, next);
    }


    protected virtual async Task<IActionResult?> InitialiseEditContext(ActionExecutingContext context, TContext editContext,
        CancellationToken cancellationToken)
    {
        var studyIdValue = context.RouteData.Values["studyId"];
        var flowModeString = context.HttpContext.Request.Query["flowMode"].FirstOrDefault();
        var directionString = context.HttpContext.Request.Query["direction"].FirstOrDefault();

        editContext.FlowMode =
            !string.IsNullOrWhiteSpace(flowModeString) && Enum.TryParse<VipFlowMode>(flowModeString, out var flowMode)
                ? flowMode
                : VipFlowMode.Edit;
        Direction =
            !string.IsNullOrWhiteSpace(directionString) && Enum.TryParse<FlowDirection>(directionString, out var direction)
                ? direction
                : FlowDirection.Forward;
        
        if (studyIdValue is string studyIdString 
                 && int.TryParse(studyIdString, out var studyId))
        {
            editContext.StudyId = studyId;
            var currentVsi = await VipRepository.GetPage(studyId, cancellationToken); // TODO: Optimise
            if (currentVsi == null)
            {
                return new NotFoundResult();
            }
        }
        else
        {
            return new NotFoundResult();
        }

        return null;
    }
}