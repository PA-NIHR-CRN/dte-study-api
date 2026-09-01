using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.Ms4.Models;
using BPOR.Rms.Ms4.Repositories;
using BPOR.Rms.Ms4.Validators.Details;
using BPOR.Rms.Ms4.Validators.Overview;
using BPOR.Rms.Ms4.Validators.ParticipantDetails;
using BPOR.Rms.Ms4.Validators.Sponsorship;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BPOR.Rms.Ms4.Controllers;

[Route("study-request")]
public class StudyRequestController(IStudyDraftRepository studyDraftRepository) : Controller
{
    [HttpGet("start")]
    public IActionResult Start()
    {
        return View();
    }

    [HttpPost("start")]
    public async Task<IActionResult> Start(
        StudyRequestStartViewModel model,
        [FromServices] IValidator<StudyRequestStartViewModel> validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View(model);
        }

        var study = new Study();

        var studyId = await studyDraftRepository.CreateDraftStudyAsync(study, cancellationToken);

        return RedirectToAction(nameof(EthicsApproval), new { studyId });
    }

    [HttpGet("{studyId:int}/ethics-approval")]
    public async Task<IActionResult> EthicsApproval(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = new StudyRequestViewModel
        {
            HasEthicsApproval = study.HasEthicsApproval
        };

        return View("Overview/EthicsApproval", model);
    }

    [HttpPost("{studyId:int}/ethics-approval")]
    public async Task<IActionResult> EthicsApproval(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] EthicsApprovalValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/EthicsApproval", model);
        }

        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        study.HasEthicsApproval = model.HasEthicsApproval;

        await studyDraftRepository.SaveStudyAsync(study, cancellationToken);

        return RedirectToAction(nameof(InclusionInRdnPortfolio), new { studyId });
    }

    [HttpGet("{studyId:int}/inclusion-in-rdn-portfolio")]
    public async Task<IActionResult> InclusionInRdnPortfolio(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(
            studyId,
            cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = new StudyRequestViewModel
        {
            InclusionInRdnPortfolioStatus = study.Submitted?.Id,
            CpmsId = study.CpmsId
        };

        return View("Overview/InclusionInRdnPortfolio", model);
    }

    [HttpPost("{studyId:int}/inclusion-in-rdn-portfolio")]
    public async Task<IActionResult> InclusionInRdnPortfolio(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] InclusionInRdnPortfolioValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/InclusionInRdnPortfolio", model);
        }

        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }
        
        study.CpmsId = model.CpmsId;
        study.SubmittedId = model.InclusionInRdnPortfolioStatus;

        await studyDraftRepository.SaveStudyAsync(study, cancellationToken);

        var nextAction =
            model.InclusionInRdnPortfolioStatus ==
            SubmittedType.Yes
                ? nameof(FinishRecruiting)
                : nameof(NihrFunding);

        return RedirectToAction(nextAction, new { studyId });
    }

    [HttpGet("{studyId:int}/nihr-funding")]
    public async Task<IActionResult> NihrFunding(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = new StudyRequestViewModel
        {
            NihrFundingStatus = study.NihrFundingStatus?.Id
        };

        return View("Overview/NihrFunding", model);
    }

    [HttpPost("{studyId:int}/nihr-funding")]
    public async Task<IActionResult> NihrFunding(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] NihrFundingValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/NihrFunding", model);
        }

        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        study.HasNihrFunding = model.NihrFundingStatus;

        await studyDraftRepository.SaveStudyAsync(study, cancellationToken);

        var nextAction =
            model.NihrFundingStatus ==
            NihrFundingStatusType.No
                ? nameof(MoreInformationRequired)
                : nameof(FinishRecruiting);

        return RedirectToAction(nextAction, new { studyId });
    }

    [HttpGet("{studyId:int}/finish-recruiting")]
    public async Task<IActionResult> FinishRecruiting(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = new StudyRequestViewModel
        {
            FinishRecruitingDay = study.RecruitmentEndDate?.Day,
            FinishRecruitingMonth = study.RecruitmentEndDate?.Month,
            FinishRecruitingYear = study.RecruitmentEndDate?.Year,
        };

        return View("Overview/FinishRecruiting", model);
    }

    [HttpPost("{studyId:int}/finish-recruiting")]
    public async Task<IActionResult> FinishRecruiting(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] FinishRecruitingValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Overview/FinishRecruiting", model);
        }

        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }
        
        study.RecruitmentEndDate = new DateTime(
            model.FinishRecruitingYear!.Value,
            model.FinishRecruitingMonth!.Value,
            model.FinishRecruitingDay!.Value);

        await studyDraftRepository.SaveStudyAsync(study, cancellationToken);

        return RedirectToAction(nameof(StudyDescription), new { studyId });
    }

    [HttpGet("{studyId:int}/more-information-required")]
    public async Task<IActionResult> MoreInformationRequired(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }
        
        await studyDraftRepository.RemoveStudyAsync(study.Id, cancellationToken);

        return View("MoreInformationRequired");
    }

    [HttpGet("{studyId:int}/study-description")]
    public async Task<IActionResult> StudyDescription(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = new StudyRequestViewModel
        {
            StudyTitle = study.StudyName,
            StudyDescription = study.Description
        };

        return View("Details/StudyDescription", model);
    }

    [HttpPost("{studyId:int}/study-description")]
    public async Task<IActionResult> StudyDescription(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] StudyDescriptionValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/StudyDescription", model);
        }

        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        study.StudyName = model.StudyTitle;
        study.Description = model.StudyDescription;

        await studyDraftRepository.SaveStudyAsync(study, cancellationToken);
        
        return RedirectToAction(nameof(ResearchLocations), new { studyId });
    }

    [HttpGet("{studyId:int}/research-locations")]
    public async Task<IActionResult> ResearchLocations(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = new StudyRequestViewModel
        {
            HasMultipleResearchLocations = study.HasMultipleResearchLocations
        };

        return View("Details/ResearchLocation", model);
    }

    [HttpPost("{studyId:int}/research-locations")]
    public async Task<IActionResult> ResearchLocations(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] ResearchLocationValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ResearchLocation", model);
        }

        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        study.HasMultipleResearchLocations = model.HasMultipleResearchLocations;

        await studyDraftRepository.SaveStudyAsync(study, cancellationToken);

        return RedirectToAction(nameof(ResearchManager), new { studyId });
    }

    [HttpGet("{studyId:int}/research-manager")]
    public async Task<IActionResult> ResearchManager(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = new StudyRequestViewModel
        {
            SinglePersonResponsibleForRecruiting = study.SinglePersonResponsibleForRecruiting
        };

        return View("Details/ResearchManager", model);
    }

    [HttpPost("{studyId:int}/research-manager")]
    public async Task<IActionResult> ResearchManager(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] ResearchManagerValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ResearchManager", model);
        }

        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        study.SinglePersonResponsibleForRecruiting = model.SinglePersonResponsibleForRecruiting;

        await studyDraftRepository.SaveStudyAsync(study, cancellationToken);
        
        return RedirectToAction(nameof(ChiefInvestigator), new { studyId });
    }

    [HttpGet("{studyId:int}/chief-investigator")]
    public async Task<IActionResult> ChiefInvestigator(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = new StudyRequestViewModel
        {
            ChiefInvestigatorName = study.ChiefInvestigator,
            ChiefInvestigatorEmail = study.ChiefInvestigatorEmail
        };

        return View("Details/ChiefInvestigator", model);
    }

    [HttpPost("{studyId:int}/chief-investigator")]
    public async Task<IActionResult> ChiefInvestigator(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] ChiefInvestigatorValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ChiefInvestigator", model);
        }
        
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        study.ChiefInvestigatorEmail = model.ChiefInvestigatorEmail;
        study.ChiefInvestigator = model.ChiefInvestigatorName;

        await studyDraftRepository.SaveStudyAsync(study, cancellationToken);

        return RedirectToAction(nameof(ChiefInvestigatorContact), new { studyId });
    }

    [HttpGet("{studyId:int}/chief-investigator-contact")]
    public IActionResult ChiefInvestigatorContact(int studyId, CancellationToken cancellationToken)
    {
        return View("Details/ChiefInvestigatorContact");
    }

    [HttpPost("{studyId:int}/chief-investigator-contact")]
    public async Task<IActionResult> ChiefInvestigatorContact(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] ChiefInvestigatorContactValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/ChiefInvestigatorContact", model);
        }

        if (model.IsChiefInvestigatorMainContact != true)
        {
            return RedirectToAction(nameof(MainContact), new { studyId });
        }

        return RedirectToAction(nameof(SponsorOrganisation), new { studyId });
    }

    [HttpGet("{studyId:int}/main-contact")]
    public async Task<IActionResult> MainContact(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = new StudyRequestViewModel
        {
            MainContactName = study.FullName,
            MainContactEmail = study.EmailAddress,
            MainContactRole = study.MainContactRole
        };

        return View("Details/MainContact", model);
    }

    [HttpPost("{studyId:int}/main-contact")]
    public async Task<IActionResult> MainContact(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] MainContactValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Details/MainContact", model);
        }

        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        study.FullName = model.MainContactName;
        study.EmailAddress = model.MainContactEmail;
        study.MainContactRole = model.MainContactRole;

        await studyDraftRepository.SaveStudyAsync(study, cancellationToken);

        return RedirectToAction(nameof(SponsorOrganisation), new { studyId });
    }

    [HttpGet("{studyId:int}/sponsor-organisation")]
    public async Task<IActionResult> SponsorOrganisation(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = new StudyRequestViewModel
        {
            SponsorName = study.Sponsors
        };

        return View("Sponsorship/SponsorOrganisation", model);
    }

    [HttpPost("{studyId:int}/sponsor-organisation")]
    public async Task<IActionResult> SponsorOrganisation(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] SponsorOrganisationValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("Sponsorship/SponsorOrganisation", model);
        }

        var study = await GetStudyAsync(studyId, cancellationToken);
        
        if (study is null)
        {
            return NotFound();
        }

        study.Sponsors = model.SponsorName;

        await studyDraftRepository.SaveStudyAsync(study, cancellationToken);

        return RedirectToAction(nameof(ParticipantDetails), new { studyId });
    }

    [HttpGet("{studyId:int}/participant-details")]
    public async Task<IActionResult> ParticipantDetails(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = new StudyRequestViewModel
        {
            InclusionCriteria = study.InclusionCriteria
        };

        return View("ParticipantDetails/ParticipantDetails", model);
    }

    [HttpPost("{studyId:int}/participant-details")]
    public async Task<IActionResult> ParticipantDetails(
        int studyId,
        StudyRequestViewModel model,
        [FromServices] ParticipantDetailsValidator validator,
        CancellationToken cancellationToken)
    {
        if (!await ValidateAsync(validator, model, cancellationToken))
        {
            return View("ParticipantDetails/ParticipantDetails", model);
        }

        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        study.InclusionCriteria = model.InclusionCriteria;

        await studyDraftRepository.SaveStudyAsync(study, cancellationToken);

        return RedirectToAction(nameof(Summary), new { studyId });
    }

    [HttpGet("{studyId:int}/summary")]
    public async Task<IActionResult> Summary(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        var model = MapSummary(study);

        return View(model);
    }

    [HttpPost("{studyId:int}/summary")]
    public async Task<IActionResult> SubmitStudy(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        await studyDraftRepository.SubmitStudyAsync(studyId, cancellationToken);

        return RedirectToAction(nameof(ApplicationSubmitted), new { studyId });
    }

    [HttpGet("{studyId:int}/application-submitted")]
    public async Task<IActionResult> ApplicationSubmitted(int studyId, CancellationToken cancellationToken)
    {
        var study = await GetStudyAsync(studyId, cancellationToken);

        if (study is null)
        {
            return NotFound();
        }

        return View();
    }

    private Task<Study?> GetStudyAsync(int studyId, CancellationToken cancellationToken)
    {
        return studyDraftRepository.GetStudyAsync(studyId, cancellationToken);
    }

    private static StudyRequestViewModel MapSummary(Study study)
    {
        return new StudyRequestViewModel
        {
            StudyId = study.Id,
            HasEthicsApproval = study.HasEthicsApproval,
            InclusionInRdnPortfolioStatusDisplay = study.Submitted?.Code,
            CpmsId = study.CpmsId,
            NihrFundingStatusDisplay = study.NihrFundingStatus?.Code,
            RecruitmentEndDate = study.RecruitmentEndDate,
            StudyTitle = study.StudyName,
            StudyDescription = study.Description,
            HasMultipleResearchLocations = study.HasMultipleResearchLocations,
            SinglePersonResponsibleForRecruiting = study.SinglePersonResponsibleForRecruiting,
            ChiefInvestigatorName = study.ChiefInvestigator,
            ChiefInvestigatorEmail = study.ChiefInvestigatorEmail,
            MainContactName = study.FullName,
            MainContactEmail = study.EmailAddress,
            MainContactRole = study.MainContactRole,
            SponsorName = study.Sponsors,
            InclusionCriteria = study.InclusionCriteria
        };
    }

    private async Task<bool> ValidateAsync<TModel>(
        IValidator<TModel> validator,
        TModel model,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return validationResult.IsValid;
    }
}