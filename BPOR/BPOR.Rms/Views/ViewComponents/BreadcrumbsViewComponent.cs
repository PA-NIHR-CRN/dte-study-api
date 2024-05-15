using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BPOR.Rms.Views.ViewComponents;

public class BreadcrumbItem(string text, string url)
{
    public string Text { get; set; } = text;
    public string Url { get; set; } = url;
}

public class BreadcrumbsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
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

    public static void AddBreadcrumbs(this ViewDataDictionary viewData, Dictionary<string, string> items)
    {
        if (!(viewData["Breadcrumbs"] is List<BreadcrumbItem> breadcrumbs))
        {
            breadcrumbs = new List<BreadcrumbItem>();
            viewData["Breadcrumbs"] = breadcrumbs;
        }

        foreach (var item in items)
        {
            breadcrumbs.Add(new BreadcrumbItem(item.Key, item.Value));
        }
    }
}

