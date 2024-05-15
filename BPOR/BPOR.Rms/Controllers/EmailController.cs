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
        model.EmailTemplates = FetchEmailTemplates();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(SetupCampaignViewModel model, CancellationToken cancellationToken)
    {
        model.EmailTemplates = FetchEmailTemplates();

        if (model.TotalVolunteers is null)
        {
            ModelState.AddModelError(nameof(model.TotalVolunteers), "Enter the number of volunteers to be contacted.");
        }
        else if (model.TotalVolunteers > model.MaxNumbers)
        {
            ModelState.AddModelError("TotalVolunteers", "The number of volunteers to be contacted must be the same as, or less than, the 'total number of volunteer accounts matching the filter options'.");
        }

        if (string.IsNullOrEmpty(model.SelectedTemplateId))
        {
            ModelState.AddModelError(nameof(model.SelectedTemplateId), "Select an email template.");
        }

        if (!ModelState.IsValid)
        {
            return View(nameof(SetupCampaign), model);
        }

        var selectedTemplateId = FetchEmailTemplates().FirstOrDefault(t => t.Value == model.SelectedTemplateId)?.Text;

        await emailCampaignService.SendCampaignAsync(new EmailCampaign
        {
            FilterCriteriaId = model.FilterCriteriaId,
            TargetGroupSize = model.TotalVolunteers.Value,
            EmailTemplateId = new Guid(model.SelectedTemplateId),
            Name = selectedTemplateId
        }, cancellationToken);

        // TODO make this a view?
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

    [HttpPost]
    public IActionResult SendPreviewEmail(SetupCampaignViewModel model)
    {
        model.EmailTemplates = FetchEmailTemplates();

        if (string.IsNullOrEmpty(model.SelectedTemplateId))
        {
            ModelState.AddModelError("SelectedTemplate", "Select an email template.");
        }

        if (string.IsNullOrWhiteSpace(model.PreviewEmails))
        {
            ModelState.AddModelError(nameof(model.PreviewEmails), "Enter at least one address");
        }
        else
        {
            var emails = model.PreviewEmails.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var email in emails)
            {
                if (!IsValidEmail(email))
                {
                    ModelState.AddModelError(nameof(model.PreviewEmails), "Please enter a valid email address.");
                    break;
                }
            }
        }

        var selectedTemplateName = string.Empty;
        if (model.SelectedTemplateId != null)
        {
            selectedTemplateName = model.EmailTemplates.FirstOrDefault(t => t.Value == model.SelectedTemplateId)?.Text;
        }
        else
        {
            ModelState.AddModelError(nameof(model.SelectedTemplateId), "Select an email template.");
        }


        if (ModelState.IsValid)
        {
            TempData["SelectedTemplateId"] = model.SelectedTemplateId;
            TempData["SelectedTemplateName"] = selectedTemplateName;
            TempData.AddSuccessNotification($"Preview email using template {selectedTemplateName} has been sent to {model.PreviewEmails}");
        }

        return View(nameof(SetupCampaign), model);
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

    private IEnumerable<SelectListItem> FetchEmailTemplates()
    {
        return
        [
            new SelectListItem("Template 1", "2bdd0916-d4b3-4fad-baa2-7f12890f3c08"),
            new SelectListItem("Template 2", "7c3275d4-7a60-4d97-87ed-f91e8c870935"),
            new SelectListItem("Template 3", "d825d25f-2f4a-48f5-9a7d-f34e5747ef61"),
        ];
    }
}
