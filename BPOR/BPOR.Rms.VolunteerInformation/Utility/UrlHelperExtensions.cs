using System.Diagnostics.CodeAnalysis;
using BPOR.Rms.VolunteerInformation.Controllers;
using BPOR.Rms.VolunteerInformation.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata;
using NIHR.GovUk.AspNetCore.Mvc;

namespace BPOR.Rms.VolunteerInformation.Utility;

public static class UrlHelperExtensions
{
    public static string? FlowNext(this IUrlHelper helper, VsiEditContext context,
        [AspMvcAction] string actionName)
    {
        return context.FlowMode switch
        {
            VipFlowMode.Edit => helper.Action(new UrlActionContext(){Action = "Section4", Controller = "VolunteerStudyInformation", Values = context}),
            VipFlowMode.Create => helper.Action(new UrlActionContext(){Action = actionName, Values = context}),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public static string? FlowNext(this IUrlHelper helper, VsiEditContext context,
        Func<IUrlHelper, string?> createAction)
    {
        return context.FlowMode switch
        {
            VipFlowMode.Edit => helper.Action(new UrlActionContext(){Action = "Section4", Controller = "VolunteerStudyInformation", Values = context}),
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