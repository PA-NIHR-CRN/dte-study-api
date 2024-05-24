using BPOR.Rms.Models.Email;
using BPOR.Rms.Models.Researcher;
using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Controllers;

public class ResearcherController: Controller
{
    public IActionResult Create()
    {
        ViewData["ShowBackLink"] = true;
        return View(new ResearcherFormViewModel());
    }
    

    [HttpPost]
    public IActionResult Create(ResearcherFormViewModel model)
    {
        ViewData["ShowBackLink"] = true;

        if (model.Password?.Length < 12)
        {
            ModelState.AddModelError("Password", "Enter a password that is at least 12 characters long and does not include any symbols");
        }

        if (ModelState.IsValid)
        {
            return View("AddResearcherSuccess");
        }

        return View(model);
    }

    public IActionResult CreateStudy()
    {
        return View(new ResearcherStudyFormViewModel());
    }

    [HttpPost]
    public IActionResult CreateStudy(ResearcherStudyFormViewModel model)
    {
        if (!model.AgreedToTermsAndConditions)
        {
            ModelState.AddModelError("AgreedToTermsAndConditions", "Confirm that the terms and conditions have been read and agreed before applying");
        }
        return View(model);
    }

}
