using System.Diagnostics.CodeAnalysis;
using BPOR.Rms.VolunteerInformation.Controllers;
using BPOR.Rms.VolunteerInformation.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NIHR.GovUk.AspNetCore.Mvc;

namespace BPOR.Rms.VolunteerInformation.Utility;

public static class UrlHelperExtensions
{
    public static string? BackAction(this IUrlHelper helper, VsiEditContext context,
        [AspMvcAction] string actionName)
    {
        var routeData = context.ToRouteData();
        routeData.Add("direction", nameof(FlowDirection.Back));
        return helper.Action(actionName, routeData);
    }

    public static string? FlowNext(this IUrlHelper helper, VsiEditContext context,
        [AspMvcAction] string actionName, [AspMvcController] string? controller = null)
        => Flow(helper, context, FlowDirection.Forward, actionName, controller);
    
    public static string? FlowBack(this IUrlHelper helper, VsiEditContext context,
        [AspMvcAction] string actionName, [AspMvcController] string? controller = null)
        => Flow(helper, context, FlowDirection.Back, actionName, controller);
    
    private static string? Flow(this IUrlHelper helper, VsiEditContext context, FlowDirection direction, 
        [AspMvcAction] string actionName, [AspMvcController] string? controller = null)
    {
        var values = context.ToRouteData();
        if (direction != FlowDirection.Forward)
        {
            values.Add("direction", direction.ToString());
        }
        
        return context.FlowMode switch
        {
            VipFlowMode.Edit => helper.Action(new UrlActionContext(){Action = "Section4", Controller = "VolunteerInformationPage", Values = values}),
            VipFlowMode.Create => helper.Action(new UrlActionContext(){Action = actionName, Controller = controller, Values = values}),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public static string? FlowNext(this IUrlHelper helper, VsiEditContext context,
        Func<IUrlHelper, string?> createAction)
    {
        return context.FlowMode switch
        {
            VipFlowMode.Edit => helper.Action(new UrlActionContext(){Action = "Section4", Controller = "VolunteerInformationPage", Values = context}),
            VipFlowMode.Create => createAction(helper),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public static void SetSkipLink(this ViewDataDictionary viewData, string? backLinkOverride)
    {
        viewData[SkipLinkKey] = backLinkOverride;
        viewData.ShowBackLink(backLinkOverride != null);
    }
    
    public static bool TryGetSkipLink(this ViewDataDictionary viewData, [MaybeNullWhen(false)] out string result)
    {
        return viewData.TryGetValueAs(SkipLinkKey, out result);
    }
    
    public static void SetSectionName(this ViewDataDictionary viewData, string? backLinkOverride)
    {
        viewData[SectionNameKey] = backLinkOverride;
        viewData.ShowBackLink(backLinkOverride != null);
    }
    
    public static string? GetSectionName(this ViewDataDictionary viewData) => viewData.GetValueAs<string>(SectionNameKey);
    
    public static TValue? GetValueAs<TValue>(this ViewDataDictionary viewData, string key)
    {
        TryGetValueAs(viewData, key, out TValue? result);
        return result;
    }

    public static bool TryGetValueAs<TValue>(this ViewDataDictionary viewData, string key, out TValue? value)
    {
        if (viewData.TryGetValue(key, out var resultObject) && resultObject is TValue castValue)
        {
            value = castValue;
            return true;
        }

        value = default;
        return false;
    }

    private const string SectionNameKey = "SectionName";
    private const string SkipLinkKey = "SkipUrl";
}