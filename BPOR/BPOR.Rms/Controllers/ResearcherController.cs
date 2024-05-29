using BPOR.Domain.Entities;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Models.Researcher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BPOR.Rms.Controllers;

public class ResearcherController(ParticipantDbContext context) : Controller
{
    public IActionResult Create()
    {
        ViewData["ShowBackLink"] = true;
        return View(new ResearcherFormViewModel());
    }
    

    [HttpPost]
    public IActionResult Create(ResearcherFormViewModel model)
    {
        ViewData["ShowBackLink"] = true;

        if (model.Password?.Length < 12)
        {
            ModelState.AddModelError("Password", "Enter a password that is at least 12 characters long and does not include any symbols");
        }

        if (ModelState.IsValid)
        {
            return View("AddResearcherSuccess");
        }

        return View(model);
    }

    public IActionResult TermsAndConditions()
    {
        return View(new ResearcherTermsAndConditionsViewModel());
    }

    [HttpPost]
    public IActionResult TermsAndConditions(ResearcherTermsAndConditionsViewModel model)
    {
        if (!model.AgreedToTermsAndConditions)
        {
            ModelState.AddModelError("AgreedToTermsAndConditions", "Confirm that the terms and conditions have been read and agreed before applying");
        }

        if (ModelState.IsValid)
        {
            return RedirectToAction("SubmitStudy", model);
        }

        return View(model);
    }

    public IActionResult SubmitStudy(ResearcherStudyFormViewModel model)
    {
        ViewData["ShowBackLink"] = true;
        ViewData["ShowProgressBar"] = true;
        ViewData["ProgressPercentage"] = 0;

        return View(model);
    }

    [HttpPost]
    public IActionResult SubmitStudy(ResearcherStudyFormViewModel model, string action)
    {
        ViewData["ShowBackLink"] = true;
        ViewData["ShowProgressBar"] = true;
        ViewData["ProgressPercentage"] = (model.Step - 1) * 50;

        model.PortfolioSubmissionStatusOptions = context.Submitted.ToList();
        model.OutcomeOfSubmissionOptions = context.SubmissionOutcome.ToList();

        if (action == "Next" && model.Step == 1)
        {
            if (String.IsNullOrEmpty(model.ShortName))
            {
                ModelState.AddModelError("ShortName", "Enter the study short name");
            }

            if (String.IsNullOrEmpty(model.ChiefInvestigator))
            {
                ModelState.AddModelError("ChiefInvestigator", "Enter the name of the Chief Investigator for the study");
            }

            if (String.IsNullOrEmpty(model.StudySponsors))
            {
                ModelState.AddModelError("StudySponsors", "Enter the name(s) of the study sponsor(s), funder(s) and CRO (if applicable)");
            }

            if (ModelState.IsValid)
            {
                model.Step = 2;
                ViewData["ProgressPercentage"] = (model.Step - 1) * 50;
                return View(model);
            }
        }
        else if (action == "Next" && model.Step == 2)
        {
            if (model.PortfolioSubmissionStatus == null)
            {
                ModelState.AddModelError("PortfolioSubmissionStatus", "Select whether the study has been submitted for inclusion on the NIHR CRN portfolio");
            }

            if (ModelState.IsValid)
            {
                if (model.PortfolioSubmissionStatus == 1)
                {
                    model.Step = 3;
                }
                else
                {
                    model.Step = 4;
                }
                ViewData["ProgressPercentage"] = (model.Step - 1) * 50;
                return View(model);
            }
        }
        else if (action == "Next" && model.Step == 3)
        {
            if (model.OutcomeOfSubmission == null)
            {
                ModelState.AddModelError("OutcomeOfSubmission", "Select the outcome of the submission for inclusion on the NIHR CRN portfolio");
            }

            if (model.CPMSId == null)
            {
                ModelState.AddModelError("CPMSId", "Enter the CPMS ID for the study");
            }

            if (ModelState.IsValid)
            {
                model.Step = 4;
                ViewData["ProgressPercentage"] = (model.Step - 1) * 50;
                return View(model);
            }
        }
        else if (action == "Next" && model.Step == 4)
        {
            if (model.HasFunding == null)
            {
                ModelState.AddModelError("HasFunding", "Select whether the study has NIHR funding");
            }

            if (ModelState.IsValid)
            {
                if (model.HasFunding == true)
                {
                    model.Step = 5;
                }
                else
                {
                    model.Step = 6;
                }
                ViewData["ProgressPercentage"] = (model.Step - 1) * 50;
                return View(model);
            }
        }
        else if (action == "Next" && model.Step == 5)
        {
            if (String.IsNullOrEmpty(model.FundingCode))
            {
                ModelState.AddModelError("FundingCode", "Enter the NIHR funding stream or grant code");
            }

            if (ModelState.IsValid)
            {
                model.Step = 6;
                ViewData["ProgressPercentage"] = (model.Step - 1) * 50;
                return View(model);
            }
        }

        return View(model);
    }

}
