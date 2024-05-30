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
        else if (action == "Next" && model.Step == 6)
        {
            if (String.IsNullOrEmpty(model.UKRecruitmentTarget))
            {
                ModelState.AddModelError("UKRecruitmentTarget", "Enter the UK recruitment target for the study");
            }

            if (String.IsNullOrEmpty(model.TargetPopulation))
            {
                ModelState.AddModelError("TargetPopulation", "Enter the target population for the study");
            }

            if (ModelState.IsValid)
            {
                model.Step = 7;
                ViewData["ProgressPercentage"] = (model.Step - 1) * 50;
                return View(model);
            }
        }
        else if (action == "Next" && model.Step == 7)
        {
            ValidateRecruitmentDates(model);

            if (model.RecruitingIdentifiableVolunteers == null)
            {
                ModelState.AddModelError("RecruitingIdentifiableVolunteers", "Select whether participants in the study will be recruited as named individual volunteers");
            }

            if (ModelState.IsValid)
            {
                model.Step = 8;
                ViewData["ProgressPercentage"] = (model.Step - 1) * 50;
                return View(model);
            }
        }

        return View(model);
    }

    public void ValidateRecruitmentDates(ResearcherStudyFormViewModel model)
    {
        // Recruitment Start Date

        if (model.RecruitmentStartDateDay == null)
        {
            ModelState.AddModelError("RecruitmentStartDateDay", "Recruitment start date (UK) must include a day");
        }

        if (model.RecruitmentStartDateMonth == null)
        {
            ModelState.AddModelError("RecruitmentStartDateMonth", "Recruitment start date (UK) must include a month");
        }

        if (model.RecruitmentStartDateYear == null)
        {
            ModelState.AddModelError("RecruitmentStartDateYear", "Recruitment start date (UK) must include a year");
        }

        if (model.RecruitmentStartDateDay == null && model.RecruitmentStartDateMonth == null && model.RecruitmentStartDateYear == null)
        {
            ClearRecruitmentStartDateStates();
            ModelState.AddModelError("RecruitmentStartDateDay", "Enter the recruitment start date (UK)");
        }

        if (model.RecruitmentStartDateDay != null && model.RecruitmentStartDateMonth == null && model.RecruitmentStartDateYear == null)
        {
            ClearRecruitmentStartDateStates();
            ModelState.AddModelError("RecruitmentStartDateDay", "Recruitment start date (UK) must include a month and year");
        }

        if (model.RecruitmentStartDateDay == null && model.RecruitmentStartDateMonth != null && model.RecruitmentStartDateYear == null)
        {
            ClearRecruitmentStartDateStates();
            ModelState.AddModelError("RecruitmentStartDateDay", "Recruitment start date (UK) must include a day and year");
        }

        if (model.RecruitmentStartDateDay == null && model.RecruitmentStartDateMonth == null && model.RecruitmentStartDateYear != null)
        {
            ClearRecruitmentStartDateStates();
            ModelState.AddModelError("RecruitmentStartDateDay", "Recruitment start date (UK) must include a day and month");
        }

        int startDateErrorCount = 0;

        if (model.RecruitmentStartDateDay > 31)
        {
            startDateErrorCount++;
            ModelState.AddModelError("RecruitmentStartDateDay", "Recruitment start date (UK) Day must be a real date");
        }

        if (model.RecruitmentStartDateMonth > 12)
        {
            startDateErrorCount++;
            ModelState.AddModelError("RecruitmentStartDateMonth", "Recruitment start date (UK) Month must be a real date");
        }

        if (model.RecruitmentStartDateYear.ToString().Length != 4)
        {
            startDateErrorCount++;
            ModelState.AddModelError("RecruitmentStartDateYear", "Recruitment start date (UK) Year must be 4 numbers");
        }

        if (model.RecruitmentStartDateYear < 1900)
        {
            startDateErrorCount++;
            ModelState.AddModelError("RecruitmentStartDateYear", "Recruitment start date (UK) Year must be later than 1900");
        }

        if (startDateErrorCount > 1)
        {
            ClearRecruitmentStartDateStates();
            ModelState.AddModelError("RecruitmentStartDateDay", "Recruitment start date (UK) must be a real date");
        }

        // Recruitment End Date

        if (model.RecruitmentEndDateDay == null)
        {
            ModelState.AddModelError("RecruitmentEndDateDay", "Recruitment end date (UK) must include a day");
        }

        if (model.RecruitmentEndDateMonth == null)
        {
            ModelState.AddModelError("RecruitmentEndDateMonth", "Recruitment end date (UK) must include a month");
        }

        if (model.RecruitmentEndDateYear == null)
        {
            ModelState.AddModelError("RecruitmentEndDateYear", "Recruitment end date (UK) must include a year");
        }

        if (model.RecruitmentEndDateDay == null && model.RecruitmentEndDateMonth == null && model.RecruitmentEndDateYear == null)
        {
            ClearRecruitmentEndDateStates();
            ModelState.AddModelError("RecruitmentEndDateDay", "Enter the recruitment end date (UK)");
        }

        if (model.RecruitmentEndDateDay != null && model.RecruitmentEndDateMonth == null && model.RecruitmentEndDateYear == null)
        {
            ClearRecruitmentEndDateStates();
            ModelState.AddModelError("RecruitmentEndDateDay", "Recruitment end date (UK) must include a month and year");
        }

        if (model.RecruitmentEndDateDay == null && model.RecruitmentEndDateMonth != null && model.RecruitmentEndDateYear == null)
        {
            ClearRecruitmentEndDateStates();
            ModelState.AddModelError("RecruitmentEndDateDay", "Recruitment end date (UK) must include a day and year");
        }

        if (model.RecruitmentEndDateDay == null && model.RecruitmentEndDateMonth == null && model.RecruitmentEndDateYear != null)
        {
            ClearRecruitmentEndDateStates();
            ModelState.AddModelError("RecruitmentEndDateDay", "Recruitment end date (UK) must include a day and month");
        }

        int endDateErrorCount = 0;

        if (model.RecruitmentEndDateDay > 31)
        {
            endDateErrorCount++;
            ModelState.AddModelError("RecruitmentEndDateDay", "Recruitment end date (UK) Day must be a real date");
        }

        if (model.RecruitmentEndDateMonth > 12)
        {
            endDateErrorCount++;
            ModelState.AddModelError("RecruitmentEndDateMonth", "Recruitment end date (UK) Month must be a real date");
        }

        if (model.RecruitmentEndDateYear.ToString().Length != 4)
        {
            endDateErrorCount++;
            ModelState.AddModelError("RecruitmentEndDateYear", "Recruitment end date (UK) Year must be 4 numbers");
        }

        if (model.RecruitmentEndDateYear < 1900)
        {
            endDateErrorCount++;
            ModelState.AddModelError("RecruitmentEndDateYear", "Recruitment end date (UK) Year must be later than 1900");
        }

        if (endDateErrorCount > 1)
        {
            ClearRecruitmentEndDateStates();
            ModelState.AddModelError("RecruitmentEndDateDay", "Recruitment end date (UK) must be a real date");
        }
    }

    public void ClearRecruitmentStartDateStates()
    {
        ModelState["RecruitmentStartDateDay"].Errors.Clear();
        ModelState["RecruitmentStartDateMonth"].Errors.Clear();
        ModelState["RecruitmentStartDateYear"].Errors.Clear();
    }

    public void ClearRecruitmentEndDateStates()
    {
        ModelState["RecruitmentEndDateDay"].Errors.Clear();
        ModelState["RecruitmentEndDateMonth"].Errors.Clear();
        ModelState["RecruitmentEndDateYear"].Errors.Clear();
    }
}
