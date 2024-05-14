using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BPOR.Rms.Controllers;

public class EmailController(IEmailCampaignService emailCampaignService, ParticipantDbContext context) : Controller
{
    public async Task<IActionResult> SetupCampaign(SetupCampaignViewModel model, string? activity = null, CancellationToken cancellationToken = default)
    {
        if (activity == "SendEmail")
        {
            return await SendEmail(model, cancellationToken);
        }
        else if (activity == "SendPreviewEmail")
        {
            return SendPreviewEmail(model);
        }

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

    protected async Task<IActionResult> SendEmail(SetupCampaignViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove("PreviewEmails");
        model.EmailTemplates = FetchEmailTemplates();

        if (model.TotalVolunteers > model.MaxNumbers)
        {
            ModelState.AddModelError("TotalVolunteers", "The number of volunteers to be contacted must be the same as, or less than, the 'total number of volunteer accounts matching the filter options'.");
        }

        if (string.IsNullOrEmpty(model.SelectedTemplateId))
        {
            ModelState.AddModelError("SelectedTemplate", "Select an email template.");
        }

        if (!ModelState.IsValid)
        {
            return View("SetupCampaign", model);
        }

        var selectedTemplateId = FetchEmailTemplates().FirstOrDefault(t => t.Id == model.SelectedTemplateId)?.Name;

        await emailCampaignService.SendCampaignAsync(new EmailCampaign
        {
            FilterCriteriaId = model.FilterCriteriaId,
            TargetGroupSize = model.TotalVolunteers.Value,
            EmailTemplateId = new Guid(model.SelectedTemplateId),
            Name = selectedTemplateId
        }, cancellationToken);

        return RedirectToAction("EmailSuccess", model);
    }


    public Task<IActionResult> EmailSuccess(EmailSuccessViewModel model)
    {
        if (model.StudyId > 0)
        {
            model.StudyName = context.Studies.Where(s => s.Id == model.StudyId).Select(s => s.StudyName).FirstOrDefault();
        }

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
