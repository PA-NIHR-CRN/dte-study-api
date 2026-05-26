using BPOR.Domain.Entities;
using BPOR.Rms.Models;
using BPOR.Rms.Models.Researcher;
using BPOR.Rms.Models.Study;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIHR.GovUk.AspNetCore.Mvc;

namespace BPOR.Rms.Controllers;

public class ResearcherController(ParticipantDbContext context, ICurrentUserProvider<User> currentUserProvider) : Controller
{
    public IActionResult TermsAndConditions()
    {
        return View(new ResearcherTermsAndConditionsViewModel());
    }

    [HttpPost]
    public IActionResult TermsAndConditions(ResearcherTermsAndConditionsViewModel model)
    {
        if (!model.AgreedToTermsAndConditions)
        {
            ModelState.AddModelError("AgreedToTermsAndConditions", "Confirm that you have read and agree to the terms and conditions before applying");
        }

        if (ModelState.IsValid)
        {
            return RedirectToAction("SubmitStudy", model);
        }

        return View(model);
    }

    public IActionResult SubmitStudy(ResearcherStudyFormViewModel model)
    {
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitStudy(ResearcherStudyFormViewModel model, string action)
    {
        if (action == "Next" || action == "Apply")
        {
            ValidateMandatoryFields(model);
            if (ModelState.IsValid)
            {
                if (model.Step == model.TotalSteps)
                {
                    var user = currentUserProvider?.User?.ContactFullName;
                    var email = currentUserProvider?.User?.ContactEmail;

                    var study = new Study
                    {
                        FullName = user == null ? "" : user,
                        EmailAddress = email == null ? "" : email,
                        StudyName = model.ShortName,
                        ChiefInvestigator = model.ChiefInvestigator,
                        Sponsors = model.StudySponsors,
                        Submitted = context.Submitted.FirstOrDefault(s => s.Id == model.PortfolioSubmissionStatus),
                        HasNihrFunding = model.HasFunding,
                        RecruitmentTarget = model.UKRecruitmentTarget,
                        TargetPopulation = model.TargetPopulation,
                        RecruitmentStartDate = model.RecruitmentStartDate.ToDateOnly()?.ToDateTime(TimeOnly.MinValue),
                        RecruitmentEndDate = model.RecruitmentEndDate.ToDateOnly()?.ToDateTime(TimeOnly.MaxValue),
                        IsRecruitingIdentifiableParticipants = model.RecruitingIdentifiableVolunteers.Value,
                        SubmissionOutcomeId = model.PortfolioSubmissionStatus == 1 ? model.OutcomeOfSubmission : null,
                        CpmsId = model.PortfolioSubmissionStatus == 1 ? model.CPMSId : null,
                        FundingCode = model.HasFunding == true ? model.FundingCode : null,
                    };

                    context.Add(study);
                    await context.SaveChangesAsync();

                    return RedirectToAction(nameof(AddStudySuccess), new AddStudySuccessViewModel
                    {
                        Id = study.Id,
                        StudyName = study.StudyName,
                    });
                }
                else
                {
                    switch (model.Step)
                    {
                        case 2:
                            if (model.PortfolioSubmissionStatus != 1)
                            {
                                model.OutcomeOfSubmission = null;
                                ModelState.Remove(nameof(model.OutcomeOfSubmission));
                                model.CPMSId = null;
                                model.GotoNextStep(4);
                            }
                            else
                            {
                                model.Step = 3;
                            }
                            break;
                        case 4:
                            if (model.HasFunding != true)
                            {
                                model.FundingCode = null;
                                model.GotoNextStep(6);
                            }
                            else
                            {
                                model.Step = 5;
                            }
                            break;
                        default:
                            model.GotoNextStep();
                            break;
                    }
                }
            }
        }
        else if (action == "Back")
        {
            // Clear validation when clicking back link
            // TODO: Needs to be more robust when there are other action names
            ModelState.Clear();

            // Skip step if dependency questions are not required
            if (model.Step == 4 && model.PortfolioSubmissionStatus != 1)
            {
                model.GotoNextStep(2);
            }
            else if (model.Step == 3)
            {
                ValidateMandatoryFields(model);
                if (ModelState.IsValid)
                {
                    model.Step = 2;
                }
            }
            else if (model.Step == 6 && model.HasFunding != true)
            {
                model.GotoNextStep(4);
            }
            else if (model.Step == 5)
            {
                ValidateMandatoryFields(model);
                if (ModelState.IsValid)
                {
                    model.Step = 4;
                }
            }
            else if (model.Completed && model.Step < model.TotalSteps)
            {
                model.Step = model.TotalSteps;
            }
            else if (model.Step == 1)
            {
                // Back link is exiting the process.
                // Return to a known entry point.
                // TODO: add referer as a query parameter
                // at the start of the journey so we can start
                // from any location and the back link
                // will exit correctly.
                return RedirectToAction(nameof(TermsAndConditions));
            }
            else
            {
                model.Step--;
            }
        }


        model.PortfolioSubmissionStatusOptions = context.Submitted.ToList();
        model.OutcomeOfSubmissionOptions = context.SubmissionOutcome.ToList();

        return View(model);
    }


    private void ValidateMandatoryFields(ResearcherStudyFormViewModel model)
    {
        if (model.Step == 1)
        {
            if (String.IsNullOrEmpty(model.ShortName))
            {
                ModelState.AddModelError("ShortName", "Enter the study short name");
            }
            else if (model.ShortName.Length > 255)
            {
                ModelState.AddModelError("ShortName", "Study short name must be less than 255 characters");
            }

            if (String.IsNullOrEmpty(model.ChiefInvestigator))
            {
                ModelState.AddModelError("ChiefInvestigator", "Enter the name of the Chief Investigator for the study");
            }

            if (String.IsNullOrEmpty(model.StudySponsors))
            {
                ModelState.AddModelError("StudySponsors", "Enter the name(s) of the study sponsor(s), funder(s) and CRO (if applicable)");
            }
        }

        if (model.Step == 2)
        {
            if (model.PortfolioSubmissionStatus == null)
            {
                ModelState.AddModelError("PortfolioSubmissionStatus", "Select whether the study has been submitted for inclusion on the NIHR CRN portfolio");
            }
        }

        if (model.Step == 3)
        {
            if (model.OutcomeOfSubmission == null && model.PortfolioSubmissionStatus == 1)
            {
                ModelState.AddModelError("OutcomeOfSubmission", "Select the outcome of the submission for inclusion on the NIHR CRN portfolio");
            }

            if (model.CPMSId == null && model.PortfolioSubmissionStatus == 1)
            {
                ModelState.AddModelError("CPMSId", "Enter the CPMS ID for the study");
            }
        }

        if (model.Step == 4)
        {
            if (model.HasFunding == null)
            {
                ModelState.AddModelError("HasFunding", "Select whether the study has NIHR funding");
            }
        }

        if (model.Step == 5)
        {
            if (String.IsNullOrEmpty(model.FundingCode) && model.HasFunding == true)
            {
                ModelState.AddModelError("FundingCode", "Enter the NIHR funding stream or grant code");
            }
        }

        if (model.Step == 6)
        {
            if (model.UKRecruitmentTarget == null)
            {
                ModelState.AddModelError("UKRecruitmentTarget", "Enter the UK recruitment target for the study");
            }

            if (String.IsNullOrEmpty(model.TargetPopulation))
            {
                ModelState.AddModelError("TargetPopulation", "Enter the target population for the study");
            }
        }

        if (model.Step == 7)
        {
            var startValid = ValidateDate(model.RecruitmentStartDate, nameof(model.RecruitmentStartDate),
                "Recruitment start date (UK)");
            var endValid = ValidateDate(model.RecruitmentEndDate, nameof(model.RecruitmentEndDate),
                "Recruitment end date (UK)");

            if (startValid && endValid)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);

                if (model.RecruitmentEndDate.ToDateOnly() < today)
                {
                    ModelState.AddModelError("RecruitmentEndDate.Day",
                        "Recruitment end date (UK) must be in the future");
                }

                if (model.RecruitmentStartDate.ToDateOnly() > model.RecruitmentEndDate.ToDateOnly())
                {
                    ModelState.AddModelError("RecruitmentEndDate.Day",
                        "Recruitment end date (UK) must be the same as or after Recruitment start date (UK)");
                }
            }

            if (model.RecruitingIdentifiableVolunteers == null)
            {
                ModelState.AddModelError("RecruitingIdentifiableVolunteers",
                    "Select whether participants in the study will be recruited as named individual volunteers");
            }
        }
    }

    public IActionResult AddStudySuccess(AddStudySuccessViewModel viewModel)
    {
        return View(viewModel);
    }

    public bool ValidateDate(GovUkDate value, string propertyPath, string propertyCaption)
    {
        // All 3 values missing...
        if (value.Day == null && value.Month == null && value.Year == null)
        {
            ModelState.AddModelError($"{propertyPath}.Day", $"Enter {propertyCaption}");
            return false;
        }

        // 2 values missing...
        if (value.Day != null && value.Month == null && value.Year == null)
        {
            ModelState.AddModelError($"{propertyPath}.Day", $"{propertyCaption} must include a month and year");
            return false;
        }

        if (value.Day == null && value.Month != null && value.Year == null)
        {
            ModelState.AddModelError($"{propertyPath}.Day", $"{propertyCaption} must include a day and year");
            return false;
        }

        if (value.Day == null && value.Month == null && value.Year != null)
        {
            ModelState.AddModelError($"{propertyPath}.Day", $"{propertyCaption} must include a day and month");
            return false;
        }

        // 1 values missing...
        if (value.Day == null)
        {
            ModelState.AddModelError($"{propertyPath}.Day", $"{propertyCaption} must include a day");
            return false;
        }

        if (value.Month == null)
        {
            ModelState.AddModelError($"{propertyPath}.Month", $"{propertyCaption} must include a month");
            return false;
        }

        if (value.Year == null)
        {
            ModelState.AddModelError($"{propertyPath}.Year", $"{propertyCaption} must include a year");
            return false;
        }

        // Numeric ranges...
        if (value.Year < 1900 ||
            value.Year > 9999 ||
            value.Month < 1 ||
            value.Month > 12 ||
            value.Day < 1 ||
            value.Day > DateTime.DaysInMonth(value.Year.Value, value.Month.Value))
        {
            ModelState.AddModelError($"{propertyPath}.Day", $"{propertyCaption} Day must be a real date");
            return false;
        }

        return true;
    }

    public async Task<IActionResult> Edit(int id, int field)
    {
        var studyModel = await context.Studies
            .AsResearcherFormViewModel()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (studyModel == null)
        {
            return NotFound();
        }

        studyModel.Step = field;
        studyModel.PortfolioSubmissionStatusOptions = context.Submitted.ToList();
        studyModel.OutcomeOfSubmissionOptions = context.SubmissionOutcome.ToList();

        ViewData["IsEditMode"] = true;
        return View(studyModel);
    }

    // POST: Researcher/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("ShortName,ChiefInvestigator,StudySponsors,CPMSId,PortfolioSubmissionStatus,OutcomeOfSubmission," +
              "HasFunding,FundingCode,UKRecruitmentTarget,TargetPopulation,RecruitmentStartDate,RecruitmentEndDate,RecruitingIdentifiableVolunteers,Step")]
        ResearcherStudyFormViewModel model)
    {
        model.Id = id;
        model.PortfolioSubmissionStatusOptions = context.Submitted.ToList();
        model.OutcomeOfSubmissionOptions = context.SubmissionOutcome.ToList();

        ModelState.Remove("IsRecruitingIdentifiableParticipants");

        ValidateMandatoryFields(model);

        switch (model.Step)
        {
            case 2:
                if (model.PortfolioSubmissionStatus == 1)
                {
                    model.OutcomeOfSubmission = null;
                    model.Step = 3;
                    return View(model);
                }

                break;
            case 4:
                if (model.HasFunding == true)
                {
                    model.FundingCode = string.Empty;
                    model.Step = 5;
                    return View(model);
                }

                break;
        }

        if (ModelState.IsValid)
        {
            try
            {
                var studyToUpdate = await context.Studies.FirstOrDefaultAsync(s => s.Id == id);

                if (studyToUpdate == null)
                {
                    return NotFound();
                }

                switch (model.Step)
                {
                    case 1:
                        studyToUpdate.StudyName = model.ShortName;
                        studyToUpdate.ChiefInvestigator = model.ChiefInvestigator;
                        studyToUpdate.Sponsors = model.StudySponsors;
                        break;
                    case 2:
                        studyToUpdate.SubmittedId = model.PortfolioSubmissionStatus;
                        if (model.PortfolioSubmissionStatus != 1)
                        {
                            model.OutcomeOfSubmission = null;
                        }
                        break;
                    case 3:
                        studyToUpdate.SubmissionOutcomeId = model.OutcomeOfSubmission;
                        studyToUpdate.CpmsId = model.CPMSId;
                        break;
                    case 4:
                        studyToUpdate.HasNihrFunding = model.HasFunding;
                        if (model.HasFunding != true)
                        {
                            model.FundingCode = null;
                        }
                        break;
                    case 5:
                        studyToUpdate.FundingCode = model.FundingCode;
                        break;
                    case 6:
                        studyToUpdate.RecruitmentTarget = model.UKRecruitmentTarget;
                        studyToUpdate.TargetPopulation = model.TargetPopulation;
                        break;
                    case 7:
                        studyToUpdate.RecruitmentStartDate =
                            model.RecruitmentStartDate.ToDateOnly()?.ToDateTime(TimeOnly.MinValue);
                        studyToUpdate.RecruitmentEndDate =
                            model.RecruitmentEndDate.ToDateOnly()?.ToDateTime(TimeOnly.MinValue);
                        break;
                }


                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            this.AddNotification(new NotificationBannerModel
            {
                IsSuccess = true,
                Title = "Study details updated",
                Body = $"{model.ShortName} has been successfully updated"
            });

            return RedirectToAction(nameof(StudyController.Details), "Study", new { id });
        }

        ViewData["IsEditMode"] = true;

        return View(model);
    }

    private bool StudyExists(int id)
    {
        return context.Studies.Any(e => e.Id == id);
    }
}