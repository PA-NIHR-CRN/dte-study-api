using System.Text;
using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using NIHR.Infrastructure.Models;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace BPOR.Rms.Controllers;

public class EmailController(IEmailCampaignService emailCampaignService, ParticipantDbContext context,
    INotificationService notificationService,
    IDistributedCache cache)
    : Controller
{
    
    private const string EmailCacheKey = "EmailTemplates";
    public async Task<IActionResult> SetupCampaign(SetupCampaignViewModel model, string? activity = null, CancellationToken cancellationToken = default)
    {
        if (activity == "SendEmail")
        {
            return await SendEmail(model, cancellationToken);
        }
        else if (activity == "SendPreviewEmail")
        {
            // TODO: Make this an independent POST endpoint
            // to keep the email address out of the URL.
            return await SendPreviewEmail(model, cancellationToken);
        }

        ModelState.Clear();
        var templates = await FetchEmailTemplates(cancellationToken);
        model.EmailTemplates = templates;

        await CacheEmailTemplates(templates, cancellationToken);
        return View(model);
    }

    public async Task<IActionResult> Index(SetupCampaignViewModel model)
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

        model.EmailTemplates = await FetchEmailTemplates();

        return View("SetupCampaign", model);
    }

    protected async Task<IActionResult> SendEmail(SetupCampaignViewModel model, CancellationToken cancellationToken)
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

        var templates = await FetchEmailTemplates(cancellationToken);
        var selectedTemplateId = templates.templates.FirstOrDefault(t => t.id == model.SelectedTemplateId)?.name;

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

    public async Task<IActionResult> SendPreviewEmail(SetupCampaignViewModel model, CancellationToken cancellationToken)
    {
        var emails = model.PreviewEmails.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (model.PreviewEmails != null)
        {
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
        var emailTemplates = await FetchEmailTemplates(cancellationToken);

        if (model.SelectedTemplateId != null)
        {
            var selectedTemplateName =
                emailTemplates.templates.FirstOrDefault(t => t.id == model.SelectedTemplateId)?.name;
            if (selectedTemplateName != null)
            {
                model.SelectedTemplateName = selectedTemplateName;
            }
        }


        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", model);
        }

        var personalisationData = emails.ToDictionary(
            email => email,
            email => new Dictionary<string, dynamic>
            {
                {"firstName", "John"},
                {"lastName", "Doe"},
                {"uniqueLink", $"https://example.com/{email}"},
                {"email", email},
                {"reference", "PreviewEmailReference"}
            });
        
        await notificationService.SendBatchEmailAsync(new SendBatchEmailRequest
        {
            EmailAddresses = emails,
            EmailTemplateId = new Guid(model.SelectedTemplateId),
            PersonalisationData = personalisationData
        }, cancellationToken);
        
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

    private async Task<TemplateList> FetchEmailTemplates(CancellationToken cancellationToken = default)
    {
        var cachedData = await cache.GetAsync(EmailCacheKey, cancellationToken);
        if (cachedData != null)
        {
            var jsonData = Encoding.UTF8.GetString(cachedData);
            return JsonConvert.DeserializeObject<TemplateList>(jsonData);
        }

        var templates = await notificationService.GetTemplatesAsync(cancellationToken);
        await CacheEmailTemplates(templates, cancellationToken);
        return templates;
    }
    
    private async Task CacheEmailTemplates(TemplateList templates, CancellationToken cancellationToken = default)
    {
        var jsonData = JsonConvert.SerializeObject(templates);
        var data = Encoding.UTF8.GetBytes(jsonData);

        await cache.SetAsync(EmailCacheKey, data, cancellationToken);
    }
}
