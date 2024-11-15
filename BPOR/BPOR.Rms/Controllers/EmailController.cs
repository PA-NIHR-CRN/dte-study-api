using System.Text;
using BPOR.Domain.Entities;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Services;
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

namespace BPOR.Rms.Controllers;

public class EmailController(
    ParticipantDbContext context,
    INotificationService notificationService,
    IDistributedCache cache,
    ILogger<EmailController> logger,
    IRmsTaskQueue taskQueue,
    IEncryptionService encryptionService,
    LinkGenerator linkGenerator,
    ITransactionalEmailService transactionalEmailService,
    IOptions<RmsSettings> rmsOptions
)
    : Controller
{
    private const string _templateCacheKey = "Templates";

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

            var campaign = new Campaign
            {
                FilterCriteriaId = model.FilterCriteriaId,
                TargetGroupSize = model.TotalVolunteers,
                TemplateId = new Guid(model.SelectedTemplateId!),
                Name = selectedTemplateName
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
                        logger.LogWarning("Empty notification email address for study ({studyId}) '{studyName}', email campaign ({emailCampaignId}).", model.StudyId, model.StudyName, campaign.Id);

                        continue;
                    }

                    await transactionalEmailService.SendAsync(recipient, "email-rms-campaign-sent", new { numberOfVolunteers = model.TotalVolunteers, studyName = studyInfo.StudyName }, cancellationToken);
                    // TODO: send letter campaign notification instead
                }
            }

            return View("EmailSuccess",
                new EmailSuccessViewModel { StudyId = model.StudyId, StudyName = model.StudyName });
        }

        return View(nameof(SetupCampaign), model);
    }


    private async Task PopulateReferenceDataAsync(SetupCampaignViewModel model, bool forceRefresh = false,
    CancellationToken cancellationToken = default)
    {
        model.EmailTemplates = await FetchTemplates(ContactMethod.Email, forceRefresh, cancellationToken);
        //model.LetterTemplates = await FetchTemplates(forceRefresh, ContactMethod.Letter, cancellationToken);


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

    private async Task<TemplateList> FetchTemplates(ContactMethod contactMethod, bool forceRefresh = false,
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
        await context.Campaigns.AddAsync(campaign, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
