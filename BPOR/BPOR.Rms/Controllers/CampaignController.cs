using System.Text;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Services;
using BPOR.Domain.Enums;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NIHR.GovUk.AspNetCore.Mvc;
using NIHR.Infrastructure.Interfaces;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Exceptions;
using Notify.Models.Responses;
using BPOR.Rms.Exceptions;

namespace BPOR.Rms.Controllers;

public class CampaignController(
    ParticipantDbContext context,
    INotificationService notificationService,
    IDistributedCache cache,
    ILogger<CampaignController> logger,
    IRmsTaskQueue taskQueue,
    IEncryptionService encryptionService,
    LinkGenerator linkGenerator,
    ITransactionalEmailService transactionalEmailService,
    IOptions<RmsSettings> rmsOptions
)
    : Controller
{
    private const string _templateCacheKey = "Templates";
    private string templateId;

    public async Task<IActionResult> Setup(SetupCampaignViewModel model)
    {
        await PopulateReferenceDataAsync(model, true);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Send(SetupCampaignViewModel model, CancellationToken cancellationToken)
    {
        await PopulateReferenceDataAsync(model, cancellationToken: cancellationToken);

        if (!ModelState.IsValid)
        {
            if (ModelState[nameof(model.TotalVolunteers)]?.Errors.Any(e => e.ErrorMessage.Contains("is not valid")) ?? false)
            {
                ModelState[nameof(model.TotalVolunteers)].Errors.Clear();
                ModelState.AddModelError(nameof(model.TotalVolunteers), "Number of volunteers to be contacted must be a whole number, like 15");
            }
        }

        if (model.TotalVolunteers is null)
        {
            ModelState.AddModelError(nameof(model.TotalVolunteers), "Enter the number of volunteers to be contacted");
        }

        else if (model.TotalVolunteers > model.MaxNumbers)
        {
            ModelState.AddModelError(nameof(model.TotalVolunteers),
                $"Number of volunteers to be contacted must be between 1 and {model.MaxNumbers:N0}");
        }

        if (string.IsNullOrEmpty(model.SelectedTemplateId))
        {
            ModelState.AddModelError(nameof(model.SelectedTemplateId), "Select a template");
        }

        if (ModelState.IsValid)
        {
            var selectedTemplate =
                model.Templates.First(t => t.id == model.SelectedTemplateId);

            if (!Enum.TryParse<ContactMethodId>(selectedTemplate.type, true, out var contactMethod))
            {
                throw new InvalidContactMethodException(selectedTemplate.type);
            }

            var campaign = new Campaign
            {
                FilterCriteriaId = model.FilterCriteriaId,
                TargetGroupSize = model.TotalVolunteers,
                TemplateId = new Guid(model.SelectedTemplateId!),
                Name = selectedTemplate.name,
                TypeId = contactMethod
            };

            await AddCampaignToContextAsync(campaign, cancellationToken);

            var callback =
                linkGenerator.GetUriByName(HttpContext, nameof(NotifyCallbackController.RegisterInterest)) ??
                throw new InvalidOperationException("Callback URL not found");

            await taskQueue.QueueBackgroundWorkItemAsync(campaign.Id, callback, cancellationToken);

            var studyInfo = await context.Studies
                                    .Where(x => x.Id == model.StudyId)
                                    .Select(x => new { x.StudyName, x.EmailAddress })
                                    .FirstOrDefaultAsync(cancellationToken);

            if (studyInfo is not null)
            {
                IEnumerable<string> notificationRecipients = [rmsOptions.Value.CampaignNotificationEmailAddress, studyInfo.EmailAddress];

                foreach (var recipient in notificationRecipients)
                {
                    if (string.IsNullOrWhiteSpace(recipient))
                    {
                        logger.LogWarning("Empty notification email address for study ({studyId}) '{studyName}', campaign ({campaignId}).", model.StudyId, model.StudyName, campaign.Id);

                        continue;
                    }

                    var sendParams = new Dictionary<string, object>
                    {
                        { "numberOfVolunteers", model.TotalVolunteers }
                    };

                    switch (campaign.TypeId)
                    {
                        case ContactMethodId.Email:
                            templateId = "email-rms-campaign-sent";
                            sendParams.Add("studyName", studyInfo.StudyName);
                            break;

                        case ContactMethodId.Letter:
                            templateId = "letter-rms-campaign-sent";
                            sendParams.Add("letterTemplateFilename", selectedTemplate.name);
                            break;
                    }

                    await transactionalEmailService.SendAsync(recipient, templateId, sendParams, cancellationToken);
                }
            }

            return View("Success",
                new EmailSuccessViewModel { StudyId = model.StudyId, StudyName = model.StudyName, ContactMethod = model.ContactMethod });
        }

        return View(nameof(Setup), model);
    }


    private async Task PopulateReferenceDataAsync(SetupCampaignViewModel model, bool forceRefresh = false,
    CancellationToken cancellationToken = default)
    {

        TemplateList templateList = await FetchTemplates(forceRefresh, cancellationToken);
        model.Templates = templateList.templates.ToList();

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
            var selectedTemplateName = model.Templates.First(t => t.id == model.SelectedTemplateId).name;
            var personalisationData = emailAddresses.ToDictionary(
                email => email,
                email => new Dictionary<string, string>
                {
                    { "email", email },
                    { "campaignParticipantId", "PreviewEmailReference" },
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
                    await notificationService.SendPreviewEmailAsync(new SendNotificationRequest
                    {
                        EmailAddress = email,
                        TemplateId = model.SelectedTemplateId,
                        Personalisation = personalisationData[email],
                        Reference = "PreviewEmailReference"
                    }, cancellationToken);
                }
                catch (NotifyClientException e)
                {
                    logger.LogError(e, "Error sending preview email");
                    ModelState.AddModelError(nameof(model.PreviewEmails),
                        "Gov Notify does not accept the email address(es) provided.");
                    return View(nameof(Setup), model);
                }
            }

            this.AddSuccessNotification(
                $"Preview email using template {selectedTemplateName} has been sent to {model.PreviewEmails}");
        }

        return View(nameof(Setup), model);
    }

    private bool IsValidEmail(string email) => System.Net.Mail.MailAddress.TryCreate(email, out var address) &&
                                               email.Equals(address.Address,
                                                   StringComparison.InvariantCultureIgnoreCase);

    private async Task<TemplateList> FetchTemplates(bool forceRefresh = false,
        CancellationToken cancellationToken = default)
    {
        var cachedData = await cache.GetAsync(_templateCacheKey, cancellationToken);
        if (cachedData != null && !forceRefresh)
        {
            var jsonData = Encoding.UTF8.GetString(cachedData);
            return JsonConvert.DeserializeObject<TemplateList>(jsonData);
        }

        var templates = await notificationService.GetTemplatesAsync(cancellationToken);
        await CacheTemplates(templates, cancellationToken);
        return templates;
    }

    private async Task CacheTemplates(TemplateList templates, CancellationToken cancellationToken = default)
    {
        var jsonData = JsonConvert.SerializeObject(templates);
        var data = Encoding.UTF8.GetBytes(jsonData);

        await cache.SetAsync(_templateCacheKey, data, cancellationToken);
    }

    private async Task AddCampaignToContextAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        await context.Campaign.AddAsync(campaign, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}