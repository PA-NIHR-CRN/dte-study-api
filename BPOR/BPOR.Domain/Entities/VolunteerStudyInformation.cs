using BPOR.Domain.Enums;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class VolunteerStudyInformation : ISoftDelete
{
    public long Id { get; set; }
    public VolunteerStudyInformationStatusId StatusId { get; set;}
    public bool IsDeleted { get; set;}
    public string Description { get; set; }
    public VolunteerStudyInformationStudyTypeId StudyTypeId { get; set; }
    public string WhatYouWillDo { get; set; }
    public bool CostReimbursement { get; set; }
    public bool HasIncentive { get; set; }
    public string IncentiveDetails  { get; set; }  
    public string NumberOfVisits { get; set; }
    public string StudyDuration { get; set; }
    public string StudyFormat { get; set; }
    public string OtherDetails  { get; set; }
    public string ExternalWebsiteUrl { get; set; }
    public string InfoToRegisterByEmail { get; set; }
    public string StagedPreScreenerUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; } 
    
    public ICollection<VolunteerStudyInformationContact> Contacts { get; } = new List<VolunteerStudyInformationContact>();
    public ICollection<VolunteerStudyInformationGroup> Groups { get; } = new List<VolunteerStudyInformationGroup>();
    public ICollection<VolunteerStudyInformationSite> Sites { get; } = new List<VolunteerStudyInformationSite>();
}