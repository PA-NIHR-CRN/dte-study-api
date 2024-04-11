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
        if (ModelState.IsValid)
        {
            // Do something

            return RedirectToAction("EmailSuccess", new EmailSuccessViewModel
            {
                Id = 1,
                StudyName = model.StudyName
            });
        }

        return RedirectToAction("EmailSuccess", new EmailSuccessViewModel
        {
            Id = 1,
            StudyName = model.StudyName
        });
    }

    public Task<IActionResult> EmailSuccess(EmailSuccessViewModel model)
    {
        return Task.FromResult<IActionResult>(View(model));
    }
}
