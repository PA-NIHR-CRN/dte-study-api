using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Controllers;

public class FilterController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
