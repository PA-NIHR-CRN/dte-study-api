using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.VolunteerInformation.Data;
using BPOR.Rms.VolunteerInformation.Models;
using BPOR.Rms.VolunteerInformation.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIHR.Infrastructure.AspNetCore.Validation;
using NIHR.Rts.Client;

namespace BPOR.Rms.VolunteerInformation.Controllers;

[Route("Study/{studyId}/VolunteerInformation/[action]")]
public class VolunteerStudyInformationController : Controller
{
    // static VolunteerStudyInformationController()
    // {
    //     var metadata = new MultistepFormBuilder<VsiEditModel>()
    //         .AddSection("Section 1. Overview", section => section
    //             .AddPropertyStep("About the Study", step => step
    //                 .AddProperty(i => i.Description))
    //             .AddPropertyStep("Select the type of study", step => step
    //                 .AddRadioProperty(i => i.StudyType, radio => radio
    //                     .AddOption(VsiStudyTypeId.InPerson, "In Person")
    //                     .AddOption(VsiStudyTypeId.Remote, "Remote")
    //                     .AddOption(VsiStudyTypeId.Hybrid, "Hybrid"))
    //                 .WithPostAction("SelectStudyType"))
    //             .AddCustomActionStep("ResearchLocation", step => step
    //                 .ShowOnlyIf(i => i.StudyType is VsiStudyTypeId.InPerson or VsiStudyTypeId.Hybrid))
    //             .AddCustomActionStep("VolunteerGroups"))
    //         .AddSection("Section 2: What does the study involve?", section => section
    //             .AddPropertyStep("What you will do", step => step
    //                 .WithDescription(
    //                     "A brief explanation about what the volunteer is expected to do immediately after recruitment")
    //                 .AddProperty(i => i.WhatWillYouDo))
    //             .AddPropertyStep("Will volunteers be offered an incentive for taking part?", step => step
    //                 .AddYesNoProperty(i => i.HasIncentive)
    //                 .AddProperty(i => i.IncentiveDetails, prop => prop
    //                     .WithCaption(
    //                         "If yes please provide details of the incentives the volunteer will receive for taking part.")))
    //         ).Build();
    // }

    private void SetContext(VolunteerStudyInformation currentVsi, string sectionName, string? backUrl)
    {
        this.ViewBag.Context = new VsiEditContext()
        {
            Vsi = currentVsi,
            SectionName = sectionName,
            BackUrl = backUrl
        };
    }

    #region Start
    
    [HttpGet]
    public IActionResult Start()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Start(
        [FromServices] ParticipantDbContext db,
        [FromServices] VsiRepository vsiRepository,
        int studyId,
        CancellationToken cancellationToken)
    {
        if (!await db.Studies.AnyAsync(s => s.Id == studyId && !s.IsDeleted, cancellationToken))
        {
            return NotFound();
        }

        var activeDraftId = (await vsiRepository.GetActiveDraftId(studyId, cancellationToken));
        if (activeDraftId.HasValue)
        {
            // TODO: there is an existing active draft ... Cope with this better!!
            return BadRequest();
        }

        VolunteerStudyInformation newVsi = new VolunteerStudyInformation()
        {
            StudyId = studyId,
            StatusId = VolunteerStudyInformationStatusId.Draft
        };

        db.VolunteerStudyInformation.Add(newVsi);
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Section1_Step1", new { studyId = studyId, id = newVsi.Id });
    }

    public async Task<IActionResult> Resume([FromServices] VsiRepository db, int studyId,
        CancellationToken cancellationToken)
    {
        var vsiId = await db.GetActiveDraftId(studyId, cancellationToken);
        if (vsiId == null)
        {
            return NotFound();
        }
        else
        {
            // TODO: figure out which step to resume on.
            return RedirectToAction("Section1_Step1", new { studyId, id = vsiId });
        }
    }

    #endregion
    #region Step 1

    [HttpGet]
    public async Task<IActionResult> Section1_Step1([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }

        // Don't go back to the start page as it makes no sense once the VSI draft has been created.
        SetContext(currentVsi, Resources.Section1Name, Url.Action("Details", "Study", new { id = currentVsi.StudyId }));
        
        var data = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                Description = i.Description
            },
            cancellationToken);
        
        return data == null ? NotFound() : View(data);
    }


    [HttpPost]
    public async Task<IActionResult> Section1_Step1(
        [FromServices] VsiRepository db,
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.Description).AddToModelState(ModelState);

        var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.Description = model.Description;
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Section1_Step2", new { studyId, id = vsi.Id });
    }

    #endregion

    #region Step 2

    [HttpGet]
    public async Task<IActionResult> Section1_Step2([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }
        SetContext(currentVsi, Resources.Section1Name, Url.Action("Section1_Step1", new { id = currentVsi.StudyId }));
        
        var model = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                StudyType = i.StudyTypeId
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section1_Step2(
        [FromServices] VsiRepository db,
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.StudyType).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.StudyTypeId = model.StudyType;
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Section1_Step3", new { studyId, id = vsi.Id });
    }

    #endregion

    #region Step 3

    [HttpGet]
    public async Task<IActionResult> SiteSearch(
        [FromServices] IRtsAddressSource addressSource,
        [FromRoute] int studyId,
        string searchTerm,
        CancellationToken cancellationToken)
    {
        var result = string.IsNullOrWhiteSpace(searchTerm)
            ? []
            : await addressSource.SearchByPostcode(searchTerm, cancellationToken);
        return View(new SiteSearchModel
            { StudyId = studyId, SearchTerm = searchTerm, SearchResult = result.ToArray() });
    }

    [HttpPost]
    public async Task<IActionResult> SiteSearchPost(
        [FromServices] IRtsAddressSource addressSource,
        [FromServices] VsiRepository db,
        [FromRoute] int studyId,
        int rtsAddressId,
        CancellationToken cancellationToken)
    {
        var result = await addressSource.GetById(rtsAddressId, cancellationToken);
        if (result == null)
        {
            return BadRequest();
        }

        return await AddSite(db, result, studyId, cancellationToken);
    }

    private async Task<IActionResult> AddSite(VsiRepository db, RtsAddress addressToAdd, int studyId,
        CancellationToken cancellationToken)
    {
        var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.Sites.Add(new VolunteerStudyInformationSite()
        {
            AddressLine1 = addressToAdd.AddressLine1,
            AddressLine2 = addressToAdd.AddressLine2,
            AddressLine3 = addressToAdd.AddressLine3,
            AddressLine4 = addressToAdd.AddressLine4,
            AddressLine5 = addressToAdd.AddressLine5,
            Postcode = addressToAdd.Postcode
        });
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Section1_Step3", new { studyId });
    }

    [HttpGet]
    public async Task<IActionResult> Section1_Step3([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }
        SetContext(currentVsi, Resources.Section1Name, Url.Action("Section1_Step2", new { id = currentVsi.StudyId }));
        
        var model = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                Sites = i.Sites.Select(site => new VsiSiteModel
                {
                    AddressLine1 = site.AddressLine1,
                    AddressLine2 = site.AddressLine2,
                    AddressLine3 = site.AddressLine3,
                    AddressLine4 = site.AddressLine4,
                    AddressLine5 = site.AddressLine5,
                    Postcode = site.Postcode,
                    Id = site.Id
                })
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveSite([FromServices] ParticipantDbContext db, int studyId, int siteId,
        CancellationToken cancellationToken)
    {
        var site = await db.VolunteerStudyInformationSite.SingleOrDefaultAsync(i =>
            i.VolunteerStudyInformation.StudyId == studyId && i.Id == siteId, cancellationToken);
        if (site == null)
        {
            return NotFound();
        }

        db.VolunteerStudyInformationSite.Remove(site);
        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction("Section1_Step3", new { studyId, id = siteId });
    }

    [HttpGet]
    public IActionResult ManualSiteEntry(int studyId)
    {
        return View(new ManualSiteEntryModel { StudyId = studyId });
    }

    [HttpPost]
    public async Task<IActionResult> ManualSiteEntry(
        [FromServices] VsiRepository vsiRepository,
        int studyId,
        [FromForm] ManualSiteEntryModel model,
        CancellationToken cancellationToken)
    {
        // TODO: Validation

        return await AddSite(vsiRepository, model.Address, studyId, cancellationToken);
    }

    #endregion

    #region Step 4

    [HttpGet]
    public async Task<IActionResult> Section1_Step4([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }
        SetContext(currentVsi, Resources.Section1Name, Url.Action("Section1_Step3", new { id = currentVsi.StudyId }));
        
        var model = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                Groups = i.Groups.Select(g => new VsiGroupModel
                {
                    Id = g.Id,
                    Name = g.Name
                })
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExclusionCriterion(
        [FromServices] ParticipantDbContext db,
        int studyId,
        int id,
        [FromForm] string criteria,
        CancellationToken cancellationToken)
    {
        var group = await db.VolunteerStudyInformationGroup
            .SingleOrDefaultAsync(i => i.Id == id, cancellationToken);

        var newCriteria = new VolunteerStudyInformationGroupCriteria()
        {
            Criteria = criteria,
            TypeId = VolunteerStudyInformationGroupCriteriaTypeId.Exclude
        };

        group.Criteria.Add(newCriteria);
        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction("ExclusionCriteria", new { studyId, id });
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateInclusionCriterion(
        [FromServices] ParticipantDbContext db,
        int studyId,
        int id,
        [FromForm] string criteria,
        CancellationToken cancellationToken)
    {
        var group = await db.VolunteerStudyInformationGroup
            .SingleOrDefaultAsync(i => i.Id == id, cancellationToken);

        var newCriteria = new VolunteerStudyInformationGroupCriteria()
        {
            Criteria = criteria,
            TypeId = VolunteerStudyInformationGroupCriteriaTypeId.Include
        };

        group.Criteria.Add(newCriteria);
        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction("InclusionCriteria", new { studyId, id });
    }

    [HttpGet]
    public async Task<IActionResult> InclusionCriteria(
        [FromServices] ParticipantDbContext db, int studyId, int id, CancellationToken cancellationToken)
    {
        var group = await db.VolunteerStudyInformationGroup
            .Where(i => i.Id == id && i.VolunteerStudyInformation.StudyId == studyId)
            .Select(i => new VsiGroupModel()
                {
                    StudyId = studyId,
                    Id = i.Id,
                    Name = i.Name,
                    Criteria = i.Criteria.Select(c => new VsiGroupCriteriaModel
                    {
                        Id = c.Id,
                        Criteria = c.Criteria,
                        Type = c.TypeId
                    }).ToList()
                }
            )
            .SingleOrDefaultAsync(cancellationToken);

        return View(group);
    }

    [HttpGet]
    public IActionResult QueryAddExclusionCriteria(int studyId, int groupId)
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult QueryAddExclusionCriteria(int studyId, int groupId, [FromForm] bool addExclusionCriteria)
    {
        if (addExclusionCriteria)
        {
            return RedirectToAction("ExclusionCriteria");
        }
        else
        {
            return RedirectToAction("GroupOverview", new {studyId, groupId});
        }
    }

    [HttpGet]
    public async Task<IActionResult> ExclusionCriteria(
        [FromServices] ParticipantDbContext db, int studyId, int id, CancellationToken cancellationToken)
    {
        var group = await db.VolunteerStudyInformationGroup
            .Where(i => i.Id == id && i.VolunteerStudyInformation.StudyId == studyId)
            .Select(i => new VsiGroupModel()
                {
                    StudyId = studyId,
                    Id = i.Id,
                    Name = i.Name,
                    Criteria = i.Criteria.Select(c => new VsiGroupCriteriaModel
                    {
                        Id = c.Id,
                        Criteria = c.Criteria,
                        Type = c.TypeId
                    }).ToList()
                }
            )
            .SingleOrDefaultAsync(cancellationToken);

        return View(group);
    }
    
    [HttpPost]
    public async Task<IActionResult> ExclusionCriteria(
        [FromServices] ParticipantDbContext db, int studyId, int id, bool addExclusionCriteria, CancellationToken cancellationToken)
    {
        var group = await db.VolunteerStudyInformationGroup
            .Where(i => i.Id == id && i.VolunteerStudyInformation.StudyId == studyId)
            .Select(i => new VsiGroupModel()
                {
                    StudyId = studyId,
                    Id = i.Id,
                    Name = i.Name,
                    Criteria = i.Criteria.Select(c => new VsiGroupCriteriaModel
                    {
                        Id = c.Id,
                        Criteria = c.Criteria,
                        Type = c.TypeId
                    }).ToList()
                }
            )
            .SingleOrDefaultAsync(cancellationToken);

        return View(group);
    }

    [HttpGet]
    public async Task<IActionResult> CreateInclusionCriterion(
        [FromServices] VsiRepository db, int studyId, int id, CancellationToken cancellationToken)
    {
        var group = await db.GetCurrentVsiGroup(studyId, id,
            i => new VsiGroupModel()
            {
                StudyId = studyId,
                Id = i.Id,
                Name = i.Name
            }
            , cancellationToken);

        return View(group);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveGroup(
        [FromServices] VsiRepository vsiRepository,
        int studyId,
        [FromForm] int id,
        CancellationToken cancellationToken)
    {
        return RedirectToAction("Section1_Step4", new { studyId, id });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveCriteria(
        [FromServices] ParticipantDbContext db,
        int studyId,
        int id,
        CancellationToken cancellationToken)
    {
        var criteria = await db.VolunteerStudyInformationGroupCriteria
            .Include(i => i.Group)
            .SingleOrDefaultAsync(i => i.Id == id, cancellationToken: cancellationToken);

        db.VolunteerStudyInformationGroupCriteria.Remove(criteria);
        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction("InclusionCriteria", new { studyId, id = criteria.VolunteerStudyInformationGroupId });
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(
        [FromServices] VsiRepository vsiRepository,
        int studyId,
        [FromForm] string name,
        CancellationToken cancellationToken)
    {
        var vsi = await vsiRepository.GetCurrentVsi(studyId,
            cancellationToken);

        if (vsi == null)
        {
            return NotFound();
        }

        var group = new VolunteerStudyInformationGroup()
        {
            Name = name,
        };

        vsi.Groups.Add(group);
        await vsiRepository.SaveChangesAsync(cancellationToken);

        return RedirectToAction("CreateInclusionCriterion", new { studyId, group.Id });
    }

    #endregion

    #region Section2_Step1

    [HttpGet]
    public async Task<IActionResult> Section2_Step1([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }
        SetContext(currentVsi, Resources.Section1Name, Url.Action("Section1_Step4", new { id = currentVsi.StudyId }));
        
        var model = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                WhatWillYouDo = i.WhatYouWillDo
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Section2_Step1(
        [FromServices] VsiRepository db,
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.WhatWillYouDo).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.WhatYouWillDo = model.WhatWillYouDo;
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Section2_Step2", new { studyId, id = vsi.Id });
    }

    #endregion

    #region Section2_Step2

    [HttpGet]
    public async Task<IActionResult> Section2_Step2([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }
        SetContext(currentVsi, Resources.Section1Name, Url.Action("Section2_Step1", new { id = currentVsi.StudyId }));
        
        var model = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                CostReimbursement = i.CostReimbursement
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Section2_Step2(
        [FromServices] VsiRepository db,
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.CostReimbursement).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.CostReimbursement = model.CostReimbursement;
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Section2_Step3", new { studyId, id = vsi.Id });
    }

    #endregion

    #region Section 2 Step 3

    [HttpGet]
    public async Task<IActionResult> Section2_Step3([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }
        SetContext(currentVsi, Resources.Section1Name, Url.Action("Section2_Step2", new { id = currentVsi.StudyId }));
        
        var model = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                IncentiveDetails = i.IncentiveDetails
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section2_Step3(
        [FromServices] VsiRepository db,
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.IncentiveDetails).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.IncentiveDetails = model.IncentiveDetails;
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Section2_Step4", new { studyId });
    }

    #endregion

    #region Step 7

    [HttpGet]
    public async Task<IActionResult> Section2_Step4([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }
        SetContext(currentVsi, Resources.Section1Name, Url.Action("Section2_Step3", new { id = currentVsi.StudyId }));
        
        var model = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                NumberOfVisits = i.NumberOfVisits
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section2_Step4(
        [FromServices] VsiRepository db,
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.NumberOfVisits).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.NumberOfVisits = model.NumberOfVisits;
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Section2_Step5", new { studyId });
    }

    #endregion

    #region Section2_Step5
    [HttpGet]
    public async Task<IActionResult> Section2_Step5([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }
        SetContext(currentVsi, Resources.Section1Name, Url.Action("Section2_Step4", new { id = currentVsi.StudyId }));
        
        var model = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                StudyDuration = i.StudyDuration
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section2_Step5(
        [FromServices] VsiRepository db,
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.StudyDuration).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.StudyDuration = model.StudyDuration;
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Section2_Step6", new { studyId });
    }

    #endregion

    #region Section2_Step6

    [HttpGet]
    public async Task<IActionResult> Section2_Step6([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }
        SetContext(currentVsi, Resources.Section1Name, Url.Action("Section2_Step4", new { id = currentVsi.StudyId }));
        
        var model = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                StudyFormat = i.StudyFormat
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section2_Step6(
        [FromServices] VsiRepository db,
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.StudyFormat).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.StudyFormat = model.StudyFormat;
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Section2_Step7", new { studyId });
    }

    #endregion
    
    #region Section2_Step7

    [HttpGet]
    public async Task<IActionResult> Section2_Step7([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }
        SetContext(currentVsi, Resources.Section1Name, Url.Action("Section2_Step4", new { id = currentVsi.StudyId }));
        
        var model = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                OtherDetails = i.OtherDetails
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Section2_Step7(
        [FromServices] VsiRepository db,
        [FromRoute] int studyId,
        [FromForm] VsiEditModel model,
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i => i.OtherDetails).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.OtherDetails = model.OtherDetails;
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Section3_Step1", new { studyId });
    }

    #endregion
    [HttpGet("ResearchLocation")]
    public IActionResult ResearchLocation(long id)
    {
        return NotFound();
    }

    [HttpGet("VolunteerGroups")]
    public IActionResult VolunteerGroups(long id)
    {
        return NotFound();
    }

    [HttpGet]
    public IActionResult GroupOverview(int studyId, int groupId)
    {
        return View();
    }

    #region Section 3
    
        #region Section3_Step1

        [HttpGet]
        public async Task<IActionResult> Section3_Step1([FromServices] VsiRepository vsiRepository, int studyId,
            CancellationToken cancellationToken)
        {
            var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
            if (currentVsi == null)
            {
                return NotFound();
            }
            SetContext(currentVsi, Resources.Section3Name, Url.Action("Section2_Step4", new { id = currentVsi.StudyId }));
            
            var model = await vsiRepository.GetCurrentVsi(studyId,
                i => new VsiEditModel
                {
                    StagedPreScreenerUrl = i.StagedPreScreenerUrl
                },
                cancellationToken);
            return model == null ? NotFound() : View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Section3_Step1(
            [FromServices] VsiRepository db,
            [FromRoute] int studyId,
            [FromForm] VsiEditModel model,
            CancellationToken cancellationToken)
        {
            VsiValidator validator = new VsiValidator();
            validator.ValidateSpecificProperties(model, i => i.StagedPreScreenerUrl).AddToModelState(ModelState);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
            if (vsi == null)
            {
                return NotFound();
            }

            vsi.StagedPreScreenerUrl = model.StagedPreScreenerUrl;
            await db.SaveChangesAsync(cancellationToken);
            return RedirectToAction("Section3_Step2", new { studyId });
        }

        #endregion
        
        #region Section3_Step2

        [HttpGet]
        public async Task<IActionResult> Section3_Step2([FromServices] VsiRepository vsiRepository, int studyId,
            CancellationToken cancellationToken)
        {
            var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
            if (currentVsi == null)
            {
                return NotFound();
            }
            SetContext(currentVsi, Resources.Section3Name, Url.Action("Section2_Step4", new { id = currentVsi.StudyId }));
            
            var model = await vsiRepository.GetCurrentVsi(studyId,
                i => new VsiEditModel
                {
                    ExternalWebsiteUrl = i.ExternalWebsiteUrl
                },
                cancellationToken);
            return model == null ? NotFound() : View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Section3_Step2(
            [FromServices] VsiRepository db,
            [FromRoute] int studyId,
            [FromForm] VsiEditModel model,
            CancellationToken cancellationToken)
        {
            VsiValidator validator = new VsiValidator();
            validator.ValidateSpecificProperties(model, i => i.ExternalWebsiteUrl).AddToModelState(ModelState);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
            if (vsi == null)
            {
                return NotFound();
            }

            vsi.ExternalWebsiteUrl = model.ExternalWebsiteUrl;
            await db.SaveChangesAsync(cancellationToken);
            return RedirectToAction("Step9", new { studyId });
        }

        #endregion
        
        #region Section3_Step3

        [HttpGet]
        public async Task<IActionResult> Section3_Step3([FromServices] VsiRepository vsiRepository, int studyId,
            CancellationToken cancellationToken)
        {
            var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
            if (currentVsi == null)
            {
                return NotFound();
            }
            SetContext(currentVsi, Resources.Section3Name, Url.Action("Section2_Step4", new { id = currentVsi.StudyId }));
            
            var model = await vsiRepository.GetCurrentVsi(studyId,
                i => new VsiEditModel
                {
                    Contacts = i.Contacts.Select(c => new VsiContactModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Role = c.Role,
                        Organisation = c.Organisation,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber
                    })
                },
                cancellationToken);
            return model == null ? NotFound() : View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Section3_Step3(
            [FromServices] VsiRepository db,
            [FromRoute] int studyId,
            [FromForm] VsiEditModel model,
            CancellationToken cancellationToken)
        {
            VsiValidator validator = new VsiValidator();
            validator.ValidateSpecificProperties(model, i => i.ExternalWebsiteUrl).AddToModelState(ModelState);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
            if (vsi == null)
            {
                return NotFound();
            }

            vsi.ExternalWebsiteUrl = model.ExternalWebsiteUrl;
            await db.SaveChangesAsync(cancellationToken);
            return RedirectToAction("Section3_Step4", new { studyId });
        }

        #endregion
      
        #region Section3_Step4

        [HttpGet]
        public async Task<IActionResult> Section3_Step4([FromServices] VsiRepository vsiRepository, int studyId,
            CancellationToken cancellationToken)
        {
            var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
            if (currentVsi == null)
            {
                return NotFound();
            }
            SetContext(currentVsi, Resources.Section3Name, Url.Action("Section2_Step4", new { id = currentVsi.StudyId }));
            
            var model = await vsiRepository.GetCurrentVsi(studyId,
                i => new VsiEditModel
                {
                    InfoToRegisterByEmail = i.InfoToRegisterByEmail
                },
                cancellationToken);
            return model == null ? NotFound() : View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Section3_Step4(
            [FromServices] VsiRepository db,
            [FromRoute] int studyId,
            [FromForm] VsiEditModel model,
            CancellationToken cancellationToken)
        {
            VsiValidator validator = new VsiValidator();
            validator.ValidateSpecificProperties(model, i => i.InfoToRegisterByEmail).AddToModelState(ModelState);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var vsi = await db.GetCurrentVsi(studyId, cancellationToken);
            if (vsi == null)
            {
                return NotFound();
            }

            vsi.InfoToRegisterByEmail = model.InfoToRegisterByEmail;
            await db.SaveChangesAsync(cancellationToken);
            return RedirectToAction("Section4", new { studyId });
        }

        #endregion

    #endregion
    
    [HttpGet]
    public async Task<IActionResult> Section4([FromServices] VsiRepository vsiRepository, int studyId,
        CancellationToken cancellationToken)
    {
        var currentVsi = await vsiRepository.GetCurrentVsi(studyId, cancellationToken);
        if (currentVsi == null)
        {
            return NotFound();
        }
        SetContext(currentVsi, Resources.Section3Name, Url.Action("Section2_Step4", new { id = currentVsi.StudyId }));
            
        var model = await vsiRepository.GetCurrentVsi(studyId,
            i => new VsiEditModel
            {
                Contacts = i.Contacts.Select(c => new VsiContactModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Role = c.Role,
                    Organisation = c.Organisation,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber
                })
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }
}