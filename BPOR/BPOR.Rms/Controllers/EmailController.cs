using BPOR.Rms.Models;
using BPOR.Rms.Models.Email;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BPOR.Rms.Controllers;

public class EmailController : Controller
{
    public IActionResult SetupCampaign(SetupCampaignViewModel model)
    {
        if (TempData["Notification"] != null)
        {
            model.Notification =
                JsonConvert.DeserializeObject<NotificationBannerModel>(TempData["Notification"].ToString());
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult SetupCampaign()
    {
        return View();
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

    public IActionResult SendPreviewEmail(SetupCampaignViewModel model)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("EmailSuccess", model);
        }

        if (model.PreviewEmails != null)
        {
            var emails = model.PreviewEmails.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var email in emails)
            {
                if (!IsValidEmail(email))
                {
                    ModelState.AddModelError("PreviewEmails", "Please enter a valid email address.");
                    break;
                }
            }
        }

        ModelState.Remove("MaxNumbers");
        ModelState.Remove("TotalVolunteers");
        ModelState.Remove("StudyName");
        ModelState.Remove("SelectedTemplate");

        TempData["Notification"] = JsonConvert.SerializeObject(new NotificationBannerModel
        {
            IsSuccess = true,
            Heading = "Success",
            Body = $"Preview email has been sent to{model.PreviewEmails}"
        });

        return RedirectToAction("SetupCampaign", model);
    }


    [HttpPost]
    public IActionResult HandleForms(SetupCampaignViewModel model, string action)
    {
        switch (action)
        {
            case "SetupCampaign":
                return RedirectToAction("SetupCampaign");
                break;
            case "SendPreviewEmail":
                return SendPreviewEmail(model);
                break;
            default:
                break;
        }

        return RedirectToAction("SetupCampaign");
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
