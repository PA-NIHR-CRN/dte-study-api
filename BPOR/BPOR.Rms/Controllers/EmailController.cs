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
        model.EmailTemplates = FetchEmailTemplates();
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
        
        if (TempData.ContainsKey("SelectedTemplateId"))
        {
            model.SelectedTemplateId = TempData["SelectedTemplateId"].ToString();
            model.SelectedTemplateName = TempData["SelectedTemplateName"].ToString();
        }
        
        model.EmailTemplates = FetchEmailTemplates();

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

        if (string.IsNullOrEmpty(model.SelectedTemplateId))
        {
            ModelState.AddModelError("SelectedTemplate", "Please select an email template.");
        }

        if (!ModelState.IsValid)
        {
            return View("SetupCampaign", model);
        }

        await emailCampaignService.SendCampaignAsync(new EmailCampaign
        {
            FilterCriteriaId = model.FilterCriteriaId,
            TargetGroupSize = model.TotalVolunteers.Value,
            EmailTemplateId = new Guid(model.SelectedTemplateId),
            Name = model.SelectedTemplateName
        }, cancellationToken);

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
        ModelState.Remove("SelectedTemplate.Name");
        
        // TODO can we cache this or is there a better way to do this?
        var emailTemplates = FetchEmailTemplates();

        if (model.SelectedTemplateId != null)
        {
            var selectedTemplateName = emailTemplates.FirstOrDefault(t => t.Id == model.SelectedTemplateId)?.Name;
            if (selectedTemplateName != null)
            {
                model.SelectedTemplateName = selectedTemplateName;
            }
        }


        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", model);
        }
        
        TempData["SelectedTemplateId"] = model.SelectedTemplateId;
        TempData["SelectedTemplateName"] = model.SelectedTemplateName;
        TempData["Notification"] = JsonConvert.SerializeObject(new NotificationBannerModel
        {
            IsSuccess = true,
            Heading = "Success",
            Body =
                $"Preview email using template {model.SelectedTemplateName} has been sent to  {model.PreviewEmails}"
        });

        return RedirectToAction("Index", model);
    }


    [HttpPost]
    public async Task<IActionResult> HandleForms(SetupCampaignViewModel model, string action,
        CancellationToken cancellationToken)
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

    private IEnumerable<EmailTemplate> FetchEmailTemplates()
    {
        return new List<EmailTemplate>
        {
            new EmailTemplate { Id = "2bdd0916-d4b3-4fad-baa2-7f12890f3c08", Name = "Template 1" },
            new EmailTemplate { Id = "7c3275d4-7a60-4d97-87ed-f91e8c870935", Name = "Template 2" },
            new EmailTemplate { Id = "d825d25f-2f4a-48f5-9a7d-f34e5747ef61", Name = "Template 3" }
        };
    }
}
