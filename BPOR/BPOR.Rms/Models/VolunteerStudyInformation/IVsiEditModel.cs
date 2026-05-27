using BPOR.Domain.Enums;

namespace BPOR.Rms.Models.Study.VolunteerInformation;

public class VsiEditModel
{
    public string StepName { get; }
    public long Id { get; }
    public string? Description { get; set; }
    public VsiStudyTypeId? StudyType { get; set; }
    public string? WhatWillYouDo { get; set; }
    public bool? HasIncentive { get; set; }
    public string? IncentiveDetails { get; set; }
    public string? NumberOfVisits { get; set; }
    public string? StudyDuration { get; set; }
    public string? StudyFormat { get; set; }
    public string? OtherDetails  { get; set; }
    public string? ExternalWebsiteUrl { get; set; }
    public string? InfoToRegisterByEmail { get; set; }
    public string? StagedPreScreenerUrl { get; set; }
}

