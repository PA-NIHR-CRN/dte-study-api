using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Controllers;

public class ResearcherController: Controller
{
    // GET: Study/Create
    public IActionResult Create()
    {
        ViewData["ShowBackLink"] = true;
        ViewData["ShowProgressBar"] = true;
        ViewData["ProgressPercentage"] = 0;
        return View();
    }
    
    // POST: Study/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    // [ValidateAntiForgeryToken]
    public IActionResult Create(string action)
    {
        if (action == "Next")
        {
            ViewData["ShowBackLink"] = true;
            ViewData["ShowProgressBar"] = true;
            ViewData["ProgressPercentage"] = 50;
            return View();
        }
        else if (action == "Save")
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
    
}
