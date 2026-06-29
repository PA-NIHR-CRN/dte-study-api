using System.ComponentModel.DataAnnotations;
using BPOR.Rms.Abstractions.Enums;
using BPOR.Rms.VolunteerInformation.Controllers;
using NIHR.Infrastructure.AspNetCore;

namespace BPOR.Rms.VolunteerInformation.Models;

public class VsiEditModel
{
    [Display(Name = "About the study", Description = "Provide a short description of the study")]
    public string? Description { get; set; }
    
    [Display(Name = "Select the type of study")]
    public VsiStudyType? StudyType { get; set; }
    
    [Display(Name = "What will you do?", Description = "A brief explanation about what the volunteer is expected to do immediately after recruitment.")]
    public string? WhatYouWillDo { get; set; }
    
    [Display(Name = "Will volunteers be reimbursed for travel and inconvenience costs?")]
    [ValueDisplayFormatter(typeof(YesNoFormatter))]
    public bool? CostReimbursement { get; set; }
    [Display(Name = "Will volunteers be offered an incentive for taking part?")]
    [ValueDisplayFormatter(typeof(YesNoFormatter))]
    public bool? HasIncentive { get; set; }
    [Display(Name = "If yes please provide details of the incentives the volunteer will receive for taking part.")]
    public string? IncentiveDetails { get; set; }
    [Display(Name = "Number of visits", Description = "The number of times a person would need to visit the research location.")]
    public string? NumberOfVisits { get; set; }
    [Display(Name = "Study duration", Description = "This can be in days, months or years. You may also want to mention any specific dates (if any).")]
    public string? StudyDuration { get; set; }
    [Display(Name = "What is the study format?", Description = "Describe the different stages of the study, including treatments and follow-up appointments. Use short sentences and bullet points as much as possible.")]
    public string? StudyFormat { get; set; }
    [Display(Name = "Other details", Description = "Provide any other details that are important for the volunteer to know, but hasn't been mentioned before.")]
    public string? OtherDetails  { get; set; }
    [Display(Name = "Link to external website:", Description = "Copy and paste the link here")]
    public string? ExternalWebsiteUrl { get; set; }
    [Display(Name = "Information to register by email", 
        Description = "If you want the volunteer to email you to register their interest, what information should they provide in the email?\n")]
    public string? InfoToRegisterByEmail { get; set; }
    [Display(Name = "Link to the pre-screener:", Description = "Copy and paste the link here")]
    public string? PreScreenerUrl { get; set; }

    [Display(Name = "Research location")]
    public IEnumerable<VsiSiteModel> Sites { get; set; } = [];
    
    public IEnumerable<VsiGroupModel> Groups { get; set; } = [];
    public IEnumerable<VsiContactModel> Contacts { get; set; } = [];

    public VsiStatus Status { get; set; }
}
