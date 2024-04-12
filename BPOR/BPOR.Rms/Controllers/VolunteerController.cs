using BPOR.Domain.Entities;
using BPOR.Rms.Models.Volunteer;
using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Controllers;

public class VolunteerController(ParticipantDbContext context) : Controller
{
    public IActionResult UpdateRecruited(UpdateRecruitedViewModel model)
    {
        return View(model);
    }
    
    [HttpPost]
    public IActionResult SubmitVolunteerNumbers(int studyId)
    {
        var studyRecruited = context.Studies.Find(studyId);
        return RedirectToAction("UpdateRecruited", studyRecruited);
    }
    
    public IActionResult UpdateAnonymousRecruited(UpdateAnonymousRecruitedViewModel model)
    {
        return View(model);
    }
    
    [HttpPost]
    public IActionResult UpdateRecuitmentTotal(int studyId)
    {
        var studyRecruited = context.Studies.Find(studyId);
        return RedirectToAction("UpdateAnonymousRecruited", studyRecruited);
    }
    
}
