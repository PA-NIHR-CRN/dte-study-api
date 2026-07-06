using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.Abstractions.Entities;

public class VsiPage
{
    public VsiStatus Status { get; set;}
    public bool IsDeleted { get; set;}
    public string? Description { get; set; }
    public VsiStudyType? StudyType { get; set; }
    public string? WhatYouWillDo { get; set; }
    public bool? CostReimbursement { get; set; }
    public bool? HasIncentive { get; set; }
    public string? IncentiveDetails  { get; set; }  
    public string? NumberOfVisits { get; set; }
    public string? StudyDuration { get; set; }
    public string? StudyFormat { get; set; }
    public string? OtherDetails  { get; set; }
    public string? ExternalWebsiteUrl { get; set; }
    public string? InfoToRegisterByEmail { get; set; }
    public string? PreScreenerUrl { get; set; }
    
    // Data copied from study to allow for the BPOR -> RMS dependency to be removed in future:
    public required string StudyName { get; set; }
    public DateTime? StudyRecruitmentEndDate { get; set; }
    
    public List<VsiContact> Contacts { get; set; } = new();
    public List<VsiGroup> Groups { get; set; } = new();
    public List<VsiSite> Sites { get; set; } = new();
    
}