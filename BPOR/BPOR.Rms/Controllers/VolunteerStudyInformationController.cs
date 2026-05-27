using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Domain.Repositories;
using BPOR.Rms.Models.Study.VolunteerInformation;
using BPOR.Rms.Models.VolunteerStudyInformation.Metadata;
using BPOR.Rms.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.Controllers;

[Route("Study/{studyId}/VolunteerInformation")]
public class VolunteerStudyInformationController : Controller
{

    private static readonly TimeSpan draftTtl = new TimeSpan(8, 0, 0);
    
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

    [HttpGet("Start")]
    public IActionResult Start()
    {
        return View();
    }

    [HttpPost("Start")]
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
            StudyId = studyId
        };
        
        db.VolunteerStudyInformation.Add(newVsi);
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Step1", new { studyId = studyId, id = newVsi.Id });
    }

    public async Task<IActionResult> Resume([FromServices] VsiRepository db, int studyId, CancellationToken cancellationToken)
    {
        var vsiId = await db.GetActiveDraftId(studyId, cancellationToken);
        if (vsiId == null)
        {
            return NotFound();
        }
        else
        {
            // TODO: figure out which step to resume on.
            return RedirectToAction("Step1", new{studyId, id = vsiId});
        }
    }
    
    
    [HttpGet("{id}/step1")]
    public async Task<IActionResult> Step1([FromServices] VsiRepository vsiRepository, int studyId, long id, CancellationToken cancellationToken)
    {
        var model = await vsiRepository.GetActiveDraft(studyId, id,
            i => new VsiEditModel
            {
                Description = i.Description
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }
    
    [HttpPost("{id}/step1")]
    public async Task<IActionResult> Step1Post(
        [FromServices] VsiRepository db,
        [FromRoute] int studyId, 
        [FromRoute] int id,
        [FromForm] VsiEditModel model, 
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i=> i.Description).AddToModelState(ModelState);
        
        var vsi = await db.GetActiveDraft(studyId, id, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.Description = model.Description;
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Step2", new{studyId, id = vsi.Id});
    }
    
       
    [HttpGet("{id}/step2")]
    public async Task<IActionResult> Step2([FromServices] VsiRepository vsiRepository, int studyId, int id, CancellationToken cancellationToken)
    {
        var model = await vsiRepository.GetActiveDraft(studyId, id,
            i => new VsiEditModel
            {
                StudyType = i.StudyTypeId
            },
            cancellationToken);
        return model == null ? NotFound() : View(model);
    }
    
    [HttpPost("{id}/step2")]
    public async Task<IActionResult> Step2(
        [FromServices] VsiRepository db,
        [FromRoute] int studyId, 
        [FromRoute] int id,
        [FromForm] VsiEditModel model, 
        CancellationToken cancellationToken)
    {
        VsiValidator validator = new VsiValidator();
        validator.ValidateSpecificProperties(model, i=> i.StudyType).AddToModelState(ModelState);

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var vsi = await db.GetActiveDraft(studyId, id, cancellationToken);
        if (vsi == null)
        {
            return NotFound();
        }

        vsi.StudyTypeId = model.StudyType;
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Step3", new{studyId, id = vsi.Id});
    }

    
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
}

