using System.Text;
using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
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
    
    public IActionResult SetupCampaign(SetupCampaignViewModel model)
    {
        PopulateReferenceData(model);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(SetupCampaignViewModel model, CancellationToken cancellationToken)
    {
        PopulateReferenceData(model);

        if (model.TotalVolunteers is null)
        {
            ModelState.AddModelError(nameof(model.TotalVolunteers), "Enter the number of volunteers to be contacted.");
        }
        else if (model.TotalVolunteers > model.MaxNumbers)
        {
            ModelState.AddModelError(nameof(model.TotalVolunteers), "The number of volunteers to be contacted must be the same as, or less than, the 'total number of volunteer accounts matching the filter options'.");
        }

        if (string.IsNullOrEmpty(model.SelectedTemplateId))
        {
            ModelState.AddModelError(nameof(model.SelectedTemplateId), "Please select an email template.");
        }

        if (ModelState.IsValid)
        {
            var selectedTemplateName = model.EmailTemplates.templates.First(t => t.id == model.SelectedTemplateId).name;

            await emailCampaignService.SendCampaignAsync(new EmailCampaign
            {
                FilterCriteriaId = model.FilterCriteriaId,
                TargetGroupSize = model.TotalVolunteers,
                EmailTemplateId = new Guid(model.SelectedTemplateId!),
                Name = selectedTemplateName
            }, cancellationToken);

            return View("EmailSuccess", new EmailSuccessViewModel { StudyId = model.StudyId, StudyName = model.StudyName});
        }
        
        return View(nameof(SetupCampaign), model);
    }

    private async void PopulateReferenceData(SetupCampaignViewModel model)
    {
        model.EmailTemplates = await FetchEmailTemplates();
        if (model.StudyId is not null)
        {
            model.StudyName = context.Studies.Where(s => s.Id == model.StudyId).Select(s => s.StudyName).First();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> SendPreviewEmail(SetupCampaignViewModel model, CancellationToken cancellationToken)
    {
        PopulateReferenceData(model);

        var emailAddresses = model.GetPreviewEmailAddresses();

        if (!emailAddresses.Any())
        {
            ModelState.AddModelError(nameof(model.PreviewEmails), "Enter at least one address");
        }
        else if (!emailAddresses.All(IsValidEmail))
        {
            ModelState.AddModelError(nameof(model.PreviewEmails), "Please enter a valid email address.");
        }

        if (string.IsNullOrWhiteSpace(model.SelectedTemplateId))
        {
            ModelState.AddModelError(nameof(model.SelectedTemplateId), "Select an email template.");
        }

        if (ModelState.IsValid)
        {
            var selectedTemplateName = model.EmailTemplates.templates.First(t => t.id == model.SelectedTemplateId).name;
            var personalisationData = emailAddresses.ToDictionary(
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
                EmailAddresses = emailAddresses,
                EmailTemplateId = new Guid(model.SelectedTemplateId),
                PersonalisationData = personalisationData
            }, cancellationToken);
            TempData.AddSuccessNotification($"Preview email using template {selectedTemplateName} has been sent to {model.PreviewEmails}");
        }
        
        return View(nameof(SetupCampaign), model);
    }

    private bool IsValidEmail(string email) => System.Net.Mail.MailAddress.TryCreate(email, out var address) && email.Equals(address.Address, StringComparison.InvariantCultureIgnoreCase);
    
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

