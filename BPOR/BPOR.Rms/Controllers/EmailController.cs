using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BPOR.Rms.Controllers;

public class EmailController(IEmailCampaignService emailCampaignService, ParticipantDbContext context) : Controller
{
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
            ModelState.AddModelError(nameof(model.SelectedTemplateId), "Select an email template.");
        }

        if (ModelState.IsValid)
        {
            var selectedTemplateName = model.EmailTemplates.First(t => t.Value == model.SelectedTemplateId).Text;

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

    private void PopulateReferenceData(SetupCampaignViewModel model)
    {
        model.EmailTemplates = FetchEmailTemplates();
        if (model.StudyId is not null)
        {
            model.StudyName = context.Studies.Where(s => s.Id == model.StudyId).Select(s => s.StudyName).First();
        }
    }

    [HttpPost]
    public IActionResult SendPreviewEmail(SetupCampaignViewModel model)
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
            // TODO: Send preview email
            var selectedTemplateName = model.EmailTemplates.FirstOrDefault(t => t.Value == model.SelectedTemplateId)?.Text;
            TempData.AddSuccessNotification($"Preview email using template {selectedTemplateName} has been sent to {model.PreviewEmails}");
        }

        return View(nameof(SetupCampaign), model);
    }

    private bool IsValidEmail(string email) => System.Net.Mail.MailAddress.TryCreate(email, out var address) && email.Equals(address.Address, StringComparison.InvariantCultureIgnoreCase);

    private static IEnumerable<SelectListItem> FetchEmailTemplates() => [
            new SelectListItem("Template 1", "2bdd0916-d4b3-4fad-baa2-7f12890f3c08"),
            new SelectListItem("Template 2", "7c3275d4-7a60-4d97-87ed-f91e8c870935"),
            new SelectListItem("Template 3", "d825d25f-2f4a-48f5-9a7d-f34e5747ef61"),
        ];
}