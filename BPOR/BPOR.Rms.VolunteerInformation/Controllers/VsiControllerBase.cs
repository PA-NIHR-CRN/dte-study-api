using BPOR.Domain.Entities;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Models;
using BPOR.Rms.VolunteerInformation.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[AllowAnonymous]
public abstract class VsiControllerBase : Controller
{
    protected IVsiRepository VsiRepository { get; }

    protected VsiControllerBase(IVsiRepository vsiRepository)
    {
        VsiRepository = vsiRepository;
    }

    protected VsiEditContext EditContext
    {
        get => ViewBag.Context;
        set => ViewBag.Context = value;
    }
    
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
        var studyIdValue = context.RouteData.Values["studyId"];

        if (actionDescriptor.MethodInfo.HasAttribute<DoNotRequireVsiAttribute>())
        {
            // do nothing.
        }
        else if (studyIdValue is string studyIdString 
                 && int.TryParse(studyIdString, out var studyId))
        {
            EditContext = new VsiEditContext { StudyId = studyId };
            var currentVsi = await VsiRepository.GetPage(studyId, context.HttpContext.RequestAborted);
            if (currentVsi == null)
            {
                context.Result = new NotFoundResult();
                return;
            }
        }
        else
        {
            context.Result = new NotFoundResult();
        }
          

        await base.OnActionExecutionAsync(context, next);
    }
}

public class DoNotRequireVsiAttribute : Attribute
{
    
}