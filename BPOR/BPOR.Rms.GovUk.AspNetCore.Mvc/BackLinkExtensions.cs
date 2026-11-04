using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NIHR.GovUk.AspNetCore.Mvc;

public static class BackLinkExtensions
{
    private const string BackLinkOverrideKey = "BackLinkOverride";

    public static void ShowBackLink(this ViewDataDictionary viewData, bool? showBackLink = true)
    {
        viewData["ShowBackLink"] = showBackLink;
    }

    public static bool? IsBackLinkEnabled(this ViewDataDictionary viewData)
    {
        return viewData["ShowBackLink"] as bool?;
    }
    public static string? GetBackLinkOverride(this ViewDataDictionary viewData)
    {
        return viewData[BackLinkOverrideKey] as string;
    }
    
    public static void SetBackLinkOverride(this ViewDataDictionary viewData, string? backLinkOverride)
    {
        viewData[BackLinkOverrideKey] = backLinkOverride;
        viewData.ShowBackLink(backLinkOverride != null);
    }
}
