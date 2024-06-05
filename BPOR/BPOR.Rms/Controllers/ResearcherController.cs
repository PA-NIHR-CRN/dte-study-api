using BPOR.Domain.Entities;
using BPOR.Domain.Entities.RefData;
using BPOR.Rms.Models.Email;
using BPOR.Rms.Models.Researcher;
using BPOR.Rms.Models.Study;
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
        ViewData["ShowProgressBar"] = true;
        ViewData["ProgressPercentage"] = 0;

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitStudy(ResearcherStudyFormViewModel model, string action)
    {
        ViewData["ShowProgressBar"] = true;
        ViewData["ProgressPercentage"] = (model.Step - 1) * 15;

        model.PortfolioSubmissionStatusOptions = context.Submitted.ToList();
        model.OutcomeOfSubmissionOptions = context.SubmissionOutcome.ToList();

        if (action == "RedirectToCheckAnswers")
        {
            model.Step = 8;
            ViewData["ShowProgressBar"] = false;
            model.RedirectToCheckYourAnswers = false;
            return View(model);
        }

        if (action.Contains("RedirectStep")
            && action != "RedirectStep2"
            && action != "RedirectStep4")
        {
            model.RedirectToCheckYourAnswers = true;
        }
        else
        {
            model.RedirectToCheckYourAnswers = false;
        }

        if (action == "Back")
        {
            // Go back two steps if dependency questions are not required
            if ((model.Step == 4 && model.PortfolioSubmissionStatus != 1)
                || (model.Step == 6 && (model.HasFunding == false || model.HasFunding == null)))
            {
                model.Step = model.Step - 2;
            }
            else
            {
                model.Step = model.Step - 1;
            }

            ViewData["ProgressPercentage"] = (model.Step - 1) * 15;
            return View(model);
        }

        if (action == "RedirectStep1")
        {
            model.Step = 1;
            ViewData["ProgressPercentage"] = (model.Step - 1) * 15;
            return View(model);
        }

        if ((action == "Next" && model.Step == 1) || action == "RedirectStep2")
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
                ViewData["ProgressPercentage"] = (model.Step - 1) * 15;
                return View(model);
            }
        }
        else if ((action == "Next" && model.Step == 2) || action == "RedirectStep3")
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
                    model.OutcomeOfSubmission = null;
                    model.CPMSId = null;
                    if (model.RedirectToCheckYourAnswers)
                    {
                        model.Step = 8;
                    }
                    else
                    {
                        model.Step = 4;
                    }
                }
                ViewData["ProgressPercentage"] = (model.Step - 1) * 15;
                return View(model);
            }
        }
        else if ((action == "Next" && model.Step == 3) || action == "RedirectStep4")
        {
            if (model.OutcomeOfSubmission == null && model.PortfolioSubmissionStatus == 1)
            {
                ModelState.AddModelError("OutcomeOfSubmission", "Select the outcome of the submission for inclusion on the NIHR CRN portfolio");
            }

            if (model.CPMSId == null && model.PortfolioSubmissionStatus == 1)
            {
                ModelState.AddModelError("CPMSId", "Enter the CPMS ID for the study");
            }

            if (ModelState.IsValid)
            {
                model.Step = 4;
                ViewData["ProgressPercentage"] = (model.Step - 1) * 15;
                return View(model);
            }
        }
        else if ((action == "Next" && model.Step == 4) || action == "RedirectStep5")
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
                    model.FundingCode = null;
                    if (model.RedirectToCheckYourAnswers)
                    {
                        model.Step = 8;
                    }
                    else
                    {
                        model.Step = 6;
                    }
                }
                ViewData["ProgressPercentage"] = (model.Step - 1) * 15;
                return View(model);
            }
        }
        else if ((action == "Next" && model.Step == 5) || action == "RedirectStep6")
        {
            if (String.IsNullOrEmpty(model.FundingCode) && model.HasFunding == true)
            {
                ModelState.AddModelError("FundingCode", "Enter the NIHR funding stream or grant code");
            }

            if (ModelState.IsValid)
            {
                model.Step = 6;
                ViewData["ProgressPercentage"] = (model.Step - 1) * 15;
                return View(model);
            }
        }
        else if ((action == "Next" && model.Step == 6) || action == "RedirectStep7")
        {
            if (model.UKRecruitmentTarget == null)
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
                ViewData["ProgressPercentage"] = (model.Step - 1) * 15;
                return View(model);
            }
        }
        else if (action == "Next" && model.Step == 7)
        {
            ValidateRecruitmentDates(model);

            if (ModelState["RecruitmentStartDate.Day"].Errors.Count == 0 &&
                ModelState["RecruitmentStartDate.Month"].Errors.Count == 0 &&
                ModelState["RecruitmentStartDate.Year"].Errors.Count == 0 &&
                ModelState["RecruitmentEndDate.Day"].Errors.Count == 0 &&
                ModelState["RecruitmentEndDate.Month"].Errors.Count == 0 &&
                ModelState["RecruitmentEndDate.Year"].Errors.Count == 0)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                if (model.RecruitmentEndDate.ToDateOnly() < today)
                {
                    ModelState.AddModelError("RecruitmentEndDate.Day", "Recruitment end date (UK) must be in the future");
                }

                if (model.RecruitmentStartDate.ToDateOnly() > model.RecruitmentEndDate.ToDateOnly())
                {
                    ModelState.AddModelError("RecruitmentEndDate.Day", "Recruitment end date (UK) must be the same as or after Recruitment start date (UK)");
                }
            }

            if (model.RecruitingIdentifiableVolunteers == null)
            {
                ModelState.AddModelError("RecruitingIdentifiableVolunteers", "Select whether participants in the study will be recruited as named individual volunteers");
            }

            if (ModelState.IsValid)
            {
                model.Step = 8;
                ViewData["ShowProgressBar"] = false;
                return View(model);
            }
        }
        else if (action == "Apply")
        {
            if (ModelState.IsValid)
            {
                var study = new Study
                {
                    FullName = "", // TODO - take from IDG details
                    EmailAddress = "", // TODO - take from IDG details
                    StudyName = model.ShortName,
                    ChiefInvestigator = model.ChiefInvestigator,
                    Sponsors = model.StudySponsors,
                    Submitted = context.Submitted.Where(s => s.Id == model.PortfolioSubmissionStatus).FirstOrDefault(),
                    HasNihrFunding = model.HasFunding,
                    RecruitmentTarget = model.UKRecruitmentTarget,
                    TargetPopulation = model.TargetPopulation,
                    RecruitmentStartDate = model.RecruitmentStartDate.ToDateOnly()?.ToDateTime(TimeOnly.MinValue),
                    RecruitmentEndDate = model.RecruitmentEndDate.ToDateOnly()?.ToDateTime(TimeOnly.MaxValue),
                    IsRecruitingIdentifiableParticipants = model.RecruitingIdentifiableVolunteers.Value,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                if (model.PortfolioSubmissionStatus == 1)
                {
                    study.SubmissionOutcome = context.SubmissionOutcome.Where(o => o.Id == model.OutcomeOfSubmission).FirstOrDefault();
                    study.CpmsId = model.CPMSId;
                }

                if (model.HasFunding == true)
                {
                    study.FundingCode = model.FundingCode;
                }

                context.Add(study);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(AddStudySuccess), new AddStudySuccessViewModel
                {
                    Id = study.Id,
                    StudyName = study.StudyName,
                });
            }
        }

        return View(model);
    }

    public IActionResult AddStudySuccess(AddStudySuccessViewModel viewModel)
    {
        return View(viewModel);
    }

    public void ValidateRecruitmentDates(ResearcherStudyFormViewModel model)
    {
        // Recruitment Start Date

        if (model.RecruitmentStartDate.Day == null)
        {
            ModelState.AddModelError("RecruitmentStartDate.Day", "Recruitment start date (UK) must include a day");
        }

        if (model.RecruitmentStartDate.Month == null)
        {
            ModelState.AddModelError("RecruitmentStartDate.Month", "Recruitment start date (UK) must include a month");
        }

        if (model.RecruitmentStartDate.Year == null)
        {
            ModelState.AddModelError("RecruitmentStartDate.Year", "Recruitment start date (UK) must include a year");
        }

        if (model.RecruitmentStartDate.Day != null && model.RecruitmentStartDate.Month == null && model.RecruitmentStartDate.Year == null)
        {
            ClearRecruitmentStartDateStates();
            ModelState.AddModelError("RecruitmentStartDate.Day", "Recruitment start date (UK) must include a month and year");
        }

        if (model.RecruitmentStartDate.Day == null && model.RecruitmentStartDate.Month != null && model.RecruitmentStartDate.Year == null)
        {
            ClearRecruitmentStartDateStates();
            ModelState.AddModelError("RecruitmentStartDate.Day", "Recruitment start date (UK) must include a day and year");
        }

        if (model.RecruitmentStartDate.Day == null && model.RecruitmentStartDate.Month == null && model.RecruitmentStartDate.Year != null)
        {
            ClearRecruitmentStartDateStates();
            ModelState.AddModelError("RecruitmentStartDate.Day", "Recruitment start date (UK) must include a day and month");
        }

        int startDateErrorCount = 0;

        if (model.RecruitmentStartDate.Day > 31)
        {
            startDateErrorCount++;
            ModelState.AddModelError("RecruitmentStartDate.Day", "Recruitment start date (UK) Day must be a real date");
        }

        if (model.RecruitmentStartDate.Month > 12)
        {
            startDateErrorCount++;
            ModelState.AddModelError("RecruitmentStartDate.Month", "Recruitment start date (UK) Month must be a real date");
        }

        if (model.RecruitmentStartDate.Year.ToString().Length > 4)
        {
            startDateErrorCount++;
            ModelState.AddModelError("RecruitmentStartDate.Year", "Recruitment start date (UK) Year must be 4 numbers");
        }

        if (model.RecruitmentStartDate.Year < 1900)
        {
            startDateErrorCount++;
            ModelState.AddModelError("RecruitmentStartDate.Year", "Recruitment start date (UK) Year must be later than 1900");
        }

        if (startDateErrorCount > 1)
        {
            ClearRecruitmentStartDateStates();
            ModelState.AddModelError("RecruitmentStartDate.Day", "Recruitment start date (UK) must be a real date");
        }

        if (model.RecruitmentStartDate.Day == null && model.RecruitmentStartDate.Month == null && model.RecruitmentStartDate.Year == null)
        {
            ClearRecruitmentStartDateStates();
            ModelState.AddModelError("RecruitmentStartDate.Day", "Enter the recruitment start date (UK)");
        }

        // Recruitment End Date

        if (model.RecruitmentEndDate.Day == null)
        {
            ModelState.AddModelError("RecruitmentEndDate.Day", "Recruitment end date (UK) must include a day");
        }

        if (model.RecruitmentEndDate.Month == null)
        {
            ModelState.AddModelError("RecruitmentEndDate.Month", "Recruitment end date (UK) must include a month");
        }

        if (model.RecruitmentEndDate.Year == null)
        {
            ModelState.AddModelError("RecruitmentEndDate.Year", "Recruitment end date (UK) must include a year");
        }

        if (model.RecruitmentEndDate.Day != null && model.RecruitmentEndDate.Month == null && model.RecruitmentEndDate.Year == null)
        {
            ClearRecruitmentEndDateStates();
            ModelState.AddModelError("RecruitmentEndDate.Day", "Recruitment end date (UK) must include a month and year");
        }

        if (model.RecruitmentEndDate.Day == null && model.RecruitmentEndDate.Month != null && model.RecruitmentEndDate.Year == null)
        {
            ClearRecruitmentEndDateStates();
            ModelState.AddModelError("RecruitmentEndDate.Day", "Recruitment end date (UK) must include a day and year");
        }

        if (model.RecruitmentEndDate.Day == null && model.RecruitmentEndDate.Month == null && model.RecruitmentEndDate.Year != null)
        {
            ClearRecruitmentEndDateStates();
            ModelState.AddModelError("RecruitmentEndDate.Day", "Recruitment end date (UK) must include a day and month");
        }

        int endDateErrorCount = 0;

        if (model.RecruitmentEndDate.Day > 31)
        {
            endDateErrorCount++;
            ModelState.AddModelError("RecruitmentEndDate.Day", "Recruitment end date (UK) Day must be a real date");
        }

        if (model.RecruitmentEndDate.Month > 12)
        {
            endDateErrorCount++;
            ModelState.AddModelError("RecruitmentEndDate.Month", "Recruitment end date (UK) Month must be a real date");
        }

        if (model.RecruitmentEndDate.Year.ToString().Length > 4)
        {
            endDateErrorCount++;
            ModelState.AddModelError("RecruitmentEndDate.Year", "Recruitment end date (UK) Year must be 4 numbers");
        }

        if (model.RecruitmentEndDate.Year < 1900)
        {
            endDateErrorCount++;
            ModelState.AddModelError("RecruitmentEndDate.Year", "Recruitment end date (UK) Year must be later than 1900");
        }

        if (endDateErrorCount > 1)
        {
            ClearRecruitmentEndDateStates();
            ModelState.AddModelError("RecruitmentEndDate.Day", "Recruitment end date (UK) must be a real date");
        }

        if (model.RecruitmentEndDate.Day == null && model.RecruitmentEndDate.Month == null && model.RecruitmentEndDate.Year == null)
        {
            ClearRecruitmentEndDateStates();
            ModelState.AddModelError("RecruitmentEndDate.Day", "Enter the recruitment end date (UK)");
        }
    }

    public void ClearRecruitmentStartDateStates()
    {
        ModelState["RecruitmentStartDate.Day"].Errors.Clear();
        ModelState["RecruitmentStartDate.Month"].Errors.Clear();
        ModelState["RecruitmentStartDate.Year"].Errors.Clear();
    }

    public void ClearRecruitmentEndDateStates()
    {
        ModelState["RecruitmentEndDate.Day"].Errors.Clear();
        ModelState["RecruitmentEndDate.Month"].Errors.Clear();
        ModelState["RecruitmentEndDate.Year"].Errors.Clear();
    }
}
