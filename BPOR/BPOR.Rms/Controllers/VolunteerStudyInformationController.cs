using BPOR.Domain.Enums;
using BPOR.Rms.Models.VolunteerStudyInformation;
using BPOR.Rms.Models.VolunteerStudyInformation.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BPOR.Rms.Controllers;

[Route("Study/{id}/VolunteerInformation/edit")]
public class VolunteerStudyInformationController : Controller
{

    static VolunteerStudyInformationController()
    {
        var metadata = new MultistepFormBuilder<VsiViewModel>()
            .AddSection("Section 1. Overview", section => section
                .AddPropertyStep("About the Study", step => step
                    .AddProperty(i => i.Description))
                .AddPropertyStep("Select the type of study", step => step
                    .AddRadioProperty(i => i.StudyType, radio => radio
                        .AddOption(VsiStudyTypeId.InPerson, "In Person")
                        .AddOption(VsiStudyTypeId.Remote, "Remote")
                        .AddOption(VsiStudyTypeId.Hybrid, "Hybrid"))
                    .WithPostAction("SelectStudyType"))
                .AddCustomActionStep("ResearchLocation", step => step
                    .ShowOnlyIf(i => i.StudyType is VsiStudyTypeId.InPerson or VsiStudyTypeId.Hybrid))
                .AddCustomActionStep("VolunteerGroups"))
            .AddSection("Section 2: What does the study involve?", section => section
                .AddPropertyStep("What you will do", step => step
                    .WithDescription("A brief explanation about what the volunteer is expected to do immediately after recruitment")
                    .AddProperty(i => i.WhatWillYouDo))
                .AddPropertyStep("Will volunteers be offered an incentive for taking part?", step => step
                    .AddYesNoProperty(i => i.HasIncentive)
                    .AddProperty(i => i.IncentiveDetails, prop => prop
                        .WithCaption("If yes please provide details of the incentives the volunteer will receive for taking part.")))
            ).Build();
    }
    public IActionResult Index(long id)
    {
        return View("Start");
    }

    [HttpGet("step1")]
    public IActionResult Step1(long id)
    {
        
    }
    
    [HttpGet("ResearchLocation")]
    public IActionResult ResearchLocation(long id)
    {
        
    }
    
    [HttpGet("VolunteerGroups")]
    public IActionResult VolunteerGroups(long id)
    {
        
    }
}

public class VsiViewModel
{
    public string Description { get; set; }
    public VsiStudyTypeId StudyType { get; set; }
    public string WhatWillYouDo { get; set; }
    public bool HasIncentive { get; set; }
    public string IncentiveDetails { get; set; }
}