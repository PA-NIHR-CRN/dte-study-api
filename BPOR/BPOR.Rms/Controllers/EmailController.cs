using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BPOR.Rms.Controllers;

public class EmailController(IEmailCampaignService emailCampaignService) : Controller
{
    public IActionResult SetupCampaign(SetupCampaignViewModel model)
    {
        ModelState.Clear();

        return View(model);
    }

    public IActionResult Index(SetupCampaignViewModel model)
    {
        ModelState.Remove("MaxNumbers");
        ModelState.Remove("TotalVolunteers");
        ModelState.Remove("StudyName");
        ModelState.Remove("SelectedTemplate");
        if (TempData["Notification"] != null)
        {
            model.Notification =
                JsonConvert.DeserializeObject<NotificationBannerModel>(TempData["Notification"].ToString());
        }

        return View("SetupCampaign", model);
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(SetupCampaignViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove("PreviewEmails");

        if (model.TotalVolunteers > model.MaxNumbers)
        {
            ModelState.AddModelError("TotalVolunteers", "Total Volunteers must be less than or equal to Max Numbers.");
        }

        if (string.IsNullOrEmpty(model.SelectedTemplate))
        {
            ModelState.AddModelError("SelectedTemplate", "Please select a email template.");
        }

        if (!ModelState.IsValid)
        {
            return View("SetupCampaign", model);
        }

        await emailCampaignService.SendCampaignAsync(new EmailCampaign
        {
            FilterCriteriaId = model.FilterCriteriaId,
            TargetGroupSize = model.TotalVolunteers.Value,
            EmailTemplateId = new Guid(model.SelectedTemplate)
        }, cancellationToken);

        // implement email sending
        // get list of volunteers for the filter criteria
        // get a random sample of volunteers including prioticising those who have not been contacted before
        // send email to volunteers
        // mark volunteers as contacted in the database with the email template used

        return RedirectToAction("EmailSuccess", model);
    }


    public Task<IActionResult> EmailSuccess(EmailSuccessViewModel model)
    {
        return Task.FromResult<IActionResult>(View(model));
    }

    public IActionResult SendPreviewEmail(SetupCampaignViewModel model)
    {
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

        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", model);
        }

        TempData["Notification"] = JsonConvert.SerializeObject(new NotificationBannerModel
        {
            IsSuccess = true,
            Heading = "Success",
            Body = $"Preview email has been sent to {model.PreviewEmails}"
        });

        return RedirectToAction("Index", model);
    }


    [HttpPost]
    public async Task<IActionResult> HandleForms(SetupCampaignViewModel model, string action, CancellationToken cancellationToken)
    {
        return action switch
        {
            "SetupCampaign" => await SendEmail(model, cancellationToken),
            "SendPreviewEmail" => SendPreviewEmail(model),
            _ => RedirectToAction("SetupCampaign")
        };
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
