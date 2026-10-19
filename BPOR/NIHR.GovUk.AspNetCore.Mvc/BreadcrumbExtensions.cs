using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NIHR.GovUk.AspNetCore.Mvc;

public class BreadcrumbModel
{
    public string CurrentPageCaption { get; set; }
    public BreadcrumbItem[] Ancestors { get; set; }
}
public class BreadcrumbItem(string text, string url)
{
    public string Text { get; set; } = text;
    public string Url { get; set; } = url;
}

public static class BreadcrumbExtensions
{
    public static void AddBreadcrumb(this ViewDataDictionary viewData, string text, string url)
    {
        if (!(viewData["Breadcrumbs"] is List<BreadcrumbItem> breadcrumbs))
        {
            breadcrumbs = new List<BreadcrumbItem>();
            viewData["Breadcrumbs"] = breadcrumbs;
        }

        breadcrumbs.Add(new BreadcrumbItem(text, url));
    }

    public static void AddBreadcrumbs(this ViewDataDictionary viewData, string currentPageCaption,
        params BreadcrumbItem[] ancestors)
    {
        viewData["Breadcrumbs"] = new BreadcrumbModel
        {
            CurrentPageCaption = currentPageCaption,
            Ancestors = ancestors
        };
    }
}

