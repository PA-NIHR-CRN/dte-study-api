using BPOR.Rms.Models.Email;
using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Controllers;

public class EmailController : Controller
{
    public Task<IActionResult> SetupCampaign()
    {
        return Task.FromResult<IActionResult>(View());
    }
    
    [HttpPost]
    public IActionResult SetupCampaign(SetupCampaignViewModel model)
    {
        return View(model);
    }
    [HttpPost]
    public IActionResult SendEmail(SetupCampaignViewModel model)
    {
        return RedirectToAction("EmailSuccess", model);
    }
    
    
    public Task<IActionResult> EmailSuccess(EmailSuccessViewModel model)
    {
        return Task.FromResult<IActionResult>(View(model));
    }
}
