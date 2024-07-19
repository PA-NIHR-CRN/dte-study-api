using System.Text;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.Infrastructure.Interfaces;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Exceptions;
using Notify.Models.Responses;

namespace BPOR.Rms.Controllers;

public class EmailController(
    ParticipantDbContext context,
    INotificationService notificationService,
    IDistributedCache cache,
    ILogger<EmailController> logger,
    IRmsTaskQueue taskQueue,
    IEncryptionService encryptionService,
    IHostEnvironment hostEnvironment,
    LinkGenerator linkGenerator
)
    : Controller
{
    private const string _emailCacheKey = "EmailTemplates";

    public async Task<IActionResult> SetupCampaign(SetupCampaignViewModel model)
    {
        await PopulateReferenceDataAsync(model, true);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(SetupCampaignViewModel model, CancellationToken cancellationToken)
    {
        await PopulateReferenceDataAsync(model, cancellationToken: cancellationToken);

        if (model.TotalVolunteers is null)
        {
            ModelState.AddModelError(nameof(model.TotalVolunteers), "Enter the number of volunteers to be contacted.");
        }
        else if (model.TotalVolunteers > model.MaxNumbers)
        {
            ModelState.AddModelError(nameof(model.TotalVolunteers),
                "The number of volunteers to be contacted must be the same as, or less than, the 'total number of volunteer accounts matching the filter options'.");
        }

        if (string.IsNullOrEmpty(model.SelectedTemplateId))
        {
            ModelState.AddModelError(nameof(model.SelectedTemplateId), "Please select an email template.");
        }

        if (ModelState.IsValid)
        {
            var selectedTemplateName =
                model.EmailTemplates.templates.First(t => t.id == model.SelectedTemplateId).name;

            var emailCampaign = new EmailCampaign
            {
                FilterCriteriaId = model.FilterCriteriaId,
                TargetGroupSize = model.TotalVolunteers,
                EmailTemplateId = new Guid(model.SelectedTemplateId!),
                Name = selectedTemplateName
            };

            await AddCampaignToContextAsync(emailCampaign, cancellationToken);

            var callback =
                linkGenerator.GetUriByName(HttpContext, nameof(NotifyCallbackController.RegisterInterest)) ??
                throw new InvalidOperationException("Callback URL not found");

            await taskQueue.QueueBackgroundWorkItemAsync(emailCampaign.Id, callback, cancellationToken);

            return View("EmailSuccess",
                new EmailSuccessViewModel { StudyId = model.StudyId, StudyName = model.StudyName });
        }

        return View(nameof(SetupCampaign), model);
    }


    private async Task PopulateReferenceDataAsync(SetupCampaignViewModel model, bool forceRefresh = false,
    CancellationToken cancellationToken = default)
{
    model.EmailTemplates = await FetchEmailTemplates(forceRefresh, cancellationToken);
    
    if (model.StudyId is not null)
    {
        var studyData = await context.Studies
            .Where(s => s.Id == model.StudyId)
            .Select(s => new 
            {
                s.StudyName,
                s.EmailAddress
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (studyData != null)
        {
            model.StudyName = studyData.StudyName;
            model.EmailAddress = studyData.EmailAddress;
        }
    }
}

    [HttpPost]
    public async Task<IActionResult> SendPreviewEmail(SetupCampaignViewModel model, CancellationToken cancellationToken)
    {
        await PopulateReferenceDataAsync(model, cancellationToken: cancellationToken);

        var emailAddresses = model.GetPreviewEmailAddresses();

        if (!emailAddresses.Any())
        {
            ModelState.AddModelError(nameof(model.PreviewEmails), "Enter at least one email address");
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
                email => new Dictionary<string, string>
                {
                    { "email", email },
                    { "emailCampaignParticipantId", "PreviewEmailReference" },
                    { "firstName", "John" },
                    { "lastName", "Doe" },
                    {
                        "uniqueLink",
                        linkGenerator.GetUriByName(HttpContext, nameof(NotifyCallbackController.RegisterInterest),
                            new { reference = encryptionService.Encrypt("0123456789101112") }) ?? string.Empty
                    },
                    { "uniqueReference", "0123456789101112" }
                });

            foreach (var email in emailAddresses)
            {
                try
                {
                    await notificationService.SendPreviewEmailAsync(new SendEmailRequest
                    {
                        EmailAddress = email,
                        EmailTemplateId = model.SelectedTemplateId,
                        Personalisation = personalisationData[email],
                        Reference = "PreviewEmailReference"
                    }, cancellationToken);
                }
                catch (NotifyClientException e)
                {
                    logger.LogError(e, "Error sending preview email");
                    ModelState.AddModelError(nameof(model.PreviewEmails),
                        "Gov Notify does not accept the email address(es) provided.");
                    return View(nameof(SetupCampaign), model);
                }
            }


            this.AddSuccessNotification(
                $"Preview email using template {selectedTemplateName} has been sent to {model.PreviewEmails}");
        }

        return View(nameof(SetupCampaign), model);
    }

    private bool IsValidEmail(string email) => System.Net.Mail.MailAddress.TryCreate(email, out var address) &&
                                               email.Equals(address.Address,
                                                   StringComparison.InvariantCultureIgnoreCase);

    private async Task<TemplateList> FetchEmailTemplates(bool forceRefresh = false,
        CancellationToken cancellationToken = default)
    {
        var cachedData = await cache.GetAsync(_emailCacheKey, cancellationToken);
        if (cachedData != null && !forceRefresh)
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

        await cache.SetAsync(_emailCacheKey, data, cancellationToken);
    }

    private async Task AddCampaignToContextAsync(EmailCampaign campaign, CancellationToken cancellationToken)
    {
        await context.EmailCampaigns.AddAsync(campaign, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
