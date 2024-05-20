using BPOR.Rms.Models.Email;
using BPOR.Rms.Models.Researcher;
using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Controllers;

public class ResearcherController: Controller
{
    public IActionResult Create()
    {
        ViewData["ShowBackLink"] = true;
        return View(new ResearcherStudyFormViewModel());
    }
    

    [HttpPost]
    public IActionResult Create(ResearcherStudyFormViewModel model)
    {
        ViewData["ShowBackLink"] = true;

        if (model.Password?.Length < 12)
        {
            ModelState.AddModelError("Password", "Enter a password that is at least 12 characters long and does not include any symbols");
        }

        if (model.Password != model.ConfirmPassword)
        {
            ModelState.AddModelError("ConfirmPassword", "Password does not match confirmation");
        }

        if (ModelState.IsValid)
        {
            return View("AddResearcherSuccess");
        }

        return View(model);
    }
    
}
