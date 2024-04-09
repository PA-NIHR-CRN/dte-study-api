using BPOR.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Controllers;

public class FilterController(AuroraDbContext context) : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
