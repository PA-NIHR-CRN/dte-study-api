using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Views.ViewComponents;

public class BreadcrumbItem(string text, string url)
{
    public string Text { get; set; } = text;
    public string Url { get; set; } = url;
}

public class BreadcrumbsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<BreadcrumbItem> items)
    {
        return View(items);
    }
}
